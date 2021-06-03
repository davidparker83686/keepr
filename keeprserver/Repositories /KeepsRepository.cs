using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories_
{
  public class KeepsRepository
  {

    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    // -----------------------------------------------------------------------------------------------------
    public IEnumerable<Keep> GetAll()
    {
      string sql = @"
      SELECT
        k.*,
        a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id;";
      return _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
{
  keep.Creator = account;
  return keep;
}
, splitOn: "id");
    }
    // -----------------------------------------------------------------------------------------------------
    internal IEnumerable<Keep> GetKeepByProfile(string id)
    {
      string sql = "SELECT * FROM keeps WHERE creatorId = @id;";
      // return _db.QueryFirstOrDefault<Keep>(sql, new { id });
      // return _db.Query<Keep>(sql);
      return _db.Query<Keep>(sql, new { id });
    }
    // -----------------------------------------------------------------------------------------------------
    public Keep Create(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
      (creatorId, name, description, img, views, shares, keeps)
      VALUES
      ( @CreatorId, @Name, @Description, @Img, @Views, @Shares, @Keeps);
      SELECT LAST_INSERT_ID();";

      newKeep.Id = _db.ExecuteScalar<int>(sql, newKeep);
      return newKeep;
    }
    // -----------------------------------------------------------------------------------------------------
    // public Keep GetById(int id)
    // {
    //   string sql = "SELECT * FROM keeps WHERE id =@id;";
    //   return _db.QueryFirstOrDefault<Keep>(sql, new { id });
    // }
    // public Keep GetById(int id)
    // {
    //   string sql = "SELECT * FROM keeps JOIN accounts a ON k.creatorId = id WHERE id =accounts.id;";
    //   // return _db.QueryFirstOrDefault<Keep>(sql, new { id });
    //   return _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
    //   {
    //     keep.Creator = account;
    //     return keep;
    //   }
    //   , new { id }, splitOn: "id").FirstOrDefault();
    // }



    public Keep GetById(int id)
    {
      string sql = @"
      SELECT 
        k.*,
        a.* 
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.id = @id";
      return _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
      {
        keep.Creator = account;
        return keep;
      }
      , new { id }, splitOn: "id").FirstOrDefault();
    }
    // -----------------------------------------------------------------------------------------------------
    internal bool Update(Keep original)
    {
      string sql = @"
      UPDATE keeps
      SET
        name = @Name,
        description = @Description,
        img = @Img,
        views = @Views,
        shares = @Shares,
        keeps = @Keeps
      WHERE id = @Id
      ";
      int affectedRows = _db.Execute(sql, original);
      return affectedRows == 1;
    }
    // -----------------------------------------------------------------------------------------------------
    internal bool Delete(object id)
    {
      string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id });
      return affectedRows == 1;
    }
  }


}
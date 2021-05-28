using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keeprserver.Models;

namespace keeprserver.Repositories_
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
      string sql = "SELECT * FROM keeps";
      return _db.Query<Keep>(sql);
    }
    // -----------------------------------------------------------------------------------------------------
    public Keep Create(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, img, views, shares, keeps)
      VALUES
      ( @Name, @Description, @Img, @Views, @Shares, @Keeps);
      SELECT LAST_INSERT_ID();";

      newKeep.Id = _db.ExecuteScalar<int>(sql, newKeep);
      return newKeep;
    }
    // -----------------------------------------------------------------------------------------------------
    public Keep GetById(int id)
    {
      string sql = "SELECT * FROM keeps WHERE id =@id;";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id });
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
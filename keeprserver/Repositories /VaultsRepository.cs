using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories_
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }
    // -----------------------------------------------------------------------------------------------------
    public IEnumerable<Vault> GetAll()
    {
      string sql = "SELECT * FROM vaults";
      return _db.Query<Vault>(sql);
    }
    // -----------------------------------------------------------------------------------------------------
    public Vault Create(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults
      (creatorId, name, description, isPrivate, img)
      VALUES
      (@CreatorId, @Name, @Description, @IsPrivate, @Img);
      SELECT LAST_INSERT_ID();";

      newVault.Id = _db.ExecuteScalar<int>(sql, newVault);
      return newVault;
    }
    // -----------------------------------------------------------------------------------------------------
    public Vault GetById(int id)
    {
      string sql = @"
      SELECT 
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
       WHERE v.id =@id;";
      return _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
    {
      vault.Creator = account;
      return vault;
    }
    , new { id }, splitOn: "id").FirstOrDefault();
    }
    // -----------------------------------------------------------------------------------------------------
    internal bool Update(Vault original)
    {
      string sql = @"
      UPDATE vaults
      SET
        name = @Name,
        img = @Img,
        description = @Description,
        isPrivate = @IsPrivate
        WHERE id = @Id
      ";
      int affectedRows = _db.Execute(sql, original);
      return affectedRows == 1;
    }

    // -----------------------------------------------------------------------------------------------------

    // internal IEnumerable<Vault> GetVaultByProfile(string id)
    // {
    //   string sql = "SELECT * FROM vaults WHERE creatorId = @id;";
    //   return _db.Query<Vault>(sql, new { id });
    // }
    internal IEnumerable<Vault> GetVaultByProfile(string id)
    {
      string sql = @"SELECT * FROM vaults WHERE creatorId = @id;";
      return _db.Query<Vault>(sql, new { id });
    }

    // -----------------------------------------------------------------------------------------------------
    internal bool Delete(object id)
    {
      string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id });
      return affectedRows == 1;
    }
  }

}
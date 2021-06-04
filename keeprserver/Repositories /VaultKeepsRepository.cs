using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories_
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    // -----------------------------------------------------------------------------------------------------
    public VaultKeep Create(VaultKeep newVaultKeep)
    {
      string sql = @"
      INSERT INTO vaultkeeps
      (creatorId, vaultId, keepId)
      VALUES
      ( @CreatorId, @VaultId, @KeepId);
      SELECT LAST_INSERT_ID();";

      newVaultKeep.Id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      return newVaultKeep;
    }
    // -----------------------------------------------------------------------------------------------------
    internal bool Delete(object id)
    {
      string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id });
      return affectedRows == 1;
    }
    // -----------------------------------------------------------------------------------------------------
    // public IEnumerable<VaultKeep> GetAll()
    // {
    //   string sql = "SELECT * FROM vaultkeeps";
    //   return _db.Query<VaultKeep>(sql);
    // }
    // -----------------------------------------------------------------------------------------------------
    public VaultKeep GetById(int id)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE id =@id;";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }


    // -----------------------------------------------------------------------------------------------------
    // internal IEnumerable<Keep> GetKeepByVault(int id)
    // {
    //   string sql = "SELECT * FROM keeps WHERE creatorId = @id;";

    //   return _db.Query<Keep>(sql, new { id });
    // }

    internal IEnumerable<VaultKeepViewModel> GetKeepByVault(int id)
    {
      string sql = @"
                SELECT
                v.*,
                k.*,
                vk.id as vaultkeepId,
                vk.vaultId as vaultId,
                vk.keepId as keepId
                FROM vaultkeeps vk
                JOIN vaults v ON v.id = vk.vaultId
                JOIN keeps k ON k.id = vk.keepId
                WHERE vk.vaultId = @id;";
      return _db.Query<VaultKeepViewModel>(sql, new { id }).ToList();
    }
  }
}
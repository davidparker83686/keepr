using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories_;

namespace keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vaultsRepository;
    private readonly VaultKeepsRepository _vaultKeepsRepository;

    public VaultsService(VaultsRepository vaultsRepository, VaultKeepsRepository vaultKeepsRepository)
    {
      _vaultsRepository = vaultsRepository;
      _vaultKeepsRepository = vaultKeepsRepository;
    }
    // -----------------------------------------------------------------------------------------------------
    internal IEnumerable<Vault> GetAll()
    {
      return _vaultsRepository.GetAll();
    }
    // -----------------------------------------------------------------------------------------------------
    internal IEnumerable<Vault> GetByCreatorId(string id)
    {
      throw new NotImplementedException();
    }
    // -----------------------------------------------------------------------------------------------------
    internal Vault Create(Vault newVault)
    {
      Vault vaults = _vaultsRepository.Create(newVault);
      return vaults;
    }
    // -----------------------------------------------------------------------------------------------------
    internal IEnumerable<VaultKeepViewModel> GetKeepByVault(int id)
    {
      return _vaultKeepsRepository.GetKeepByVault(id);
    }
    // -----------------------------------------------------------------------------------------------------
    internal Vault GetById(int id)
    {
      Vault vault = _vaultsRepository.GetById(id);
      if (vault == null)
      {
        throw new Exception("Invalid Vault Id");
      }
      return (Vault)vault;
    }
    // -----------------------------------------------------------------------------------------------------
    internal IEnumerable<Vault> GetVaultByProfile(string id)
    {
      return _vaultsRepository.GetVaultByProfile(id);
    }
    // -----------------------------------------------------------------------------------------------------

    internal void Delete(int apple, string id2)
    {
      Vault vault = GetById(apple);
      if (!_vaultsRepository.Delete(apple))
      {
        throw new Exception("Something has gone terribly wrong");
      }
    }



    // -----------------------------------------------------------------------------------------------------
    internal Vault Update(Vault update)
    {
      Vault original = GetById(update.Id);
      original.Name = update.Name.Length > 0 ? update.Name : original.Name;
      original.Description = update.Description.Length > 0 ? update.Description : original.Description;
      original.IsPrivate = update.IsPrivate != update.IsPrivate ? update.IsPrivate : original.IsPrivate;

      if (_vaultsRepository.Update(original))
      {
        return original;
      }
      throw new Exception("Something Went Wrong???");
    }
  }

}
using System;
using System.Collections.Generic;
using keeprserver.Models;
using keeprserver.Repositories_;
using keeprserver.Services;

namespace keeprserver.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vaultKeepsRepository;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository)
    {
      _vaultKeepsRepository = vaultKeepsRepository;
    }
    // -----------------------------------------------------------------------------------------------------
    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      VaultKeep vaultKeeps = _vaultKeepsRepository.Create(newVaultKeep);
      return vaultKeeps;
    }
    // -----------------------------------------------------------------------------------------------------

    internal void Delete(int apple, string id2)
    {
      VaultKeep vaultKeep = GetById(apple);
      if (!_vaultKeepsRepository.Delete(apple))
      {
        throw new Exception("Something has gone terribly wrong");
      }
    }

    // -----------------------------------------------------------------------------------------------------
    internal VaultKeep GetById(int id)
    {
      VaultKeep vaultKeep = _vaultKeepsRepository.GetById(id);
      if (vaultKeep == null)
      {
        throw new Exception("Invalid Vault Id");
      }
      return (VaultKeep)vaultKeep;
    }
  }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vaultKeepsService;
    private readonly VaultsService _vaultsService;

    public VaultKeepsController(VaultKeepsService vaultKeepsService, VaultsService vaultsService)
    {
      _vaultsService = vaultsService;
      _vaultKeepsService = vaultKeepsService;
    }
    // -----------------------------------------------------------------------------------------------------
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVaultKeep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        // Account fullAccount = _accountService.GetOrCreateAccount(userInfo);
        newVaultKeep.CreatorId = userInfo.Id;

        VaultKeep vaultKeeps = _vaultKeepsService.Create(newVaultKeep);
        //TODO[epic=Populate] adds the account to the new object as the creator
        vaultKeeps.Creator = userInfo;
        return Ok(vaultKeeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // -----------------------------------------------------------------------------------------------------
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        VaultKeep vaultKeeps = _vaultKeepsService.GetById(id);
        Vault vaults = _vaultsService.GetById(vaultKeeps.VaultId);
        if (vaults.IsPrivate == true && vaults.CreatorId != userInfo.Id)
        {
          throw new Exception("This vault is private");
        }
        _vaultKeepsService.Delete(id, userInfo.Id);
        return Ok("Deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // -----------------------------------------------------------------------------------------------------

    [HttpGet("{id}")]
    public ActionResult<VaultKeep> GetById(int id)
    {
      try
      {
        VaultKeep vaultKeeps = _vaultKeepsService.GetById(id);
        return Ok(vaultKeeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}
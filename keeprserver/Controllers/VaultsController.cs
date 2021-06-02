using System;
using System.Collections.Generic;
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
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vaultsService;
    private readonly KeepsService _keepsService;
    private readonly AccountService _accountService;

    public VaultsController(VaultsService vaultsService, KeepsService keepsService, AccountService accountService)
    {
      _vaultsService = vaultsService;
      _accountService = accountService;
      _keepsService = keepsService;
    }
    // -----------------------------------------------------------------------------------------------------
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        // Account fullAccount = _accountService.GetOrCreateAccount(userInfo);
        newVault.CreatorId = userInfo.Id;

        Vault vaults = _vaultsService.Create(newVault);
        //TODO[epic=Populate] adds the account to the new object as the creator
        vaults.Creator = userInfo;
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // -----------------------------------------------------------------------------------------------------
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Delete(int id)
    {
      try
      {
        // TODO[epic=Auth] Get the user info to set the creatorID
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        // safety to make sure an account exists for that user before CREATE-ing stuff.
        _vaultsService.Delete(id, userInfo.Id);
        return Ok("Deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // -----------------------------------------------------------------------------------------------------
    [HttpGet]
    public ActionResult<IEnumerable<Vault>> Get()
    {
      try
      {
        IEnumerable<Vault> vaults = _vaultsService.GetAll();
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // -----------------------------------------------------------------------------------------------------

    // [HttpGet("{id}")]
    // public ActionResult<Vault> GetById(int id)
    // {
    //   try
    //   {

    //     Vault vaults = _vaultsService.GetById(id);
    //     return Ok(vaults);
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> GetByIdAsync(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Vault vaults = _vaultsService.GetById(id);
        if (vaults.IsPrivate == true && vaults.CreatorId != userInfo.Id)
        {
          throw new Exception("This vault is private");
        }
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // -----------------------------------------------------------------------------------------------------
    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<IEnumerable<VaultKeepViewModel>>> GetKeepByVaultAsync(int id)
    {
      try
      {

        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Vault vaults = _vaultsService.GetById(id);
        if (vaults.IsPrivate == true && vaults.CreatorId != userInfo.Id)
        {
          throw new Exception("This vault is private");
        }



        IEnumerable<Keep> keeps = _vaultsService.GetKeepByVault(id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // -----------------------------------------------------------------------------------------------------
    [HttpPut("{id}")]
    [Authorize]
    public ActionResult<Vault> Update(int id, [FromBody] Vault update)
    {
      try
      {
        update.Id = id;
        Vault updated = _vaultsService.Update(update);
        return Ok(updated);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}
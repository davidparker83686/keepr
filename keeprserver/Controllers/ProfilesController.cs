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
  public class ProfilesController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly KeepsService _keepsService;
    private readonly VaultKeepsService _vaultKeepsService;
    private readonly VaultsService _vaultsService;

    public ProfilesController(KeepsService keepsService, AccountService accountService, VaultsService vaultsService, VaultKeepsService vaultKeepsService)
    {
      _accountService = accountService;
      _keepsService = keepsService;
      _vaultsService = vaultsService;
      _vaultKeepsService = vaultKeepsService;
    }
    // -----------------------------------------------------------------------------------------------------
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // -----------------------------------------------------------------------------------------------------
    [HttpGet("{id}/keeps")]
    public ActionResult<IEnumerable<Keep>> GetKeepByProfile(string id)
    {
      try
      {
        IEnumerable<Keep> keeps = _keepsService.GetKeepByProfile(id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // -----------------------------------------------------------------------------------------------------
    [HttpGet("{id}/vaults")]
    public ActionResult<IEnumerable<Vault>> GetVaultByProfile(string id)
    {
      try
      {
        IEnumerable<Vault> vaults = _vaultsService.GetVaultByProfile(id);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // -----------------------------------------------------------------------------------------------------

  }
}
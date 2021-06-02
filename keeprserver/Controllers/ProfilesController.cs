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
    [HttpGet("{id}")]
    public ActionResult<Profile> GetProfile(string id)
    {
      try
      {
        Profile profile = _accountService.GetProfileById(id);
        return Ok(profile);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // -----------------------------------------------------------------------------------------------------
    [HttpGet]
    // [Authorize]
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
    public async Task<ActionResult<IEnumerable<Vault>>> GetVaultByProfileAsync(string id)
    {
      try
      {

        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        // were passing userinfo as string because if null will fail and the userinfo? is checking if theres anythign there like a v-if
        IEnumerable<Vault> vaults = _vaultsService.GetVaultByProfile(id, userInfo?.Id);
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

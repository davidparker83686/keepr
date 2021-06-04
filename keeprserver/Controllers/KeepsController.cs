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
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _keepsService;
    private readonly AccountService _accountService;

    public KeepsController(KeepsService keepsService, AccountService accountService)
    {
      _keepsService = keepsService;
      _accountService = accountService;
    }
    // -----------------------------------------------------------------------------------------------------
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep newKeep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        // Account fullAccount = _accountService.GetOrCreateAccount(userInfo);
        newKeep.CreatorId = userInfo.Id;

        Keep keeps = _keepsService.Create(newKeep);
        //TODO[epic=Populate] adds the account to the new object as the creator
        keeps.Creator = userInfo;
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // -----------------------------------------------------------------------------------------------------
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
      try
      {
        IEnumerable<Keep> keeps = _keepsService.GetAll();
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // -----------------------------------------------------------------------------------------------------

    [HttpGet("{id}")]
    public ActionResult<Keep> GetById(int id)
    {
      try
      {
        Keep keeps = _keepsService.GetById(id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public ActionResult<Keep> Update(int id, [FromBody] Keep update)
    {
      try
      {
        update.Id = id;
        Keep updated = _keepsService.Update(update);
        return Ok("updated");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // -----------------------------------------------------------------------------------------------------
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _keepsService.Delete(id, userInfo.Id);
        return Ok("Deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}
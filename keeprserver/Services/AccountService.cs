
using System;
using keepr.Models;
using keepr.Repositories_;

namespace keepr.Services

{
  public class AccountService
  {
    private readonly AccountRepository _repo;
    public AccountService(AccountRepository repo)
    {
      _repo = repo;
    }

    // ________________________________________________________________________________________________________________________________________________
    internal string GetProfileEmailById(string id)
    {
      return _repo.GetById(id).Email;
    }
    // ________________________________________________________________________________________________________________________________________________
    internal Account GetProfileByEmail(string email)
    {
      return _repo.GetByEmail(email);
    }
    // ________________________________________________________________________________________________________________________________________________
    internal Account GetOrCreateProfile(Account userInfo)
    {
      Account profile = _repo.GetById(userInfo.Id);
      if (profile == null)
      {
        return _repo.Create(userInfo);
      }
      return profile;
    }

    internal Account GetOrCreateAccount(Account userInfo)
    {
      Account account = _repo.GetById(userInfo.Id);
      if (account == null)
      {
        return _repo.Create(userInfo);
      }
      return account;
    }

    // ________________________________________________________________________________________________________________________________________________

    internal Profile GetProfileById(string id)
    {
      return _repo.GetById(id);
    }
    // ________________________________________________________________________________________________________________________________________________
    internal Account Edit(Account editData, string userEmail)
    {
      Account original = GetProfileByEmail(userEmail);
      original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
      original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
      return _repo.Edit(original);
    }
  }
}
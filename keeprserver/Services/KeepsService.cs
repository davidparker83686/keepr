using System;
using System.Collections.Generic;
using keepr.Repositories_;
using keepr.Models;

namespace keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _keepsRepository;

    public KeepsService(KeepsRepository keepsRepository)
    {
      _keepsRepository = keepsRepository;
    }
    // -----------------------------------------------------------------------------------------------------
    internal IEnumerable<Keep> GetAll()
    {
      return _keepsRepository.GetAll();
    }
    // -----------------------------------------------------------------------------------------------------
    internal IEnumerable<Keep> GetByCreatorId(string id)
    {
      throw new NotImplementedException();
    }
    // -----------------------------------------------------------------------------------------------------
    internal Keep Create(Keep newKeep)
    {
      Keep keeps = _keepsRepository.Create(newKeep);
      return keeps;
    }
    // -----------------------------------------------------------------------------------------------------
    internal Keep GetById(int id)
    {
      Keep keep = _keepsRepository.GetById(id);
      if (keep == null)
      {
        throw new Exception("Invalid Keep Id");
      }
      return (Keep)keep;
    }

    internal void Delete(int apple, string id2)
    {
      Keep keep = GetById(apple);
      if (!_keepsRepository.Delete(apple))
      {
        throw new Exception("Something has gone terribly wrong");
      }
    }



    // -----------------------------------------------------------------------------------------------------
    // internal Keep Update(Keep update)
    // {
    //   Keep original = GetById(update.Id);
    //   original.Name = update.Name.Length > 0 ? update.Name : original.Name;
    //   original.Description = update.Description.Length > 0 ? update.Description : original.Description;
    //   original.Img = update.Img.Length > 0 ? update.Img : original.Img;
    //   original.Views = update.Views > 0 ? update.Views : original.Views;
    //   original.Shares = update.Shares > 0 ? update.Shares : original.Shares;
    //   original.Keeps = update.Keeps > 0 ? update.Keeps : original.Keeps;

    //   if (_keepsRepository.Update(original))
    //   {
    //     return original;
    //   }
    //   throw new Exception("Something Went Wrong???");
    // }
  }

}



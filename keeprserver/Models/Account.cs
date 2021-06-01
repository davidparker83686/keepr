using System;
using System.Collections.Generic;

namespace keepr.Models
{
  public class Account : Profile
  {
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
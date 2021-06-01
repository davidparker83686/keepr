using System;

namespace keepr.Models
{
  public class Vault
  {
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public string Name { get; set; }

    public string Img { get; set; }
    public string Description { get; set; }
    public bool IsPrivate { get; set; }




    // EXTRA STUFF 
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
using System.Data;
using Dapper;
using keeprserver.Models;

namespace keeprserver.Repositories_
{
  public class AccountRepository
  {
    private readonly IDbConnection _db;

    public AccountRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Account GetByEmail(string userEmail)
    {
      string sql = "SELECT * FROM Accounts WHERE email = @userEmail";
      return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
    }

    internal Account GetById(string id)
    {
      string sql = "SELECT * FROM Accounts WHERE id = @id";
      return _db.QueryFirstOrDefault<Account>(sql, new { id });
    }

    internal Account Create(Account newAccount)
    {
      string sql = @"
            INSERT INTO Accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
      _db.Execute(sql, newAccount);
      return newAccount;
    }

    internal Account Edit(Account update)
    {
      string sql = @"
            UPDATE Accounts
            SET 
              name = @Name,
              picture = @Picture
            WHERE id = @Id;";
      _db.Execute(sql, update);
      return update;
    }
  }
}
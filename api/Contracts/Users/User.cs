using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtScheduler.Contracts.Users;

[Table("users", Schema = "public")]
public class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("handler")]
    public string Handler { get; set; }
    
    [Column("password")]
    public string Password { get; set; }
    
    [Column("did")]
    public string Did { get; set; }
    
    [Column("inserted_at")]
    public DateTime InsertedAt { get; set; }

    public static User Create(string handler, string password, string did)
    {
        return new User()
        {
            Handler = handler,
            Password = password,
            Did = did
        };
    }
}
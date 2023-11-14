namespace Ibge.Domain.Entity;

public class User : BaseEntity
{
    public string Name { get; private set; } 
    public string Email { get; private set; }
    public string Password { get; private set; }
    public bool IsAdmin { get; private set; }

    public User(string name, string email, string password, bool isAdmin)
    {
        this.Name = name;
        this.Email = email;
        this.Password = password;
        this.IsAdmin = isAdmin;
    }
}

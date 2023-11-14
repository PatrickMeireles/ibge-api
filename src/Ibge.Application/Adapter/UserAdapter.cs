using Ibge.Domain.Command.User;
using Ibge.Domain.Entity;

namespace Ibge.Domain.Adapter;

public static class UserAdapter
{
    public static User CreateNewUser(CreateUserCommand user) =>
        new User(user.Name, user.Email, BCrypt.Net.BCrypt.HashPassword(user.Password), user.IsAdmin);
}

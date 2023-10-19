using BingoWebApp.Entities;
using BingoWebApp.Models;

namespace BingoWebApp.Interfaces
{
    public interface IUser
    {
         public Task<bool> Create(User user);
         public Task<bool> SignIn(Login login);
    }
}

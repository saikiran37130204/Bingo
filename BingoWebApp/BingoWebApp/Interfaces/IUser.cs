using BingoWebApp.Entities;
using BingoWebApp.Models;

namespace BingoWebApp.Interfaces
{
    public interface IUser
    {
        public Task<bool> Create(Registration registration);
        public Task<bool> SignIn(Login login);
        public bool SignOut();
        public Task<bool> InsertInToCart(int ProductId);
        public Task<User> ProfileDetails(int UserId);
        
    }
}

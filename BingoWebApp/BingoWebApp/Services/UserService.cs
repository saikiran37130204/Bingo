using BingoWebApp.Entities;
using BingoWebApp.Interfaces;
using BingoWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BingoWebApp.Services
{
    public class UserService : IUser
    {
        public readonly BingoDbContext _dbContext;
        public readonly ILogger<UserService> _logger;

        public UserService()
        {
        }

        public UserService(BingoDbContext dbContext, ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<bool> Create(User user)
        {
            try
            {
                if (user != null)
                {
                    user.CreatedAt = DateTime.Now;
                    user.Role = 1;
                    await _dbContext.Users.AddAsync(user);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
            }
            return false;


        }

        public async Task<bool> SignIn(Login login)
        {
            try
            {
                if (login != null)
                {
                    var customer = await _dbContext.Users
                        .Where(i => i.Username == login.Username)
                        .FirstAsync();

                    if (customer != null)
                    {

                        if (customer.Password == login.Password)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
            }
            return false;
        }


    }
}

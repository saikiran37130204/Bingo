using BingoWebApp.Entities;
using BingoWebApp.Interfaces;
using BingoWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BingoWebApp.Services
{
    public class UserService : IUser
    {
        public readonly BingoDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly ILogger<UserService> _logger;
        
        public UserService()
        {

        }

        public UserService(BingoDbContext dbContext, IHttpContextAccessor httpContextAccessor, ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async Task<bool> Create(Registration registration)
        {
            try
            {
                if (registration != null)
                {
                    if (registration.Password == registration.ConfirmPassword)
                    {
                        var user = new User 
                            {
                            Username = registration.Username,
                            Address = registration.Address,
                            Email = registration.Email,
                            Password = registration.Password,
                            PhoneNumber = registration.PhoneNumber,
                            Name = registration.Name,
                            CreatedAt = DateTime.Now,
                            Role=1

                        };
                        await _dbContext.Users.AddAsync(user);
                        await _dbContext.SaveChangesAsync();
                        return true;
                    }
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
                    _httpContextAccessor.HttpContext?.Session.SetInt32("UserId", customer.UserId);

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

        public bool SignOut()
        {
            _httpContextAccessor.HttpContext?.Session.Remove("UserId");
            return true;  
        }

        public async Task<bool> InsertInToCart(int productId)
        {
           var product = await _dbContext.Products.Where(i=>i.ProductId == productId).FirstOrDefaultAsync();
            var userId= _httpContextAccessor.HttpContext?.Session.GetInt32("UserId");
            if (!userId.HasValue && product == null)
            {
                return false;
            }
            else
            {
              await _dbContext.Carts.AddAsync(new Cart()
                {
                    UserId = userId,
                    ProductId = product?.ProductId,
                    CreatedDate = DateTime.Now,
                    Quantity = 1
                }) ;
                var result = await _dbContext.SaveChangesAsync();
                if (result!=0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<User> ProfileDetails(int userId)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);

                if (user != null)
                {
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                // Handle the exception or log it based on your application's needs
                _logger.LogError(ex, "Error occurred while retrieving user details.");
                return null; // Or return an empty result or handle the error accordingly
            }
        }
    }
}

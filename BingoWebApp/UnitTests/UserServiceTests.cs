using BingoWebApp.Entities;
using BingoWebApp.Interfaces;
using BingoWebApp.Models;
using BingoWebApp.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class UserServiceTests
    {
        /*private static DbContextOptions<BingoDbContext> dbContextOptions=new 
            DbContextOptionsBuilder<BingoDbContext>().UseInMemoryDatabase(databaseName:"Bingo").Options;
       
        public BingoDbContext? _dbContext;
        
        public  UserService? _user;
        //BingoDbContext? _dbContext=new BingoDbContext();

       
       
        [OneTimeSetUp]
        public void SetUp()
        {
            _user = new UserService();
            _dbContext = new BingoDbContext(dbContextOptions);
            _dbContext.Database.EnsureCreated();
            // _user = new UserService(_dbContext);

            SeedDatabase();
              //_user=new UserService();
           // _DbContext = new BingoDbContext();
        }
        [OneTimeSetUp]
        public void CleanUp()
        {
            _dbContext?.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            
            var users = new User
            {
                UserId = 1,
                Username = "K_Ram",
                Password = "Ram@123",
                Name = "Ram",
                Email = "Ram@123",
                PhoneNumber = "123456789",
                Role = 1,
                Address = "3500 n bonnie brae st",
                CreatedAt = DateTime.Now,
                Deliveries = { new Delivery { DeliveryId=1,DeliveryAddress="3500 n bonnie brae st"} },
                DriverLocations = { new DriverLocation { } },
                Orders = { new Order { } }
            };
            _dbContext?.AddRange(users);
            _dbContext?.SaveChanges();
        }

        [Test]
        public void SignInWithExistingUserDetails()
        {
            //Arrange
            
            var users = new Login() { Username = "K_Ram", Password = "Ram@123" };

            //Act
            var Login=_user?.SignIn(users);


            //Assert
            Assert.That(Login,Is.Not.Null);
            //Assert.That(Login, Is.EqualTo(false));
        }*/
    }
}
// <summary>
// <copyright file="UserServiceTest.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>
namespace Axity.Users.Test.Services
{
    /// <summary>
    /// Class ProjectServiceTest.
    /// </summary>
    [TestFixture]
    public class UserServiceTest : BaseTest
    {
        private IUsersService userService;
        private IUsersDao usersDao;
        private IMapper mapper;

        private DatabaseContext context;

        /// <summary>
        /// Init configuration.
        /// </summary>
        [OneTimeSetUp]
        public void Init()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            this.mapper = mapperConfiguration.CreateMapper();

            DbConnection connection = new SqliteConnection("Data Source=TempProject;Mode=Memory;Cache=Shared");
            connection.Open();
            var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseSqlite(connection).Options;

            this.context = new DatabaseContext(options);
            this.context.Database.EnsureDeleted();
            this.context.Database.EnsureCreated();

            this.context.Users.AddRange(this.GetAllUserModel());
            this.context.SaveChanges();

            this.usersDao = new UsersDao(this.context);
            this.userService = new UsersService(this.mapper, this.usersDao);
        }

        /// <summary>
        /// Method Validate GetAllAsync.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task ValidateGetAllAsync()
        {
            var response = await this.userService.GetAllAsync();

            ClassicAssert.IsNotNull(response);
            ClassicAssert.IsTrue(response.Any());
            ClassicAssert.AreEqual(8, response.Count());
        }

        /// <summary>
        /// Method Validate GetByIdAsync.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task ValidateGetByIdAsync()
        {
            int id = 1;
            var response = await this.userService.GetByIdAsync(id);

            ClassicAssert.IsNotNull(response);
            ClassicAssert.AreEqual(id, response.Id);
        }

        /// <summary>
        /// Method Validate InsertAsync.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task ValidateInsertAsync()
        {
            var user = "userToken";
            var request = new CreateUserDto()
            {
                Name = "User 10",
                UserName = "user10",
                Email = "user10@yopmail.com",
            };

            var response = await this.userService.InsertAsync(user, request);

            ClassicAssert.NotNull(response.Id);
            ClassicAssert.AreEqual(request.Name, response.Name);
            ClassicAssert.AreEqual(request.UserName, response.UserName);
            ClassicAssert.AreEqual(request.Email, response.Email);
        }

        /// <summary>
        /// Method Validate UpdateAsync.
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestCase(1)]
        public async Task ValidateUpdateAsync(int id)
        {
            var user = "userToken";
            var request = new UpdateUserDto()
            {
                Name = "Usuario Uno",
                UserName = "user1",
            };

            var before = await this.userService.GetByIdAsync(id);
            var response = await this.userService.UpdateAsync(id, user, request);

            ClassicAssert.AreEqual(before.Id, response.Id);
            ClassicAssert.AreEqual(request.Name, response.Name);
            ClassicAssert.AreEqual(request.UserName, response.UserName);
            ClassicAssert.AreNotEqual(before.Email, response.Email);
        }

        /// <summary>
        /// Method Validate DeleteAsync.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task ValidateDeleteAsync()
        {
            var id = 9;
            var before = await this.userService.GetByIdAsync(id);
            await this.userService.DeleteAsync(id);

            ClassicAssert.IsNotNull(before);
            ClassicAssert.ThrowsAsync<NotFoundException>(async () => await this.userService.GetByIdAsync(id));
        }
    }
}

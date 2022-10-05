namespace Notetaking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext dbContext;
        public UserController (UserDbContext dbContext){
            
            this.dbContext = dbContext;

        }
        [HttpPost]
        public IActionResult AddUser(User user){
            var user = new Models.DomainModels.User{
                Username = this.Username,
                Email = this.Email,
                Password = this.Password
            }
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return ok(user);
        }
    }
}
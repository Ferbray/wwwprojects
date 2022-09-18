using Microsoft.AspNetCore.Mvc;
using wdskills.WebApi.Service;
using wdskills.WebServer;
using wdskills.WebServer.Model;

namespace wdskills.WebApi.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UserController : ControllerBase
    {
        private readonly AppDbService _appDbService;
        private readonly DbValidationService _validationService;

        public UserController(
            AppDbService appDbService,
            DbValidationService validationService)
        {
            _appDbService = appDbService;
            _validationService = validationService;
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<ActionResult<User>> Get(string? login, string? password)
        {
            string answer = await _validationService.IsValidAuthModelAsync(login, password);
            if(answer == string.Empty)
            {
                User? findUser = await _appDbService.FindUserToLoginAsync(login);
                return Ok(findUser);
            }
            return NotFound(answer);
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<User>> Post(User user)
        {
            user.UserId = 0;
            user.RoleId = 1;
            string answer = await _validationService.IsValidRegModelAsync(user);
            if(answer == string.Empty)
            {
                await _appDbService.AddUserAsync(user);
                return Ok(user);
            }
            return NotFound(answer);
        }
    }
}

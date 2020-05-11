using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestOrAssess.UserIdentity.Core.Domain;
using QuestOrAssess.UserIdentity.Services;

namespace UserIdentity.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IGroupService _groupService;

        public UserController(IAuthenticationService authenticationService, IGroupService groupService)
        {
            _authenticationService = authenticationService;
            _groupService = groupService;
        }


        [HttpPost("Authenticate")]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var app = new Application()
            {
                ApplicationKey = Guid.NewGuid(),
                Description = "test App"
            };

            var AppResulkt = _groupService.AddNewApplication(app);
            var result = await _authenticationService.Login(userName, password);
            if(result.Item1)
                return Ok(new { token = result.Item2});

            return BadRequest(result.Item2);
        }

        [HttpGet]
        public string Get()
        {

            return "Hello there";
        }
    }
}
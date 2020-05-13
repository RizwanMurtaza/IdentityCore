using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestOrAssess.UserIdentity.Core.Domain;
using QuestOrAssess.UserIdentity.Services;
using QuestOrAssess.UserIdentity.Services.AppManagement;

namespace QuestOrAssess.UserIdentity.Api.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAppGroupService _groupService;
        public UserController(IAuthenticationService authenticationService, IAppGroupService groupService)
        {
            _authenticationService = authenticationService;
            _groupService = groupService;
        }


        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {

            //var app = new Application()
            //{
            //    ApplicationKey = Guid.NewGuid(),
            //    Description = "test App"
            //};

            //var AppResulkt = _groupService.AddNewApplication(app);

            //var result = await _authenticationService.Login(userName, password);

            //if(result.Item1)
            //    return Ok(new { token = result.Item2});

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {

            return Ok("Hello there");
        }
    }
}
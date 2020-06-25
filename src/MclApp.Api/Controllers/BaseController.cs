using Microsoft.AspNetCore.Mvc;
using UserIdentity.ViewModels.Authentication.Claims;

namespace MclApp.Api.Controllers
{
    public class BaseController : Controller
    {
        public TokenBreachUser BreachUser => ((BreachIdentity)User.Identity).User;

    }
}

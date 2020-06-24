using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.ViewModels.Authentication.Claims;

namespace UserIdentity.Api.Controllers
{
    public class BaseController : Controller
    {
        public TokenBreachUser BreachUser => ((BreachIdentity)User.Identity).User;

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Domain.Constants;
using WebAPI.Application.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize]
        [HttpGet]
        public object Get()
        {
            var user = userService.GetCurrentUser(User.Claims)!;

            return new
            {
                user.Name,
                user.Email,
                Role = Roles.GetLocalized(user.RoleNavigation.Name)
            };
        }
    }
}

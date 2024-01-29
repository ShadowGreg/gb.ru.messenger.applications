using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.AuthorizationModel;

namespace UserService.Controllers;

[ApiController]
[Route("[controller]")]
public class RestrictedController: ControllerBase {
    [HttpGet]
    [Route("Admins")]
    [Authorize(Roles = "Adminstrator")]
    public IActionResult AdminEndPoint() {
        var currentUser = GetCurrentUser();
        return Ok($"Hi you are an {currentUser.Role}");
    }

    [HttpGet]
    [Route("Users")]
    [Authorize(Roles = "Adminstrator, User")]
    public IActionResult UserEndPoint() {
        var currentUser = GetCurrentUser();
        return Ok($"Hi you are an {currentUser.Role}");
    }


    private UserModel GetCurrentUser() {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity != null) {
            var userClaims = identity.Claims;
            return new UserModel {
                Username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                Role = (UserRole)Enum.Parse(typeof(UserRole),
                    userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value)
            };
        }

        return null;
    }
}
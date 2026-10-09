using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIHelpDesk.API.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok(new
        {
            message = "You are authorized as an Admin!"
        });
    }
}
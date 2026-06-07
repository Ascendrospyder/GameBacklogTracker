

using System.Security.Claims;
using GameBacklogApi.Data;
using GameBacklogApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace GameBacklogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly AppDbContext _context;

  public AuthController(AppDbContext context)
  {
    _context = context;
  }

  [HttpGet("login")]
  public IActionResult Login()
  {
    return Challenge(new AuthenticationProperties { RedirectUri = "/api/auth/validate" }, "Steam");
  }

  [HttpGet("validate")]
  public IActionResult Validate()
  {
    Console.WriteLine(User.Identity);
    // When steam redirects back, middleware will validate it
    // and will populate the User.Identity item with steam data.
    if (!User.Identity?.IsAuthenticated ?? true)
    {
      return Unauthorized("Steam authentication has failed.");
    }

    var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var steamId = claim?.Split('/').Last();
    var username = User.FindFirst(ClaimTypes.Name)?.Value;

    if (string.IsNullOrEmpty(steamId)) return BadRequest("Could not pase Steam ID.");

    var existingUser = _context.Users.FirstOrDefault(u => u.SteamId == steamId);
    if (existingUser == null)
    {
      // First time login! Let's register them.
      existingUser = new User
      {
        Id = Guid.NewGuid(),
        SteamId = steamId,
        Username = username ?? "Unknown Steam User"
      };
      _context.Users.Add(existingUser);
      _context.SaveChanges();
    }

    return Ok(new
    {

      Message = "Successfully logged in via Steam.",
      SteamId = existingUser.SteamId,
      username = existingUser.Username
    });
  }

}
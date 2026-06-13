
using System.Diagnostics;
using System.Security.Claims;
using GameBacklogApi.Data;
using GameBacklogApi.DTOs;
using GameBacklogApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameBacklogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly AppDbContext _context;
  private readonly string _frontendUrl;

  public AuthController(AppDbContext context, IConfiguration configuration)
  {
    _context = context;
    _frontendUrl = configuration["Frontend:Url"] ?? "http://localhost:5173";
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
    Debug.WriteLine($"User object = {User}");

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

    return Redirect($"{_frontendUrl}/");
  }

  [Authorize]
  [HttpGet("me")]
  public IActionResult Me()
  {
    var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var steamId = claim?.Split('/').Last();

    if (string.IsNullOrEmpty(steamId))
    {
      return Unauthorized();
    }

    var user = _context.Users.FirstOrDefault(u => u.SteamId == steamId);
    if (user == null)
    {
      return Unauthorized();
    }

    return Ok(new UserDto
    {
      Id = user.Id,
      SteamId = user.SteamId,
      Username = user.Username
    });
  }

}
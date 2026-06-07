using GameBacklogApi.Data;
using GameBacklogApi.DTOs;
using GameBacklogApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameBacklogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
  private readonly AppDbContext _context;

  public GamesController(AppDbContext context)
  {
    _context = context;
  }

  // Http Post endpoint to create a game.
  [HttpPost]
  public async Task<IActionResult> CreateGame([FromBody] GameCreateDto request)
  {
    var gameOfInterest = _context.Games.FirstOrDefault(g => g.Title == request.Title);
    if (gameOfInterest != null)
    {
      return Conflict("This game already exists in the database.");
    }

    var newGame = new Game
    {
      Id = Guid.NewGuid(),
      Title = request.Title,
      SteamAppId = request.SteamAppId,
      CoverArtUrl = request.CoverArtUrl
    };

    _context.Games.Add(newGame);

    await _context.SaveChangesAsync();

    return Created($"/api/games/{newGame.Id}", newGame);
  }

  [HttpGet]
  public IActionResult GetAllGames()
  {
    var games = _context.Games.ToList();
    return Ok(games);
  }
}
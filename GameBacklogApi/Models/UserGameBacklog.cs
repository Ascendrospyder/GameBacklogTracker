
using System;

/**
 * 
 * The following class will be the join table connecting a User to a game. This will be useful to track
 * progress.
 *
*/
namespace GameBacklogApi.Models;

public enum GameStatus
{
    Backlog,
    Playing,
    Completed,
    Abandoned
}

public class UserGameBacklog
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid GameId { get; set; }
    public Game Game { get; set; } = null!;

    public GameStatus Status { get; set; }
    public int PlaytimeMinutes { get; set; }
    public int UserRating { get; set; }
}
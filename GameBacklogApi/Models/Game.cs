using System;
using System.Collections.Generic;

namespace GameBacklogApi.Models;

public class Game
{
    public Guid Id { get; set; }
    public int? SteamAppId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string CoverArtUrl { get; set; } = string.Empty;

    public ICollection<UserGameBacklog> BacklogItems { get; set; } = new List<UserGameBacklog>();
}
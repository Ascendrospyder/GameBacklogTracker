using System;
using System.Collections.Generic;

namespace GameBacklogApi.Models;

public class User
{
    public Guid Id { get; set; }
    public string SteamId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    public ICollection<UserGameBacklog> BacklogItems { get; set; } = new List<UserGameBacklog>();
}
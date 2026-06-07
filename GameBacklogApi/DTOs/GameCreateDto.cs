namespace GameBacklogApi.DTOs;

public class GameCreateDto
{
  public string Title { get; set; } = string.Empty;
  public int? SteamAppId { get; set; }
  public string CoverArtUrl { get; set;} = string.Empty;
}
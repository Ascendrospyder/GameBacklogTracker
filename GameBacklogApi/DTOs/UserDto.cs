namespace GameBacklogApi.DTOs;

public class UserDto
{
  public Guid Id { get; set; }
  public string SteamId { get; set; } = string.Empty;
  public string Username { get; set; } = string.Empty;
}

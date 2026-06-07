# GameBacklogTracker

GameBacklogTracker is a small ASP.NET Core Web API project for tracking a personal game backlog while learning .NET. The repository currently focuses on the backend domain model and API surface: games, users, and a join table that tracks a user’s relationship to a game, including status, playtime, and rating.

## What’s In The Project

The app is built around a simple backlog workflow:

- `Game` stores the game title, optional Steam app ID, and cover art URL.
- `User` stores a Steam ID and display name.
- `UserGameBacklog` links a user to a game and tracks backlog state with `Backlog`, `Playing`, `Completed`, or `Abandoned`.

The API currently includes:

- `GET /weatherforecast` from the default ASP.NET template.
- `GET /api/games` to list all games.
- `POST /api/games` to create a new game.

SQLite is used for persistence through Entity Framework Core.

## Tech Stack

- .NET 10 Web API
- ASP.NET Core
- Entity Framework Core
- SQLite
- OpenAPI support in development
- Docker

## Project Structure

- `GameBacklogApi/Program.cs` configures the web app and registers the SQLite DbContext.
- `GameBacklogApi/Controllers/GamesController.cs` contains the games API endpoints.
- `GameBacklogApi/Data/AppDbContext.cs` defines the EF Core database context.
- `GameBacklogApi/Models/` contains the entity models.
- `GameBacklogApi/DTOs/` contains request DTOs.
- `GameBacklogApi/Migrations/` contains the initial EF Core migration.

## Requirements

- .NET 10 SDK
- Optional: Docker Desktop if you want to run the API in a container

## Run Locally

1. Open a terminal in `GameBacklogApi`.
2. Restore packages:

```bash
dotnet restore
```

3. Run the API:

```bash
dotnet run
```

The launch settings are configured to use:

- `http://localhost:5205`
- `https://localhost:7272`

## Database

The connection string points to a local SQLite file at `data/gamebacklog.db`.

If the database does not already exist, EF Core will create it when the app starts and migrations are applied. Make sure the `data` folder exists under `GameBacklogApi` before running the app the first time.

If you need to create or update the database manually, use EF Core commands from the `GameBacklogApi` folder:

```bash
dotnet ef database update
```

If the EF tool is not installed, you can add it with:

```bash
dotnet tool install --global dotnet-ef
```

## API Endpoints

### Games

`GET /api/games`

Returns all games stored in the database.

`POST /api/games`

Creates a new game.

Example request body:

```json
{
	"title": "Hades",
	"steamAppId": 1145360,
	"coverArtUrl": "https://example.com/hades.jpg"
}
```

If a game with the same title already exists, the API returns a conflict response.

### Default Template Endpoint

`GET /weatherforecast`

This endpoint comes from the default ASP.NET template and is still present in the current app.

## Docker

The repository includes a multi-stage Dockerfile for building and running the API in a container.

Build the image from the `GameBacklogApi` folder:

```bash
docker build -t gamebacklogapi .
```

Run the container:

```bash
docker run --rm -p 8080:8080 gamebacklogapi
```

## Data Model

The initial migration creates three tables:

- `Games`
- `Users`
- `UserGameBacklogs`

`UserGameBacklogs` uses a composite primary key made up of `UserId` and `GameId`, which lets the app track a user’s status for a specific game without duplicate rows.

## Learning Goals

This project is intentionally small and practical. It is a good starting point for learning:

- ASP.NET Core Web API structure
- Entity Framework Core with SQLite
- DTOs and controllers
- Relational modeling with join tables
- Containerizing a .NET app with Docker

## Notes

- The app currently uses a minimal API startup style with a controller-based endpoint for games.
- There is no frontend checked into this repository yet; the current scope is the API and persistence layer.
- The default weather forecast sample route is still in place and can be removed later if you want a cleaner API surface.

## Next Steps

If you want to keep building this out, the next natural additions would be user CRUD endpoints, backlog item endpoints, authentication, and a frontend that talks to this API.
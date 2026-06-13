# Game Backlog Tracker

Track your Steam game backlog — what's queued, what you're playing, and what you've finished. A React frontend talks to an ASP.NET Core API backed by SQLite.

## Features

Work in progress. See [docs/USER_STORIES.md](docs/USER_STORIES.md) for the full roadmap.

### Implemented

| Area | What's working |
|------|----------------|
| **Auth (US-1.1)** | Sign in with Steam via OpenID; new users are created automatically; redirect back to the app after login |
| **Session (US-1.2, partial)** | `GET /api/auth/me` returns the logged-in user; home page shows your Steam username |
| **Games API** | `GET /api/games` and `POST /api/games` for listing and manually creating games |
| **Data model** | `User`, `Game`, and `UserGameBacklog` with statuses: Backlog, Playing, Completed, Abandoned |
| **Frontend shell (US-6.1, partial)** | Login page, authenticated home page, loading states |
| **API client (US-6.2, partial)** | Shared auth fetch layer with cookie credentials via `VITE_API_URL` |
| **UI** | Ant Design components, light/dark theme toggle |
| **Dev tooling** | Docker Compose runs API + Vite together with hot reload |

### Not yet implemented

- Sign out (US-1.3)
- Protected backlog endpoints (US-1.4, US-3.x)
- Steam library import and playtime sync (US-2.1, US-2.2)
- Backlog browsing, status changes, ratings, and game cards (US-3.x, US-6.3)
- Stats, filters, and "pick something to play" (US-4.x, US-5.x)

## Tech stack

| Layer | Tools |
|-------|-------|
| **Frontend** | React 19, TypeScript, Vite, Ant Design |
| **Backend** | .NET 10, ASP.NET Core Web API |
| **Database** | SQLite via Entity Framework Core |
| **Auth** | Steam OpenID, cookie-based sessions |
| **Dev ops** | Docker, Docker Compose |

## Prerequisites

**Docker (recommended)**

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) with Compose v2
- A [Steam Web API key](https://steamcommunity.com/dev/apikey)

**Local development (without Docker)**

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js 22+](https://nodejs.org/)
- Steam Web API key

## Quick start (Docker)

From the repository root:

1. Create a `.env` file for the Steam API key:

```env
STEAM_API_KEY=your_steam_web_api_key
```

2. Ensure the SQLite data directory exists:

```bash
mkdir GameBacklogApi/data
```

3. Build and start both services:

```bash
docker compose up --build
```

Note: `--build` goes **after** `up`, not before.

4. Open the app:

| Service | URL |
|---------|-----|
| Frontend | http://localhost:5173 |
| API | http://localhost:8080 |

Stop with `Ctrl+C`, or run detached with `docker compose up -d`.

Rebuild after dependency changes:

```bash
docker compose build
docker compose up
```

## Local development (without Docker)

Run the API and frontend in separate terminals.

### API

```bash
cd GameBacklogApi
dotnet restore
dotnet user-secrets set "Steam:ApiKey" "your_steam_web_api_key"
dotnet run --launch-profile https
```

The API listens on:

- https://localhost:7272
- http://localhost:5205

Migrations run automatically on startup. The SQLite database is stored at `GameBacklogApi/data/gamebacklog.db`.

### Frontend

```bash
cd Gamebacklog-Ui
npm install
cp .env.example .env
npm run dev
```

For local (non-Docker) development, set the API URL in `.env`:

```env
VITE_API_URL=https://localhost:7272
```

The frontend dev server runs at http://localhost:5173.

## Environment variables

| Variable | Used by | Description |
|----------|---------|-------------|
| `STEAM_API_KEY` | Docker Compose → API | Steam Web API key (`Steam__ApiKey`) |
| `VITE_API_URL` | Frontend | Base URL for API requests (browser-facing) |
| `Frontend__Url` | API (Docker) | CORS origin and post-login redirect (default `http://localhost:5173`) |

**Docker:** set `STEAM_API_KEY` in a root `.env` file.

**Local API:** use `dotnet user-secrets set "Steam:ApiKey" "..."` or export `Steam__ApiKey`.

Never commit API keys or `.env` files with secrets.

## Project structure

```
GameBacklogTracker/
├── docker-compose.yml          # Dev orchestration (API + frontend)
├── GameBacklogApi/             # ASP.NET Core Web API
│   ├── Controllers/            # Auth, games
│   ├── Data/                   # EF Core DbContext + SQLite file
│   ├── Models/                 # User, Game, UserGameBacklog
│   ├── DTOs/
│   ├── Migrations/
│   └── Dockerfile
├── Gamebacklog-Ui/             # React + Vite frontend
│   ├── src/
│   │   ├── api/                # API client (auth)
│   │   ├── pages/              # Login, home
│   │   └── components/         # Steam sign-in, theme toggle
│   └── Dockerfile.dev
└── docs/
    └── USER_STORIES.md         # Feature backlog and acceptance criteria
```

## API endpoints

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| `GET` | `/api/auth/login` | No | Start Steam OpenID login |
| `GET` | `/api/auth/validate` | No | Complete login, create user if new |
| `GET` | `/api/auth/me` | Yes | Return current user |
| `GET` | `/api/games` | No | List all games |
| `POST` | `/api/games` | No | Create a game manually |

OpenAPI is available in Development at `/openapi/v1.json` when running the API locally with HTTPS.

### Example: create a game

```bash
curl -X POST http://localhost:8080/api/games \
  -H "Content-Type: application/json" \
  -d '{"title":"Hades","steamAppId":1145360,"coverArtUrl":"https://example.com/hades.jpg"}'
```

## Database

SQLite file: `GameBacklogApi/data/gamebacklog.db`

Tables: `Games`, `Users`, `UserGameBacklogs` (composite key on `UserId` + `GameId`).

To apply migrations manually:

```bash
cd GameBacklogApi
dotnet ef database update
```

Install the EF CLI if needed:

```bash
dotnet tool install --global dotnet-ef
```

## Roadmap

The next MVP milestones from [docs/USER_STORIES.md](docs/USER_STORIES.md):

1. **Auth polish** — sign out, protect backlog routes (US-1.3, US-1.4)
2. **Steam import** — pull owned games and playtime (US-2.1, US-2.2)
3. **Backlog CRUD** — add, update status, browse by status (US-3.1, US-3.2, US-3.5)
4. **UI wiring** — game cards, import button, error states (US-6.2, US-6.3)

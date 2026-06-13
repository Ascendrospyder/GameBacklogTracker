# Game Backlog Tracker — User Stories

Track your Steam games: what's in the backlog, what you're playing, what you've finished. Vite + React on the front, ASP.NET + SQLite on the back.

---

## Where we're at

| Area | Status |
|------|--------|
| Steam login (`/api/auth/login`, `/api/auth/validate`) | Done |
| `User`, `Game`, `UserGameBacklog` models | Done |
| Statuses: Backlog, Playing, Completed, Abandoned | Done |
| `GET` / `POST /api/games` | Done |
| Backlog endpoints | Not started |
| Pulling games from Steam | Not started |
| UI hooked up to the API | Not started |

---

## MVP — ship this first

1. **US-1.1**, **US-1.3**, **US-1.4** — login works, routes are protected
2. **US-2.1**, **US-2.2** — pull your Steam library and playtime
3. **US-3.1**, **US-3.2**, **US-3.5** — add games, change status, browse by status
4. **US-6.1**, **US-6.2**, **US-6.3** — real UI wired to the API

---

## Auth

### US-1.1 — Sign in with Steam

Log in with Steam so your backlog actually matches your account.

**Done when:**

- [ ] "Sign in with Steam" hits `GET /api/auth/login` and kicks off OpenID
- [ ] On success we find or create a `User` by `SteamId`
- [ ] You land back in the Vite app, logged in
- [ ] If auth fails, you see an error — not a blank screen
- [ ] Refreshing the page keeps you logged in until you sign out

---

### US-1.2 — See who's logged in

Know at a glance that you're on the right account.

**Done when:**

- [ ] Header shows your `Username`
- [ ] Nice-to-have: pull your avatar from the Steam Web API
- [ ] Profile page shows your Steam ID (read-only) and when you first signed up

---

### US-1.3 — Sign out

Log out when you're on a shared PC or switching accounts.

**Done when:**

- [ ] Sign out clears the session cookie
- [ ] Hitting a protected page while logged out sends you to login
- [ ] After logout, nothing personal is still on screen

---

### US-1.4 — Lock down the API

Backlog data should only belong to the person who owns it.

**Done when:**

- [ ] Backlog and user endpoints require auth (`[Authorize]`)
- [ ] `GET /api/auth/me` tells the frontend who's logged in
- [ ] No cookie → `401`
- [ ] You can't peek at or edit someone else's backlog

---

## Steam library

### US-2.1 — Import my Steam games

One button to pull in everything you own on Steam. No typing hundreds of titles.

**Done when:**

- [ ] Backend calls Steam's `GetOwnedGames` with your `SteamId`
- [ ] Each game gets a `Game` row (`SteamAppId`, title, cover art)
- [ ] You get a `UserGameBacklog` entry per game, defaulting to `Backlog`
- [ ] Running import again doesn't create duplicates — it updates what's already there
- [ ] UI tells you when it's done (e.g. "142 games imported")

---

### US-2.2 — Keep playtime up to date

Playtime should come from Steam, not guesswork.

**Done when:**

- [ ] `PlaytimeMinutes` comes from Steam's `playtime_forever`
- [ ] "Sync" refreshes hours without duplicating games
- [ ] UI shows when you last synced
- [ ] Private Steam profile → clear message about why import failed

---

### US-2.3 — Search the Steam store

Add games you don't own yet — wishlist stuff, demos, whatever.

**Done when:**

- [ ] Search by name returns app ID, title, and header image
- [ ] You can add a result to your backlog even if you don't own it
- [ ] Manual add via `GameCreateDto` still works (title, `SteamAppId`, `CoverArtUrl`)

---

### US-2.4 — Game details

Enough info on a game to decide if tonight's the night.

**Done when:**

- [ ] Card shows cover, title, playtime, status, rating
- [ ] Detail page links to `store.steampowered.com/app/{id}`
- [ ] Nice-to-have: genres, release date, tags from the Store API

---

## Backlog (the main thing)

`UserGameBacklog` is the join table — one row per user per game, with status, playtime, and rating.

### US-3.1 — Add a game

Put something on the list.

**Done when:**

- [ ] `POST /api/backlog` creates a row with status `Backlog`
- [ ] Same game twice → rejected (composite key on `UserId` + `GameId`)
- [ ] UI shows the new game in the list right away

---

### US-3.2 — Move games between statuses

Backlog → Playing → Completed → Abandoned. The whole point of the app.

**Done when:**

- [ ] `PATCH /api/backlog/{gameId}` updates `GameStatus`
- [ ] Change it in the UI — dropdown is fine, drag-and-drop is a bonus
- [ ] Saves immediately, no full page reload
- [ ] Lists and filters show the new status

---

### US-3.3 — Rate games

Remember which ones were actually good.

**Done when:**

- [ ] `UserRating` saved per user per game (1–5 stars or 1–10, pick one)
- [ ] No rating yet → show "—", not a broken empty state
- [ ] Optional: nudge for a rating when you mark something `Completed`

---

### US-3.4 — Remove from backlog

Get rid of accidents, or games you've given up on tracking.

**Done when:**

- [ ] `DELETE /api/backlog/{gameId}` drops your row only
- [ ] The `Game` itself sticks around if anyone else has it
- [ ] Asks "are you sure?" before deleting

---

### US-3.5 — Browse by status

See your whole pipeline — what's queued, what's active, what's done.

**Done when:**

- [ ] Separate views for Backlog, Playing, Completed, Abandoned
- [ ] Count on each tab (e.g. "Backlog (47)")
- [ ] Empty tab tells you what to do ("Import from Steam" / "Add a game")

---

## Finding what to play

### US-4.1 — Sort and filter

When you have 200 games in the backlog, you need help narrowing it down.

**Done when:**

- [ ] Sort by title, playtime, rating, date added, or status
- [ ] Filter by status, playtime, rated/unrated, or search box
- [ ] Your filters stick around (URL params or local storage)

---

### US-4.2 — Pick something for me

Staring at the backlog for twenty minutes is not playing games.

**Done when:**

- [ ] Picks from `Backlog` or `Playing` — random, shortest playtime, or highest-rated unfinished
- [ ] "Start playing" flips it to `Playing` in one click

---

### US-4.3 — Priority / tags

Flag the games you actually care about getting to.

**Done when:**

- [ ] Priority or tags on `UserGameBacklog`
- [ ] Filter and sort by them
- [ ] Something visible on the card (pin, icon, whatever)

---

## Stats

### US-5.1 — Dashboard

Quick snapshot of how you're doing.

**Done when:**

- [ ] Counts: total, completed, playing, abandoned, backlog
- [ ] Total hours across all games
- [ ] Some kind of completion % (completed vs. everything you're tracking)

---

### US-5.2 — When did I finish things?

Good for seeing if you're actually making progress.

**Done when:**

- [ ] `CompletedAt` set when status goes to `Completed`
- [ ] "Recently completed" on the dashboard
- [ ] Nice-to-have: chart of finishes per month

---

### US-5.3 — Weekly playtime goal

Stretch goal — hold yourself to a hours-per-week target.

**Done when:**

- [ ] Set a weekly hours goal
- [ ] Progress bar based on Steam playtime since last sync
- [ ] Soft nudge if you're falling behind (not annoying)

---

## Frontend

### US-6.1 — Navigation and layout

Get around without getting lost.

**Done when:**

- [ ] Routes: `/`, `/backlog`, `/library`, `/stats`, `/login`
- [ ] Works on phone and desktop
- [ ] Spinners while loading, something useful when things break

---

### US-6.2 — Talk to the API

One place for all the fetch logic so we're not copy-pasting `fetch` everywhere.

**Done when:**

- [ ] `VITE_API_URL` or Vite proxy pointing at the API
- [ ] `credentials: 'include'` on requests (cookie auth)
- [ ] TS types that match the DTOs (`Game`, `UserGameBacklog`, `GameStatus`)
- [ ] `401` → send them to login

---

### US-6.3 — Game cards

The backlog should look like a game library, not a spreadsheet.

**Done when:**

- [ ] Card: cover, title, status, playtime, rating
- [ ] Skeleton placeholders while stuff loads
- [ ] Keyboard nav works, images have alt text, focus states are visible

---

## API — what's built vs. what's missing

| Endpoint | What it does | Status |
|----------|--------------|--------|
| `GET /api/auth/login` | Start Steam login | Done |
| `GET /api/auth/validate` | Finish login, create user if new | Done |
| `GET /api/auth/me` | Who am I? | Needed |
| `POST /api/auth/logout` | Sign out | Needed |
| `GET /api/games` | List games | Done |
| `POST /api/games` | Add a game manually | Done |
| `GET /api/backlog` | My backlog | Needed |
| `POST /api/backlog` | Add to my backlog | Needed |
| `PATCH /api/backlog/{gameId}` | Update status, rating, notes | Needed |
| `DELETE /api/backlog/{gameId}` | Remove from my backlog | Needed |
| `POST /api/steam/import` | Pull owned games from Steam | Needed |
| `POST /api/steam/sync` | Refresh playtime | Needed |

**Also on the API todo:**

- [ ] DTOs for requests/responses — don't leak EF entities straight to the client
- [ ] CORS for `http://localhost:5173` with credentials
- [ ] Validate inputs (required fields, rating in range, etc.)
- [ ] Errors in a consistent shape: `{ "error": "...", "code": "..." }`
- [ ] Pagination once someone's library gets huge

---

## Schema — stuff worth adding

| Field | Where | Why |
|-------|-------|-----|
| `AddedAt` | `UserGameBacklog` | "when did I add this?" |
| `CompletedAt` | `UserGameBacklog` | completion history |
| `Notes` | `UserGameBacklog` | "finish before sequel comes out" |
| `LastSyncedAt` | `UserGameBacklog` or `User` | show sync freshness |
| `IsOwned` | `UserGameBacklog` | owned vs. wishlist |
| Unique `SteamAppId` | `Game` | no duplicate Steam entries |

---

## Dev setup, security, tests

### Local dev

- [ ] Launch config runs API + Vite together
- [ ] Docs mention you need a `Steam:ApiKey`
- [ ] README: .NET version, Node version, env vars, first run
- [ ] `.env.example` for the frontend API URL

### Secrets and prod config

- [ ] Steam API key in user secrets / env — not in git
- [ ] Callback and redirect URLs configurable (stop hardcoding localhost)
- [ ] Prod: HTTPS, `HttpOnly` + `SameSite` on cookies

### Tests worth writing

- [ ] Backlog CRUD integration tests (test DB)
- [ ] Auth smoke test with fake Steam claims
- [ ] Component tests: status filter, game card
- [ ] E2E: login → import → change a status

---

## Phases

### Phase 1 — MVP

**Backend**

- [ ] `GET /api/auth/me`, `POST /api/auth/logout`
- [ ] Full `/api/backlog` CRUD
- [ ] `POST /api/steam/import`
- [ ] CORS + cookies working with Vite
- [ ] Fix redirect URL — Vite is on `5173`, not `3000`

**Frontend**

- [ ] Ditch the Vite starter; build login + backlog view
- [ ] Fetch with credentials
- [ ] Status dropdown on cards
- [ ] "Import from Steam" button
- [ ] Handle empty, loading, and error states

**Data**

- [ ] `AddedAt` on `UserGameBacklog`
- [ ] Unique `SteamAppId` on `Game`

**Polish**

- [ ] Grid looks okay on mobile
- [ ] Simple counts: backlog / playing / completed

---

### Phase 2

- [ ] Steam store search (US-2.3)
- [ ] Ratings + remove from backlog (US-3.3, US-3.4)
- [ ] Sort, filter, "pick for me" (US-4.1, US-4.2)
- [ ] Dashboard + completion history (US-5.1, US-5.2)
- [ ] Game detail page (US-2.4)
- [ ] Pagination when libraries get big

---

### Phase 3 — if we still feel like it

- [ ] Tags, priority, custom lists
- [ ] Weekly playtime goals
- [ ] Export backlog (JSON / CSV)
- [ ] Dark mode
- [ ] Year-over-year completions
- [ ] Steam achievements (?)

---

## Adding a new story

Copy-paste this into an issue:

```markdown
**ID:** US-3.2
**Title:** Change game status
**Priority:** P0 — MVP
**Estimate:** ~3 hrs

**What:**
Logged-in users can move a game between Backlog, Playing, Completed, and Abandoned.

**Done when:**
- [ ] `PATCH /api/backlog/{gameId}` takes `{ status }`
- [ ] Can't update someone else's game
- [ ] UI updates without reloading the page

**Notes:**
- `GameStatus` enum on `UserGameBacklog`
- Return the updated row with game title + cover

**Blocked by:** US-1.4, US-3.1
```

---

## Loose ends in the codebase

| What | Right now | Do this next |
|------|-----------|--------------|
| Auth | Steam login works, new users get created | US-1.4, add `/api/auth/me` |
| Games API | Anyone can list/create games | Tie it to the logged-in user's backlog |
| Backlog | Model exists, no controller | US-3.1–3.5 |
| Steam | Not wired up | US-2.1 |
| Frontend | Default Vite template | US-6.1–6.3 |
| Redirect | `AuthController` sends you to `localhost:3000` | Point at Vite on `5173` |

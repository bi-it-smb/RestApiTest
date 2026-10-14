# RestApiTest

A small .NET 10 sample application that demonstrates a game-themed REST API and a Blazor interactive UI. The solution contains two web projects:

- **RestApiTest** — a minimal ASP.NET Core API for managing players and simulating combat.
- **GameUi** — a Blazor Server-style interactive UI that consumes the API.

> **Status:** This is a learning/demo project. API data is stored in memory and is reset whenever the API process restarts.

## Features

- List, create, retrieve, and delete players
- Heal a player by a random amount
- Simulate a fight against a random enemy
- Award experience and gold after combat
- Automatically level up players when enough experience is earned
- Interactive Blazor UI for working with the API
- Nullable reference types and implicit usings enabled

## Technology

- .NET 10
- ASP.NET Core Minimal APIs
- Blazor interactive server components
- C#
- HTML, CSS, and JavaScript

## Project structure

```text
.
├── GameUi/
│   ├── Components/       # Blazor UI components
│   ├── Models/           # UI models
│   ├── wwwroot/           # Static assets and styles
│   ├── Program.cs        # UI startup and API HttpClient configuration
│   └── GameUi.csproj
├── RestApiTest/
│   ├── Models/           # Player and enemy models
│   ├── Program.cs        # Minimal API endpoints and game logic
│   └── RestApiTest.csproj
└── RestApiTest.slnx
```

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- A terminal, IDE, or editor that supports .NET development

Verify your installed SDK with:

```bash
dotnet --version
```

## Getting started

Clone the repository and move into its directory:

```bash
git clone https://github.com/bi-it-smb/RestApiTest.git
cd RestApiTest
```

Restore dependencies and build the solution:

```bash
dotnet restore RestApiTest.slnx
dotnet build RestApiTest.slnx
```

### Run the API

Start the API from the repository root:

```bash
dotnet run --project RestApiTest/RestApiTest.csproj
```

The configured development URLs are:

- HTTP: `http://localhost:5165`
- HTTPS: `https://localhost:7038`

### Run the UI

In a second terminal, start the Blazor UI:

```bash
dotnet run --project GameUi/GameUi.csproj
```

The configured development URLs are:

- HTTP: `http://localhost:5084`
- HTTPS: `https://localhost:7051`

The UI is configured to call the API at `http://localhost:5165/`. Start the API before using features in the UI.

## API endpoints

The API uses JSON request and response bodies.

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/` | Returns a basic health/message response. |
| `GET` | `/players` | Returns all players. |
| `GET` | `/players/{id}` | Returns one player by ID. |
| `POST` | `/players` | Creates a player and assigns the next available ID. |
| `POST` | `/players/{id}/heal` | Restores a random amount of health, up to the maximum of 100. |
| `POST` | `/players/{id}/fight` | Selects a random enemy, applies damage, and awards experience and gold. |
| `DELETE` | `/players/{id}` | Removes a player. |

### Example requests

List players:

```bash
curl http://localhost:5165/players
```

Create a player:

```bash
curl -X POST http://localhost:5165/players \\
  -H "Content-Type: application/json" \\
  -d '{
    "name": "Aria",
    "class": "Mage",
    "level": 1,
    "experience": 0,
    "health": 100,
    "gold": 50
  }'
```

Heal player 1:

```bash
curl -X POST http://localhost:5165/players/1/heal
```

Start a fight for player 1:

```bash
curl -X POST http://localhost:5165/players/1/fight
```

Delete player 1:

```bash
curl -X DELETE http://localhost:5165/players/1
```

## Game rules in the sample API

- Players start with 100 health, level 1, and 50 gold by default.
- Each fight selects one of three enemies: Goblin, Orc, or Dragon.
- Damage taken is randomly selected between half of the enemy's damage and its full damage.
- A fight awards the enemy's experience and gold rewards.
- Every 100 experience points increases the player's level, subtracts 100 experience, and restores health to 100.
- Healing restores between 10 and 30 health points and never raises health above 100.

## Development notes

- The API currently uses in-memory lists instead of a database.
- There is no authentication or authorization configured.
- Because the UI uses a fixed API base address, update `GameUi/Program.cs` if the API runs on a different port or host.
- The HTTPS launch profiles use the local ASP.NET Core development certificate. If HTTPS is not trusted on your machine, use the HTTP profiles or trust the certificate with:

```bash
dotnet dev-certs https --trust
```

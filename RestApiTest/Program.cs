using RestApiTest.Models;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Player> players =
[
    new Player
    {
        Id = 1,
        Name = "Benjamin",
        Class = "Warrior",
        Level = 1,
        Experience = 0,
        Health = 100,
        Gold = 50
    }
];

List<Enemy> enemies =
[
    new Enemy
    {
        Name = "Goblin",
        Health = 50,
        Damage = 10,
        ExperienceReward = 25,
        GoldReward = 10
    },

    new Enemy
    {
        Name = "Orc",
        Health = 100,
        Damage = 20,
        ExperienceReward = 50,
        GoldReward = 25
    },

    new Enemy
    {
        Name = "Dragon",
        Health = 250,
        Damage = 40,
        ExperienceReward = 150,
        GoldReward = 100
    }
];

app.MapGet("/players", () =>
{
    return Results.Ok(players);
});

app.MapGet("/players/{id}", (int id) =>
{
    var player = players.FirstOrDefault(p => p.Id == id);

    if (player == null)
    {
        return Results.NotFound("Spieler nicht gefunden.");
    }

    return Results.Ok(player);
});

app.MapPost("/players", (Player player) =>
{
    player.Id = players.Max(p => p.Id) + 1;
    players.Add(player);

    return Results.Created($"/players/{player.Id}", player);
});

app.MapPost("/players/{id}/heal", (int id) =>
{
    var player = players.FirstOrDefault(p => p.Id == id);

    if (player == null)
    {
        return Results.NotFound();
    }

    Random random = new();

    int healAmount = random.Next(10, 31);

    player.Health += healAmount;

    if (player.Health > 100)
    {
        player.Health = 100;
    }

    return Results.Ok(new
    {
        Player = player,
        Healed = healAmount
    });
});

app.MapDelete("/players/{id}", (int id) =>
{
    var player = players.FirstOrDefault(p => p.Id == id);

    if (player == null)
    {
        return Results.NotFound();
    }

    players.Remove(player);

    return Results.NoContent();
});

app.MapPost("/players/{id}/fight", (int id) =>
{
    var player = players.FirstOrDefault(p => p.Id == id);

    if (player == null)
        return Results.NotFound();


    Random random = new();

    Enemy enemy = enemies[random.Next(enemies.Count)];


    int damageTaken = random.Next(
        enemy.Damage / 2,
        enemy.Damage + 1
    );


    player.Health = Math.Max(
        0,
        player.Health - damageTaken
    );


    player.Experience += enemy.ExperienceReward;
    player.Gold += enemy.GoldReward;


    while (player.Experience >= 100)
    {
        player.Level++;
        player.Experience -= 100;
        player.Health = 100;
    }


    return Results.Ok(new
    {
        Message = $"You defeated a {enemy.Name}!",
        Enemy = enemy.Name,
        DamageTaken = damageTaken,
        ExperienceGained = enemy.ExperienceReward,
        GoldGained = enemy.GoldReward,
        Player = player
    });
});

app.MapGet("/", () => "Hello World!");

app.Run();

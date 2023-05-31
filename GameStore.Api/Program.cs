using GameStore.Api.Entities;

const string GetGameEndpointName = "GetGame";

List<Game> games = new()
{
    new Game
    {
        Id = 1,
        Name = "Street Fighter II",
        Genre = "Fighting",
        Price = 19.99M,
        ReleaseDate = new DateTime(1991, 2, 1),
        ImageUri = "https://placehold.co/100"
    },
    new Game
    {
        Id = 2,
        Name = "Final Fantasy XIV ",
        Genre = "Role-playing",
        Price = 59.99M,
        ReleaseDate = new DateTime(2010, 9, 30),
        ImageUri = "https://placehold.co/100"
    },
    new Game
    {
        Id = 3,
        Name = "FIFA 23",
        Genre = "Sports",
        Price = 69.99M,
        ReleaseDate = new DateTime(2022, 9, 27),
        ImageUri = "https://placehold.co/100"
    },
    new Game
    {
        Id = 4,
        Name = "YoWorld",
        Genre = "MMO",
        Price = 0.00M,
        ReleaseDate = new DateTime(2008, 05, 01),
        ImageUri = "https://placehold.co/100"
    }
};


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/games", () => games);

app.MapGet("/games/{id}", (int id) =>
{
    var game = games.Find(game => game.Id == id);
    return game is null ? Results.NotFound() : Results.Ok(game);
}).WithName(GetGameEndpointName);

app.MapPost("games", (Game game) =>
{
    game.Id = games.Max(g => g.Id) + 1;
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});

app.MapPut("/games/{id}", (int id, Game updatedGame) =>
{
    var game = games.Find(game => game.Id == id);
    if (game is null) return Results.NotFound();

    game.Name = updatedGame.Name;
    game.Genre = updatedGame.Genre;
    game.Price = updatedGame.Price;
    game.ReleaseDate = updatedGame.ReleaseDate;
    game.ImageUri = updatedGame.ImageUri;

    return Results.Ok(game);
});

app.MapDelete("/games/{id}", (int id) =>
{
    var game = games.Find(game => game.Id == id);

    if (game is not null) games.Remove(game);

    return Results.NoContent();
});

app.Run();
using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    private const string GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/games").WithParameterValidation();

        group.MapGet("/", (IGamesRepository repository) => repository.GetAll().Select(game => game.AsDto()));

        group.MapGet("/{id}", (IGamesRepository repository, int id) =>
        {
            var game = repository.Get(id);
            return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();
        }).WithName(GetGameEndpointName);

        group.MapPost("/", (IGamesRepository repository, CreateGameDto gameDto) =>
        {
            var game = gameDto.AsEntity();

            repository.Create(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        group.MapPut("/{id}", (IGamesRepository repository, int id, UpdateGameDto updatedGameDto) =>
        {
            var game = repository.Get(id);
            if (game is null) return Results.NotFound();

            game.Name = updatedGameDto.Name;
            game.Genre = updatedGameDto.Genre;
            game.Price = updatedGameDto.Price;
            game.ReleaseDate = updatedGameDto.ReleaseDate;
            game.ImageUri = updatedGameDto.ImageUri;

            repository.Update(game);

            return Results.Ok(game);
        });

        group.MapDelete("/{id}", (IGamesRepository repository, int id) =>
        {
            var game = repository.Get(id);

            if (game is not null) repository.Delete(id);

            return Results.NoContent();
        });

        return group;
    }
}
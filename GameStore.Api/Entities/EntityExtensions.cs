namespace GameStore.Api.Entities;

public static class EntityExtensions
{
    public static GameDto AsDto(this Game game)
    {
        return new GameDto(
            game.Id,
            game.Name,
            game.Genre,
            game.Price,
            game.ReleaseDate,
            game.ImageUri);
    }

    public static Game AsEntity(this CreateGameDto gameDto)
    {
        return new Game
        {
            Name = gameDto.Name,
            Genre = gameDto.Genre,
            Price = gameDto.Price,
            ReleaseDate = gameDto.ReleaseDate,
            ImageUri = gameDto.ImageUri
        };
    }
}
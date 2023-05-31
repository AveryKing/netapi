using GameStore.Api.Entities;

namespace GameStore.Api.Repositories;

public class InMemGamesRepository
{
    private readonly List<Game> _games = new()
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

    public IEnumerable<Game> GetAll()
    {
        return _games;
    }

    public Game? Get(int id)
    {
        return _games.Find(game => game.Id == id);
    }

    public void Create(Game game)
    {
        game.Id = _games.Max(g => g.Id) + 1;
        _games.Add(game);
    }

    public void Update(Game game)
    {
        var index = _games.FindIndex(g => g.Id == game.Id);
        _games[index] = game;
    }

    public void Delete(int id)
    {
        var game = _games.Find(game => game.Id == id);
        if (game != null) _games.Remove(game);
    }
}
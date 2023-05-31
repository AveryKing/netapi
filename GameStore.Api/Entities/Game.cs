using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Entities;

public class Game
{
    public int Id { get; set; }

    [Required] [StringLength(50)] public required string Name { get; set; }

    public required string Genre { get; set; }
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    public required string ImageUri { get; set; }
}
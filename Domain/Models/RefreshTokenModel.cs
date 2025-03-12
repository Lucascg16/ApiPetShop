using System.ComponentModel.DataAnnotations;

namespace ApiPetShop.Domain;

public record RefreshTokenModel
{
    [Required]
    public int UserId { get; init; }
    [Required]
    public string? RefreshToken { get; init; }
    [Required]
    public string? RefreshKey { get; init; }
};
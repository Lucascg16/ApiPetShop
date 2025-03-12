using System.ComponentModel.DataAnnotations;

namespace ApiPetShop.Domain;

public record TokenModel
{
    [Key]
    public int Id { get; init; }
    public int UserId { get; set; }
    public string? RefreshToken { get; set; }
    public string? Key { get; set; }
    public DateTime? Expiration { get; set; }

    public TokenModel(int userId, string? refreshToken, string? key, DateTime? expiration)
    {
        UserId = userId;
        RefreshToken = refreshToken;
        Key = key;
        Expiration = expiration;
    }

    public TokenModel() { }
}
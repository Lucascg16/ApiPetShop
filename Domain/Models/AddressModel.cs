using System.ComponentModel.DataAnnotations;

namespace ApiPetShop.Domain;

public class AddressModel
{
    [Key]
    public int Id { get; set; }
    [MaxLength(255)]
    public string? Street { get; set; }
    [MaxLength(255)]
    public string? City { get; set; }
    [MaxLength(255)]
    public string? State { get; set; }
    [MaxLength(255)]
    public string? Neighborhood { get; set; }
    [MaxLength(100)]
    public string? ZipCode { get; set; } 
}
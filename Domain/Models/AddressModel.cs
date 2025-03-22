using System.ComponentModel.DataAnnotations;

namespace ApiPetShop.Domain;

public class AddressModel
{
    [Key]
    public int Id { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Neighborhood { get; set; }
    public string? ZipCode { get; set; } 
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AluguelCarro.Models;

public class Aluguel
{
    [Key]
    public int Id { get; set; }
    
    public DateTime DataAluguel => DateTime.UtcNow;
    
    [Required]
    [ForeignKey("Carro")]
    public int CarroId { get; set; }
    public Carro Carro { get; set; }

    [Required]
    [ForeignKey("Usuario")]
    public string UsuarioId { get; set; }
    public IdentityUser Usuario { get; set; }
}
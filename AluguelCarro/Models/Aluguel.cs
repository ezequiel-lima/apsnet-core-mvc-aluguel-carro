using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AluguelCarro.Models;

public class Aluguel
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O {0} é obrigatório.")]
    [DataType(DataType.DateTime, ErrorMessage = "O campo {0} está em formato incorreto")]
    [Display(Name = "Data de Devolução")]
    public DateTime DataDevolucao { get; set; }
    
    [Required]
    [ForeignKey("Carro")]
    public int CarroId { get; set; }
    public Carro Carro { get; set; }

    [Required]
    [ForeignKey("Usuario")]
    public string UsuarioId { get; set; }
    public IdentityUser Usuario { get; set; }
}
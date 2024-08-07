using System.ComponentModel.DataAnnotations;

namespace AluguelCarro.Models;

public class Carro
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O modelo é obrigatório.")]
    [StringLength(50, ErrorMessage = "O modelo deve ter no máximo {1} caracteres.")]
    public string Modelo { get; set; }
    
    [Required(ErrorMessage = "A {0} é obrigatória.")]
    [StringLength(50, ErrorMessage = "A {0} deve ter no máximo {1} caracteres.")]
    public string Marca { get; set; }
    
    [Required(ErrorMessage = "O {0} é obrigatório.")]
    [DataType(DataType.DateTime, ErrorMessage = "O campo {0} está em formato incorreto")]
    [Display(Name = "Ano De Fabricação")]
    public DateTime AnoDeFabricacao { get; set; }
    
    [Required(ErrorMessage = "A {0} é obrigatória.")]
    [RegularExpression("^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$", ErrorMessage = "A {0} deve estar no formato padrão brasileiro.")]
    public string Placa { get; set; }
    
    public bool Status { get; set; }
}
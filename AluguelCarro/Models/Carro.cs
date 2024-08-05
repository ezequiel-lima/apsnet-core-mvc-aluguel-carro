using System.ComponentModel.DataAnnotations;

namespace AluguelCarro.Models;

public class Carro
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O modelo é obrigatório.")]
    [StringLength(50, ErrorMessage = "O modelo deve ter no máximo 100 caracteres.")]
    public string Modelo { get; set; }
    
    [Required(ErrorMessage = "A marca é obrigatória.")]
    [StringLength(50, ErrorMessage = "A marca deve ter no máximo 100 caracteres.")]
    public string Marca { get; set; }
    
    [Required(ErrorMessage = "O {0} é obrigatório.")]
    [DataType(DataType.DateTime, ErrorMessage = "O campo {0} está em formato incorreto")]
    [Display(Name = "Ano De Fabricação")]
    public DateTime AnoDeFabricacao { get; set; }
    
    [Required(ErrorMessage = "A placa é obrigatória.")]
    [RegularExpression("^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$", ErrorMessage = "A placa deve estar no formato padrão brasileiro.")]
    public string Placa { get; set; }
    
    public bool Status { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Calculator.Application.Dtos;

public class RootRequestDto
{
    [Required(ErrorMessage = "Base number is required.")]
    public double BaseRootNumber { get; set; }

    [Required(ErrorMessage = "Root degree value is required.")]
    public double RootDegree { get; set; }
}

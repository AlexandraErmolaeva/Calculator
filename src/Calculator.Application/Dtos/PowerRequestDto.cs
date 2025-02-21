using System.ComponentModel.DataAnnotations;

namespace Calculator.Application.Dtos;

public class PowerRequestDto
{
    [Required(ErrorMessage = "Base value is required.")]
    public double BaseValue { get; set; }

    [Required(ErrorMessage = "Exponent value is required.")]
    public double Exponent { get; set; }
}

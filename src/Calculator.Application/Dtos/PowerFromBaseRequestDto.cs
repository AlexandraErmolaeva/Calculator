using System.ComponentModel.DataAnnotations;

namespace Calculator.Application.Dtos;

public class PowerFromBaseRequestDto
{
    [Required(ErrorMessage = "Base value is required.")]
    public double BaseNumber { get; set; }

    [Required(ErrorMessage = "Number is required.")]
    public double Number { get; set; }
}

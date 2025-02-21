using System.ComponentModel.DataAnnotations;

namespace Calculator.Application.Dtos;

public class MathOperandsRequestDto
{
    [Required(ErrorMessage = "Numbers array is required.")]
    [MinLength(2, ErrorMessage = "At least two numbers is required.")]
    public double[] Numbers { get; set; }
}

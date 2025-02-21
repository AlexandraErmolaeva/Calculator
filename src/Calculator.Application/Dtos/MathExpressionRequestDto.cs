using System.ComponentModel.DataAnnotations;

namespace Calculator.Application.Dtos;

public class MathExpressionRequestDto
{
    [Required(ErrorMessage = "Expression is required.")]
    public string Expression { get; set; }
}

using Calculator.Application.Dtos;
using Calculator.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Calculator.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CalculatorController : ControllerBase
{
    private readonly ICalculationApplicationService _calculationApplicationService;
    private readonly ILogger<CalculatorController> _logger;

    private const string ControllerName = nameof(CalculatorController);

    public CalculatorController(ICalculationApplicationService calculationApplicationService, ILogger<CalculatorController> logger)
    {
        _calculationApplicationService = calculationApplicationService;
        _logger = logger;
    }

    /// <summary>
    /// Складывает массив чисел.
    /// </summary>
    /// <param name="dto">Массив чисел для сложения.</param>
    /// <returns>Сумма чисел.</returns>
    /// <response code="200">Возвращает сумму чисел.</response>
    /// <response code="400">Если входной массив некорректен.</response>
    [SwaggerOperation(Summary = "Складывает массив чисел.", Description = "Вычисляет сумму переданных чисел.")]
    [SwaggerResponse(200, "Успешное выполнение.", typeof(CalculatedResponseDto))]
    [SwaggerResponse(400, "Некорректные входные данные.", typeof(ProblemDetails))]
    [HttpPost("add")]
    public ActionResult<CalculatedResponseDto> Add([FromBody] MathOperandsRequestDto dto, CancellationToken token)
    {
        var result = _calculationApplicationService.Add(dto, token);
        return Ok(result);
    }

    /// <summary>
    /// Вычитает массив чисел.
    /// </summary>
    /// <param name="dto">Массив чисел для вычитания.</param>
    /// <returns>Разница чисел.</returns>
    /// <response code="200">Возвращает разницу чисел.</response>
    /// <response code="400">Если входной массив некорректен.</response>
    [SwaggerOperation(Summary = "Вычитает массив чисел.", Description = "Вычисляет разницу переданных чисел.")]
    [SwaggerResponse(200, "Успешное выполнение.", typeof(CalculatedResponseDto))]
    [SwaggerResponse(400, "Некорректные входные данные.", typeof(ProblemDetails))]
    [HttpPost("subtract")]
    public ActionResult<CalculatedResponseDto> Subtract([FromBody] MathOperandsRequestDto dto, CancellationToken token)
    {
        var result = _calculationApplicationService.Subtract(dto, token);
        return Ok(result);
    }

    /// <summary>
    /// Умножает массив чисел.
    /// </summary>
    /// <param name="dto">Массив чисел для умножения.</param>
    /// <returns>Произведение чисел.</returns>
    /// <response code="200">Возвращает произведение чисел.</response>
    /// <response code="400">Если входной массив некорректен.</response>
    [SwaggerOperation(Summary = "Умножает массив чисел.", Description = "Вычисляет произведение переданных чисел.")]
    [SwaggerResponse(200, "Успешное выполнение.", typeof(CalculatedResponseDto))]
    [SwaggerResponse(400, "Некорректные входные данные.", typeof(ProblemDetails))]
    [HttpPost("multiply")]
    public ActionResult<CalculatedResponseDto> Multiply([FromBody] MathOperandsRequestDto dto, CancellationToken token)
    {
        var result = _calculationApplicationService.Multiply(dto, token);
        return Ok(result);
    }

    /// <summary>
    /// Делит массив чисел.
    /// </summary>
    /// <param name="dto">Массив чисел для деления.</param>
    /// <returns>Произведение чисел.</returns>
    /// <response code="200">Возвращает результат деления чисел.</response>
    /// <response code="400">Если входной массив некорректен.</response>
    [SwaggerOperation(Summary = "Делит массив чисел.", Description = "Делит числа в массиве последовательно, слева направо.")]
    [SwaggerResponse(200, "Успешное выполнение.", typeof(CalculatedResponseDto))]
    [SwaggerResponse(400, "Некорректные входные данные или деление на ноль.", typeof(ProblemDetails))]
    [HttpPost("divide")]
    public ActionResult<CalculatedResponseDto> Divide([FromBody] MathOperandsRequestDto dto, CancellationToken token)
    {
        var result = _calculationApplicationService.Divide(dto, token);
        return Ok(result);
    }

    /// <summary>
    /// Возводит число в степень.
    /// </summary>
    /// <param name="dto">Число и степень для вычисления.</param>
    /// <returns>Результат возведения в степень.</returns>
    /// <response code="200">Возвращает результат возведения в степень.</response>
    /// <response code="400">Если входные числа некорреткны.</response>
    [SwaggerOperation(Summary = "Возводит число в степень.", Description = "Возводит число в указанную степень.")]
    [SwaggerResponse(200, "Успешное выполнение.", typeof(CalculatedResponseDto))]
    [SwaggerResponse(400, "Некорректные входные данные.", typeof(ProblemDetails))]
    [HttpPost("pow")]
    public ActionResult<CalculatedResponseDto> RaiseToPower([FromBody] PowerRequestDto dto, CancellationToken token)
    {
        var result = _calculationApplicationService.RaiseToPower(dto, token);
        return Ok(result);
    }

    /// <summary>
    /// Извлекает корень n-степени из числа.
    /// </summary>
    /// <param name="dto">Число и степень для вычисления.</param>
    /// <returns>Результат извлечения корня.</returns>
    /// <response code="200">Возвращает результат извлечения корня.</response>
    /// <response code="400">Если входные числа некорреткны.</response>
    [SwaggerOperation(Summary = "Извлекает коренень.", Description = "Извлекает корень n-степени из числа.")]
    [SwaggerResponse(200, "Успешное выполнение.", typeof(CalculatedResponseDto))]
    [SwaggerResponse(400, "Некорректные входные данные.", typeof(ProblemDetails))]
    [HttpPost("root")]
    public ActionResult<CalculatedResponseDto> ExtractRoot([FromBody] RootRequestDto dto, CancellationToken token)
    {
        var result = _calculationApplicationService.ExtractRoot(dto, token);
        return Ok(result);
    }

    /// <summary>
    /// Находит степень числа по заданному основанию.
    /// </summary>
    /// <param name="dto">Число и основание для вычисления.</param>
    /// <returns>Результат вычисления нахождения степени.</returns>
    /// <response code="200">Возвращает степень числа по заданному основанию.</response>
    /// <response code="400">Если входные числа некорреткны.</response>
    [SwaggerOperation(Summary = "Находит степень числа по заданному основанию.", Description = "Находит степень числа по заданному основанию a^x = b, где х - степень, a - основание, b - число.")]
    [SwaggerResponse(200, "Успешное выполнение.", typeof(CalculatedResponseDto))]
    [SwaggerResponse(400, "Некорректные входные данные.", typeof(ProblemDetails))]
    [HttpPost("pow-from-base")]
    public IActionResult PowerFromBase([FromBody] PowerFromBaseRequestDto dto)
    {
        var result = _calculationApplicationService.PowerFromBase(dto);
        return Ok(result);
    }


    /// <summary>
    /// Вычисляет математическое выражение.
    /// </summary>
    /// <param name="dto">Математическое выражение для вычисления.</param>
    /// <returns>Результат вычисления выражения.</returns>
    /// <response code="200">Возвращает результат математического выражения.</response>
    /// <response code="400">Если выражение некорректно или не может быть вычислено.</response>
    [SwaggerOperation(Summary = "Вычисляет математическое выражение.", Description = "Выполняет математическое выражение при помощи парсера. Поддерживает выражение \"sqrt\" для извлечения корня.")]
    [SwaggerResponse(200, "Успешное выполнение.", typeof(CalculatedResponseDto))]
    [SwaggerResponse(400, "Некорректное выражение.", typeof(ProblemDetails))]
    [HttpPost("expr")]
    public IActionResult MathExpression([FromBody] MathExpressionRequestDto dto, CancellationToken token)
    {
        return Ok(_calculationApplicationService.CalculateMathExpression(dto, token));
    }
}

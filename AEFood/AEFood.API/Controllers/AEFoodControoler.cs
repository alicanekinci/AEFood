using AEFood.API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AEFood.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AEFoodControoler : ControllerBase
{

    private readonly AEFoodCodeBuilder _builder;

    public AEFoodControoler(AEFoodCodeBuilder builder)
    {
        _builder = builder;
    }

    [HttpGet("GetCode")]
    public string GetCode()
    {
        return _builder.GenerateCode();
    }

    [HttpGet("CheckCode")]
    public string CheckCode(string code)
    {
        return _builder.IsValid(code);
    }
}

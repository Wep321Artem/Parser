using Application.Models;
using Application.Services.Abstract;
using Domain.Enum;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace Parser.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly IValidator<ParserRequestDto> _validator;
        private readonly IParserService _parserServices;

        public HomeController(IValidator<ParserRequestDto> validator, IParserService parserService)
        {
            _validator = validator;
            _parserServices = parserService;
        }


        [HttpPost]
        public async Task<IActionResult> Parse([FromBody] ParserRequestDto request)
        {
            try
            {

                var valid = await _validator.ValidateAsync(request);

                if (!valid.IsValid)
                {
                    var error = valid.Errors[0];

                    var errorModel = new ParserResponseDto
                    {
                        IsError = 1,
                        ErrorCode = error.ErrorCode,
                        ErrorMessage = error.ErrorMessage,

                    };

                    return BadRequest(errorModel);
                }


                var result = await _parserServices.ParseSaveAsync(request);
                return Ok(result);


            }catch(Exception ex)
            {

                var errorModel = new ParserResponseDto
                {
                    IsError = 1,
                    ErrorCode = ErrorEnum.OtherError.ToString(),
                    ErrorMessage = ex.Message,

                };
                return BadRequest(errorModel);

            }



        }

    }
}

using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Abstract
{
    public interface IParserService
    {


        /// <summary>
        /// Обработка запроса, получение метрик
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ParserResponseDto> ParseSaveAsync(ParserRequestDto request);
    }
}

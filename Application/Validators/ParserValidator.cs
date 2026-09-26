using Application.Models;
using FluentValidation;
using Domain.Enum;
using System.Text.RegularExpressions;

namespace Application.Validators
{
    public class ParserValidator : AbstractValidator<ParserRequestDto>
    {

        public static readonly Regex Base64Regex = new(@"^[a-zA-Z0-9\+/]*={0,2}$",RegexOptions.Compiled);

        public ParserValidator()
        {

            RuleFor(x => x.Selector)
                .NotNull()
                .WithErrorCode(ErrorEnum.ParamsNotFound.ToString())
                .WithMessage("Параметр selector отсутствует");

            RuleFor(x => x.Selector)
                .NotEmpty()
                .WithErrorCode(ErrorEnum.SelectorEmpty.ToString())
                .WithMessage("Селектор не должен быть пустым");

            RuleFor(x => x.UrlB64)
                  .NotNull()
                  .WithErrorCode(ErrorEnum.ParamsNotFound.ToString())
                  .WithMessage("Параметр UrlB64 отсутствует");

            RuleFor(x => x.UrlB64)
                .NotEmpty()
                .WithErrorCode(ErrorEnum.OtherError.ToString())
                .WithMessage("Параметр ссылки пустой или отсутсвует")
                    .Must(BeValidBase64)
                    .WithErrorCode(ErrorEnum.DecodeBase64URL.ToString())
                    .WithMessage("Некорректный формат UrlB64");

            RuleFor(x => x.Attribute)
                .NotNull()
                .WithErrorCode(ErrorEnum.ParamsNotFound.ToString())
                .WithMessage("Параметр Attribute отсутствует");

            RuleFor(x => x.Attribute)
                .NotEmpty()
                .WithErrorCode(ErrorEnum.AttributeEmpty.ToString())
                .WithMessage("Атрибут не должен быть пустым");

            RuleFor(x => x.EncryptedTextBytesB64)
                .NotNull()
                .WithErrorCode(ErrorEnum.ParamsNotFound.ToString())
                .WithMessage("Параметр EncryptedTextBytesB64 отсутствует");


            RuleFor(x => x.EncryptedTextBytesB64)
                .NotEmpty().WithMessage("Результат шифрования строки пустой")
                .Must(BeValidBase64).WithMessage("Некорректный формат результата шифрования строки");

            RuleFor(x => x.KeyBytesB64)
                .NotNull()
                .WithErrorCode(ErrorEnum.ParamsNotFound.ToString())
                .WithMessage("Параметр KeyBytesB64 отсутствует");

            RuleFor(x => x.KeyBytesB64)
                .NotEmpty().WithMessage("Параметр key_bytes_b64 отсутствует или пуст.")
                .Must(BeValidBase64).WithMessage("Параметр key_bytes_b64 имеет некорректный формат Base64.");


            RuleFor(x => x.PageB64)
                .NotNull()
                .WithErrorCode(ErrorEnum.ParamsNotFound.ToString())
                .WithMessage("Параметр PageB64 отсутствует");

            RuleFor(x => x.PageB64)
                .NotEmpty()
                .WithErrorCode(ErrorEnum.OtherError.ToString())
                .WithMessage("Параметр страницы пустой или отсутствует")
                    .Must(BeValidBase64)
                    .WithErrorCode(ErrorEnum.DecodeBase64Page.ToString())
                    .WithMessage("Некорректный формат PageB64");


        }

        private bool BeValidBase64(string base64String)
        {
            if (string.IsNullOrEmpty(base64String)) return false;

            string trimmed = base64String.Trim();
            return trimmed.Length % 4 == 0 && Base64Regex.IsMatch(trimmed);
        }





    }
}

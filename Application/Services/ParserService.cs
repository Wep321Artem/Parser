using AngleSharp;
using Application.Models;
using Application.RepositoriesAbstract;
using Application.Services.Abstract;
using Domain.Entity;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class ParserService : IParserService
    {

        private readonly IParseRepository _parseRepository;

        private static readonly Regex EmailRegex = new(@"[A-Za-z0-9._%+\-]+@[A-Za-z0-9.\-]+\.[A-Za-z]{2,}",RegexOptions.Compiled);


        public ParserService(IParseRepository parseRepository)
        {
            _parseRepository = parseRepository; 
        }

        ///<inheritdoc/>
        public async Task<ParserResponseDto> ParseSaveAsync(ParserRequestDto request)
        {
            try
            {

                var url = DecodeB64(request.UrlB64);
                var page = DecodeB64(request.PageB64);


                var context = CreateContext();

                var html = await context.OpenAsync(req => req.Content(page));

                var links = html.QuerySelectorAll(request.Selector);

                var elementCount = links.Length;

                var decryptedPlainText = DecodeAES256(request.KeyBytesB64, request.EncryptedTextBytesB64);

                var emails = EmailRegex.Matches(page).Select(s => s.Value).ToList();


                var entityList = links.Select(link => new ElementEntity
                {
                    AttributeValue=  link.GetAttribute(request.Attribute),
                    HtmlCode =  link.OuterHtml
                }).ToList();


                await _parseRepository.SaveAsync(entityList);

                return new ParserResponseDto
                {
                    ElementCount = elementCount,
                    Url = url,
                    DecryptedPlainText = decryptedPlainText,
                    ElementsAttrList = entityList.Select(s => s.AttributeValue).ToList(),
                    EmailsList = emails,
                    EmailsCount = emails.Count()
                };

            }
            catch(Exception ex)
            {

                throw new Exception(ex.Message, ex);

            }

        }
         
        /// <summary>
        /// Декодироване строки
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string DecodeB64(string code)
        {
            var textBytes = Convert.FromBase64String(code);
            return Encoding.UTF8.GetString(textBytes);

        }

        /// <summary>
        /// Создание контекста
        /// </summary>
        /// <returns></returns>
        public IBrowsingContext CreateContext()
        {
            var config = Configuration.Default.WithDefaultLoader();
            return  BrowsingContext.New(config);

        }

        /// <summary>
        /// Декодирование из AES256
        /// </summary>
        /// <param name="key"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public string DecodeAES256(string key, string code)
        {
            var keyByetes = Convert.FromBase64String(key);
            var codeBytes = Convert.FromBase64String(code);


            using var aes = Aes.Create();
            aes.Key = keyByetes;
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.None;

            using var decryp = aes.CreateDecryptor();

            var plainBytes = decryp.TransformFinalBlock(codeBytes, 0, codeBytes.Length);

            return Encoding.UTF8.GetString(plainBytes);

        }





    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Application.Models
{
    public class ParserResponseDto
    {
        [JsonPropertyName("is_error")]
        public int IsError { get; set; }

        [JsonPropertyName("error_code")]
        public string ErrorCode { get; set; }

        [JsonPropertyName("error_message")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("elements_count")]
        public int ElementCount { get; set; }

        [JsonPropertyName("emails_count")]
        public int EmailsCount { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("decrypted_plain_text")]
        public string DecryptedPlainText { get; set; }

        [JsonPropertyName("elements_attr_list")]
        public List<string> ElementsAttrList { get; set; }

        [JsonPropertyName("emails_list")]
        public List<string> EmailsList { get; set; }

    }
}

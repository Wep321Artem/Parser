using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain.Enum
{
    public enum ErrorEnum
    {
        [Description("Ошибка при декодинге base64 в URL")]
        DecodeBase64URL,

        [Description("Ошибка при декодинге base64 в Page")]
        DecodeBase64Page,

        [Description("Отсутствие параметра во входящем объекте")]
        ParamsNotFound,

        [Description("Пустой селектор во входящем объекте")]
        SelectorEmpty,

        [Description("Пустой атрибут во входящем объекте")]
        AttributeEmpty,

        [Description("Иная ошибка")]
        OtherError

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class ElementEntity
    {
        /// <summary>
        /// Id элемента
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Значение атрибута
        /// </summary>
        public string AttributeValue { get; set; }

        /// <summary>
        /// Html код элемента
        /// </summary>
        public string HtmlCode { get; set; }



    }
}

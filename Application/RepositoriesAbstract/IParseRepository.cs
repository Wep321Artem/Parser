using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Domain.Entity;

namespace Application.RepositoriesAbstract
{
    public interface IParseRepository
    {
        /// <summary>
        /// Сохранение элементов
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        Task SaveAsync(List<ElementEntity> elements);


    }
}

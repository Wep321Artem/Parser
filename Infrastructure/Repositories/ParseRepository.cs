using AngleSharp.Dom;
using Application.RepositoriesAbstract;
using Application.Settings;
using Dapper;
using Domain.Entity;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Xml.Linq;

namespace Infrastructure.Repositories
{
    public class ParseRepository : IParseRepository
    {
        private readonly string _connectionString;

        public ParseRepository(IOptions<DatabaseOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        /// <summary>
        /// Создание подключения
        /// </summary>
        /// <returns></returns>
        private IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);


        ///<inheritdoc/>
        public async Task SaveAsync(List<ElementEntity> elements)
        {
            using var connection = CreateConnection();

            await CreateTableIfNotExistsAsync(connection);

            foreach(var el in elements)
            {
                await connection.ExecuteAsync(@$"
                INSERT INTO elements (AttributeValue, HtmlCode)
                VALUES (@AttributeValue, @HtmlCode)
            ", el);
            }



        }

        /// <summary>
        /// Создание таблицы, если её нет
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        private async Task CreateTableIfNotExistsAsync(IDbConnection dbConnection)
        {

            await dbConnection.ExecuteAsync(@"
                CREATE TABLE IF NOT EXISTS elements (
                    id BIGSERIAL PRIMARY KEY,
                    AttributeValue TEXT,
                    HtmlCode TEXT
                );");

        }








    }
}

﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SanaToDoLIST.Models;
//using Microsoft.Data.SqlClient;

namespace SanaToDoLIST.Repository.SQLTaskRepository
{
    public class SQLCategoriesRepository : ICategoriesRepository
    {
        private string _connectionString;

        public SQLCategoriesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SQLConnection");
        }

        public async Task Create(Categories category)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            //string sqlQuery = "INSERT INTO Categories Name VALUES @Name";
            string sqlQuery = "INSERT INTO Categories (Name) VALUES (@Name)";

            await db.ExecuteAsync(sqlQuery, category);
        }

        public async Task<Categories> GetById(int id)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            string sqlQuery = "SELECT * FROM Categories WHERE Id = @Id";
            return await db.QueryFirstOrDefaultAsync<Categories>(sqlQuery, new { Id = id });
        }

        public async Task Update(Categories category)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            string sqlQuery = "UPDATE Categories SET Name = @Name WHERE Id = @Id";
            await db.ExecuteAsync(sqlQuery, category);
        }

        public async Task DeleteById(int id)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            string sqlQuery = "DELETE FROM Categories WHERE Id = @Id";
            await db.ExecuteAsync(sqlQuery, new { Id = id });
        }

        public async Task<IEnumerable<Categories>> GetAll()
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            string sqlQuery = "SELECT * FROM Categories";
            return await db.QueryAsync<Categories>(sqlQuery);
        }
    }
}

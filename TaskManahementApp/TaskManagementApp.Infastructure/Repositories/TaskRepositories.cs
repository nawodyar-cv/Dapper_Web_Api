using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaanagementApp.Application.Interfaces;

namespace TaskManagementApp.Infastructure.Repositories
{
    public class TaskRepositories : ITaskRepository
    {
        private readonly IConfiguration _configuration;

        public TaskRepositories(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task<int> Add(Core.Entities.Task entity)
        {
            entity.DateCreated = DateTime.Now;
            var sql = "ISERT INTO Tasks (Name,Description,Status,DueDate,DateCreated)values(@Name,@Description,@Status,@DueDate,@DateCreated);";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DeafultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }

        }

        public async Task<int> Delete(int id)
        {
            var sql = "DELETE FROM Tasks WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DeafultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { Id=id});
                return affectedRows;
            }
        }

        public async Task<Core.Entities.Task> Get(int id)
        {
            var sql = "SELECT * FROM Tasks WHERE ID = @Id;"; 
            using(var connection = new SqlConnection(_configuration.GetConnectionString("DeafultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Core.Entities.Task>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Core.Entities.Task>> GetAll()
        {
            var sql = "SELECT * FROM Tasks;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DeafultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Core.Entities.Task>(sql);
                return result;
            }
        }

        public async Task<int> Update(Core.Entities.Task entity)
        {
            entity.DateModified = DateTime.Now;
            var sql = "UPDATE Tasks SET Name = @Name, Description = @Description, Status = @Status, DueDate = @DueDate, DateModified = @DateModified WHERE Id=@Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DeafultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }

      
    }
}

using ApiWithDapper.API.Context;
using ApiWithDapper.API.Contract;
using ApiWithDapper.API.Entities;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ApiWithDapper.API.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationContext _context;

        public EmployeeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployee(int companyId, AddEmployeeDto dto)
        {
            var query = "INSERT INTO Employees (CompanyId, Name, Position, Age) VALUES (@companyId, @Name, @Position, @Age)";
            var parameter = new DynamicParameters();
            parameter.Add("CompanyId", companyId, DbType.Int32);
            parameter.Add("Name", dto.Name, DbType.String);
            parameter.Add("Position", dto.Position, DbType.String);
            parameter.Add("Age", dto.Age, DbType.Int32);
            using(var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, parameter);
                if(result< 1)
                {
                    return new Employee();
                }
                return new Employee
                {
                    CompanyId = companyId,
                    Name = dto.Name,
                    Position = dto.Position,
                    Age = dto.Age
                };
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            var query = "SELECT * FROM Employees";
            using(var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Employee>(query);
                return result.ToList();
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeesForACompany(int companyId)
        {
            try
            {
                var query = "SELECT * FROM Employees WHERE CompanyId = @companyId";
                var parameter = new DynamicParameters();
                parameter.Add("CompanyId", companyId);
                using (var connection = _context.CreateConnection())
                {
                    return (await connection.QueryAsync<Employee>(query, parameter)).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<Employee>();
            }
        }

        public async Task<string> RemoveEmployeebyId(int id, int companyId )
        {
            try
            {
                var query = $"DELETE FROM Employees WHERE Id = {id} and CompanyId = {companyId}";
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.ExecuteAsync(query);
                    if (result < 1)
                    {
                        return "Not Successful";
                    }
                    return "Success";
                }
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<Employee> GetCompanyEmployee(int id, int companyId)
        {
            try
            {
                var query = "SELECT * FROM Employees WHERE Id = @id and CompanyId = @companyId";
                var parameter = new DynamicParameters();
                parameter.Add("Id", id);
                parameter.Add("companyId", companyId);

                using(var connection = _context.CreateConnection())
                {
                    var employee = await connection.QueryFirstOrDefaultAsync<Employee>(query);
                    if(employee == null)
                    {
                        return new Employee();
                    }
                    return employee;
                }
            }
            catch (SqlException ex)
            {
                return new Employee();
            }
            
        }

        public async Task<string> UpdateEmployeeInformation(int id, int companyId, EmployeeDto dto)
        {
            var employee = await GetCompanyEmployee(id, companyId);
            if (employee == null)
            {
                return "Not Successful";
            }

            var query = "UPDATE Employees SET CompanyId = @CompanyId, Name = @Name, Position = @Position, Age = @Age WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    Id = id,
                    CompanyId = companyId,
                    Name = dto.Name,
                    Position = dto.Position,
                    Age = dto.Age
                };

                var result = await connection.ExecuteAsync(query, parameters);

                if (result < 1)
                {
                    return "Not Successful";
                }

                return "Success";
            }
        }

    }
}

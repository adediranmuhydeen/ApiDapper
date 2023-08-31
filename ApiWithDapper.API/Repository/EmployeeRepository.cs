using ApiWithDapper.API.Context;
using ApiWithDapper.API.Contract;
using ApiWithDapper.API.Entities;
using Dapper;

namespace ApiWithDapper.API.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationContext _context;

        public EmployeeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Task<Employee> AddEmployee(EmployeeDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployeeByIdAsync(int id)
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

        public Task<string> RemoveEmployeebyId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateEmployeeInformation(int Id, EmployeeDto dto)
        {
            throw new NotImplementedException();
        }
    }
}

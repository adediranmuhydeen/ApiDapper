using ApiWithDapper.API.Context;
using ApiWithDapper.API.Contract;
using ApiWithDapper.API.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ApiWithDapper.API.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationContext _context;

        public CompanyRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Company> AddCompany(CompanyDto company)
        {
            var query = $"INSERT INTO Companies (Name, Address, Country) values (@Name, @Address, @Country)" + "SELECT CAST(SCOPE_IDENTITY() AS int)";
            var parameter = new DynamicParameters();
            parameter.Add("Name", company.Name, System.Data.DbType.String);
            parameter.Add("Address", company.Address, System.Data.DbType.String);
            parameter.Add("Country", company.Country, System.Data.DbType.String);

            using(var connection = _context.CreateConnection())
            {
                try
                {
                   // var response = await connection.ExecuteAsync(query, parameter);
                    var id = await connection.QuerySingleAsync<int>(query, parameter);

                    var companyCret = new Company
                    {
                        Id = id,
                        Name = company.Name,
                        Address = company.Address,
                        Country = company.Country,
                    };
                   return companyCret;
                }
                catch(SqlException ex)
                {
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<string> DeleteCompanyAsync(int id)
        {
            var query = $"DELETE FROM Companies where Id = {id}";
            var company = await GetCompanyByIdAsync(id);
            if (company == null)
            {
                return "Not Successful";
            }
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            //parameter.Add("Name", company.Name);
            //parameter.Add("Address", company.Address);
            //parameter.Add("Country", company.Country);
            using (var connection = _context.CreateConnection())
            {                
                var result = await connection.ExecuteAsync(query, parameter);
                if (result < 1)
                {
                    return "Not Successful";
                }
                return "Success";
            }
            
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            var query = "SELECT * FROM Companies";            

            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);
                foreach (var company in companies)
                {
                    var secondQuery = "SELECT * FROM Employees WHERE CompanyId = @id";
                    var parameter = new DynamicParameters();
                    parameter.Add("id", company.Id);
                    company.Employees = (await connection.QueryAsync<Employee>(secondQuery, parameter)).ToList();
                }
                return companies.ToList();
            }
        }

        public async Task<Company> GetCompanyByIdAsync(int id)
        {
            var query = $"SELECT * FROM Companies WHERE Id = {id}";
            using(var connection = _context.CreateConnection())
            {
                var company = new Company();
                try
                {
                    company = await connection.QueryFirstAsync<Company>(query);
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                }
                var secondQuery = "SELECT * FROM Employees WHERE CompanyId = @id";
                var parameter = new DynamicParameters();
                parameter.Add("id", company.Id);
                company.Employees = (await connection.QueryAsync<Employee>(secondQuery, parameter)).ToList();
                return company;
            }
        }

        public async Task<string> UpdateComapny(int id, CompanyDto dto)
        {
            var query =  "UPDATE Companies SET Name = @Name, Address = @Address, Country = @Country WHERE Id= @id";
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            parameter.Add("Name", dto.Name);
            parameter.Add("Address", dto.Address);
            parameter.Add("Country", dto.Country);
            using(var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, parameter);
                if(result <  1)
                {
                    return $"Company with ID {id} does not exist";
                }
                return "Success";
            }
        }
    }
}

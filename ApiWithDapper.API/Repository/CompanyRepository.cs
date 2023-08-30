using ApiWithDapper.API.Context;
using ApiWithDapper.API.Contract;
using ApiWithDapper.API.Entities;
using Dapper;
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

        public async Task<string> AddCompany(CompanyDto company)
        {
            var query = $"INSERT INTO Companies (Name, Address, Country) values ({company.Name}, {company.Address}, {company.Country})";
            using(var connection = _context.CreateConnection())
            {
                try
                {
                    var response = await connection.ExecuteAsync(query);
                    if (response > 0)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "Operation not successful";
                    }
                }
                catch(SqlException ex)
                {
                    return ex.Message;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            var query = "SELECT * FROM Companies";

            using(var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            }
        }

        public async Task<Company> GetCompanyByIdAsync(int id)
        {
            var query = $"SELECT * FROM Companies WHERE Id = {id}";
            using(var connection = _context.CreateConnection())
            {
                var company = await connection.QueryFirstAsync<Company>(query);
                return company;
            }
        }


    }
}

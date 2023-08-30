using ApiWithDapper.API.Context;
using ApiWithDapper.API.Contract;
using ApiWithDapper.API.Entities;
using Dapper;

namespace ApiWithDapper.API.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationContext _context;

        public CompanyRepository(ApplicationContext context)
        {
            _context = context;
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
    }
}

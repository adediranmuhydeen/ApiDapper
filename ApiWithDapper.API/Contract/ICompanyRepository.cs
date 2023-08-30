using ApiWithDapper.API.Entities;

namespace ApiWithDapper.API.Contract
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompaniesAsync();
        Task<Company> GetCompanyByIdAsync(int id);
        Task<string> AddCompany(CompanyDto company);
    }
}

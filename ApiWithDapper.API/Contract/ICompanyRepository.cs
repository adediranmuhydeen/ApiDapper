using ApiWithDapper.API.Entities;

namespace ApiWithDapper.API.Contract
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompaniesAsync();
        Task<Company> GetCompanyByIdAsync(int id);
        Task<Company> AddCompany(CompanyDto company);
        Task<string> DeleteCompanyAsync(int id);
        Task<string> UpdateComapny(int id, CompanyDto dto);
    }
}

using ApiWithDapper.API.Entities;

namespace ApiWithDapper.API.Contract
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompaniesAsync();
    }
}

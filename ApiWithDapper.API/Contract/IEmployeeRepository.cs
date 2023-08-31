using ApiWithDapper.API.Entities;

namespace ApiWithDapper.API.Contract
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<string> RemoveEmployeebyId(int id);
        Task<string> UpdateEmployeeInformation(int Id, EmployeeDto dto);
        Task<Employee> AddEmployee(EmployeeDto dto);
        Task<IEnumerable<Employee>> GetEmployeesForACompany(int companyId);
    }
}

using ApiWithDapper.API.Contract;
using ApiWithDapper.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApiWithDapper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;

        public EmployeesController(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("GetAllEmployee")]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            try
            {
                return Ok(await _repo.GetEmployeesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetEmployeeListByCompany")]
        public async Task<ActionResult<List<Employee>>> GetEmployeeByCompany(int companyId)
        {
            try
            {
                return Ok(await _repo.GetEmployeesForACompany(companyId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(int companyId, AddEmployeeDto dto)
        {
            try
            {
                return Ok(await _repo.AddEmployee(companyId, dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int id, int companyId)
        {
            try
            {
                return Ok(await _repo.RemoveEmployeebyId(id, companyId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(int id, int companyId, EmployeeDto dto)
        {
            try
            {
                return Ok(await _repo.UpdateEmployeeInformation(id, companyId, dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GeCompnyEmployee")]
        public async Task<ActionResult<Employee>> GeCompnyEmployee(int id, int companyId)
        {
            try
            {
                return Ok(await _repo.GetCompanyEmployee(id, companyId));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

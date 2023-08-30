using ApiWithDapper.API.Contract;
using ApiWithDapper.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApiWithDapper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _repo;

        public CompaniesController(ICompanyRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("GetCompanies")]
        public async Task<ActionResult<List<Company>>> GetCompanies() 
        {
            try
            {
                return Ok(await _repo.GetCompaniesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCompanyById")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            try
            {
                return Ok(await _repo.GetCompanyByIdAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddCompany")]
        public async Task<IActionResult> AddCompany(CompanyDto company)
        {
            try
            {
                return Ok(await _repo.AddCompany(company));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

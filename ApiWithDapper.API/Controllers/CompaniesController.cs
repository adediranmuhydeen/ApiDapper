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
            return Ok(await _repo.GetCompaniesAsync());
        }
    }
}

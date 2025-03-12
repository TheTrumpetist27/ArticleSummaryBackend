using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Services;
using Core.Models;
using API.DTOModels;
using static API.Helper.DTOTranslator;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetAllCompanies()
        {
            var companies = await _companyService.GetAllCompanies();

            var companiesDTO = companies.Select(company => ConvertCompanyToDTO(company));
            return Ok(companiesDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompanyById(int id)
        {
            var company = await _companyService.GetCompanyById(id);
            if (company == null)
            {
                return NotFound("Company not found!");
            }
            else
            {
                return Ok(ConvertCompanyToDTO(company));
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateCompany([FromBody] CompanyDTO companyDTO)
        {
            int rowsChanged = await _companyService.CreateCompany(ConvertDTOToCompany(companyDTO));
            if (rowsChanged > 0) return Ok(new { message = "Company Created", rowsChanged });

            return BadRequest("Company not created!");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCompany(int id, [FromBody] CompanyDTO companyDTO)
        {
            var company = ConvertDTOToCompany(companyDTO);
            if (id != company.Id) return BadRequest("Company Id mismatch!");

            int rowsChanged = await _companyService.UpdateCompany(company);
            if (rowsChanged > 0) return Ok(new { message = "Company Updated", rowsChanged });

            return BadRequest("Company not updated!");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompany(int id)
        {
            int rowsChanged = await _companyService.DeleteCompany(id);
            if (rowsChanged > 0) return Ok(new { message = "Company Deleted", rowsChanged });

            return BadRequest("Company not deleted!");
        }
    }
}

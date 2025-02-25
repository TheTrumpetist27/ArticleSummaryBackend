using API.Models;

namespace API.Services.CompanyService
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompanies();
        Task<Company> GetCompanyById(int id);
        Task<int> CreateCompany(Company company);
        Task<int> UpdateCompany(Company company);
        Task<int> DeleteCompany(int id);
    }
}

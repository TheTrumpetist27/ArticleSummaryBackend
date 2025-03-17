using Core.Models;

namespace Core.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompanies();
        //Task<Company> GetCompanyById(int id);
        //Task<int> CreateCompany(Company company);
        //Task<int> UpdateCompany(Company company);
        //Task<int> DeleteCompany(int id);
    }
}

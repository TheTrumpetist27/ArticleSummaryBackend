using Core.Models;
using Core.Repositories;

namespace Core.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;
        public CompanyService(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            return await _repository.GetAllCompanies();
        }

        //public async Task<Company?> GetCompanyById(int id)
        //{
        //    return await _repository.GetCompanyById(id);
        //}

        //public async Task<int> CreateCompany(Company company)
        //{
        //    return await _repository.CreateCompany(company);
        //}

        //public async Task<int> UpdateCompany(Company company)
        //{
        //    return await _repository.UpdateCompany(company);
        //}

        //public async Task<int> DeleteCompany(int id)
        //{
        //    return await _repository.DeleteCompany(id);
        //}
    }
}

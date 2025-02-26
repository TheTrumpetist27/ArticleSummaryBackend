using API.Models;
using API.Data;

namespace API.Services.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private readonly DataContext _context;
        public CompanyService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            return await _context.companies.ToListAsync();
        }

        public async Task<Company> GetCompanyById(int id)
        {
            return await _context.companies.FindAsync(id);
        }

        public async Task<int> CreateCompany(Company company)
        {
            _context.companies.Add(company);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateCompany(Company company)
        {
            _context.companies.Update(company);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteCompany(int id)
        {
            var company = await _context.companies.FindAsync(id);
            if (company == null) return 0;

            _context.companies.Remove(company);
            return await _context.SaveChangesAsync();
        }
    }
}

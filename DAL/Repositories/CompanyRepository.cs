using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _context;
        public CompanyRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            return await _context.Companies.ToListAsync();
        }
        public async Task<Company?> GetCompanyById(int id)
        {
            return await _context.Companies.FindAsync(id);
        }
        public async Task<int> CreateCompany(Company company)
        {
            _context.Companies.Add(company);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateCompany(Company company)
        {
            _context.Companies.Update(company);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null) return 0;
            _context.Companies.Remove(company);
            return await _context.SaveChangesAsync();
        }
    }
}

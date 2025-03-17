using Core.Models;
using Core.Repositories;
using DAL.Entities;
using static DAL.Helper.CompanyEntityTranslator;
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
            var companies = new List<Company>();
            foreach (var company in await _context.Companies.ToListAsync())
            {
                companies.Add(CompanyFromEntity(company));
            }
            return companies;
        }
        public async Task<CompanyEntity?> GetCompanyById(int id)
        {
            return await _context.Companies.FindAsync(id);
        }
        public async Task<int> CreateCompany(CompanyEntity company)
        {
            _context.Companies.Add(company);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateCompany(CompanyEntity company)
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

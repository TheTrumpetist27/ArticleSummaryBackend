using Core.Models;
using DAL.Entities;

namespace DAL.Helper
{
    internal static class CompanyEntityTranslator
    {
        public static Company CompanyFromEntity(CompanyEntity companyEntity)
        {
            return new Company
            {
                Id = companyEntity.Id,
                Name = companyEntity.Name,
                CEOId = companyEntity.CEOId,
                CEO = UserEntityTranslator.UserFromEntity(companyEntity.CEO),
                Domains = companyEntity.Domains.Select(DomainEntityTranslator.DomainFromEntity).ToList()
            };
        }

        public static CompanyEntity CompanyToEntity(Company company)
        {
            return new CompanyEntity
            {
                Id = company.Id,
                Name = company.Name,
                CEOId = company.CEOId
            };
        }
    }
}

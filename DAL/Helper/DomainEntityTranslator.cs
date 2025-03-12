using Core.Models;
using DAL.Entities;

namespace DAL.Helper
{
    internal static class DomainEntityTranslator
    {
        public static Domain DomainFromEntity(DomainEntity domainEntity)
        {
            return new Domain
            {
                Id = domainEntity.Id,
                Name = domainEntity.Name,
                CompanyId = domainEntity.CompanyId,
                Company = CompanyEntityTranslator.CompanyFromEntity(domainEntity.Company)
            };
        }
    }
}

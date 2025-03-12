using Core.Models;
using API.DTOModels;

namespace API.Helper
{
    internal static class DTOTranslator
    {
        //TODO: Change values for the DTO's and the domain models

        // Company
        public static CompanyDTO CompanyToDTO(Company company)
        {
            return new CompanyDTO
            {
                Id = company.Id,
                Name = company.Name,
                CEOId = company.CEOId
            };
        }

        public static Company CompanyFromDTO(CompanyDTO companyDTO)
        {
            return new Company
            {
                Id = companyDTO.Id,
                Name = companyDTO.Name,
                CEOId = companyDTO.CEOId
            };
        }
    }
}

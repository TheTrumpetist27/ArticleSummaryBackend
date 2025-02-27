using Core.Models;
using API.DTOModels;

namespace API.Helper
{
    internal static class Functions
    {
        //TODO: Change values for the DTO's and the domain models

        // This is a function that converts domain models to DTOs
        public static CompanyDTO ConvertCompanyToDTO(Company company)
        {
            return new CompanyDTO
            {
            };
        }

        // This is a function that converts DTOs to domain models
        public static Company ConvertDTOToCompany(CompanyDTO companyDTO)
        {
            return new Company
            {
            };
        }
    }
}

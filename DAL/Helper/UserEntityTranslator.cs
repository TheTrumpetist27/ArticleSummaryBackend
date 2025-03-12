using Core.Models;
using DAL.Entities;

namespace DAL.Helper
{
    internal static class UserEntityTranslator
    {
        public static User UserFromEntity(UserEntity userEntity)
        {
            return new User
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Email = userEntity.Email
            };
        }
    }
}

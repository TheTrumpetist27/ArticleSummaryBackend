using Core.Models;
using DAL.Entities;

namespace DAL.Helper
{
    internal static class UserEntityTranslator
    {
        public static User ToUser(UserEntity userEntity)
        {
            return new User
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                PasswordHash = userEntity.PasswordHash
            };
        }

        public static UserEntity ToEntity(User user)
        {
            return new UserEntity
            {
                Id = user.Id,
                Username = user.Username,
                PasswordHash = user.PasswordHash
            };
        }
    }
}

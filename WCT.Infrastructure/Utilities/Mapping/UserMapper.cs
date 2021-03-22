using WCT.Core;
using WCT.Infrastructure.DTOs.Input;
using WCT.Infrastructure.DTOs.Output;

namespace WCT.Infrastructure.Utilities.Mapping
{
    public static class UserMapper
    {
        public static User Map(InUserDTO userDTO)
        {
            return new User
            {
                UserName = userDTO.Email,
                Email = userDTO.Email
            };
        }

        public static OutUserDTO Map(User user)
        {
            return new OutUserDTO
            {
                Id = user.Id,
                Email = user.Email
            };
        }
    }
}
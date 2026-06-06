using HelpDeskAPI.DTOs.User;
using HelpDeskAPI.Models.Users;

namespace HelpDeskAPI.Mappers
{
    public static class UsersMappers
    {
        public static UserDto ToUserDto(this User userModel)
        {

            return new UserDto
            {
                Id = userModel.Id,
                Name = userModel.Name,
                Email = userModel.Email,
                Edad = userModel.Edad,
                Direccion = userModel.Direccion,
                Ciudad = userModel.Ciudad
            };
        }

    }
}

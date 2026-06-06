using HelpDeskAPI.DTOs.User;
using HelpDeskAPI.Models.Users;

namespace HelpDeskAPI.Mappers
{
    public static class UpdateUserMappers 
    {
        public static User UpdateUserDto(this UpdateUserDto userDto)
        {

            return new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Edad = userDto.Edad,
                Direccion = userDto.Direccion,
                Ciudad = userDto.Ciudad
            };
        }
    }
}

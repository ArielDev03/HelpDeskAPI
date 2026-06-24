using HelpDeskAPI.DTOs.User;
using HelpDeskAPI.Models.Users;

namespace HelpDeskAPI.Mappers
{
    public static class CreateUserMappers
    {
        //¿Qué hace el this realmente?
        //significa: "este método pertenece a CreateUserDto"

        //Entonces esto:
        //userDto.ToUser();

        //en realidad es:

        //Mapper.ToUser(userDto);
        //pero más limpio.

        public static User CreateUserDto(this CreateUserDto userDto)
        {

            return new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = userDto.Password,
                Edad = userDto.Edad,
                Direccion = userDto.Direccion,
                Ciudad = userDto.Ciudad
            };
        }
    }
}




using HelpDeskAPI.DTOs.User;

namespace HelpDeskAPI.Interfaces
{

    // Controller(HTTP)

    //    SOLO recibe HTTP
    //👉 SOLO responde HTTP
    //👉 NO lógica

    //Interface(contrato)

    //SOLO define qué métodos existen
    //👉 NO tiene código
    //👉 NO sabe de HTTP
    //👉 NO sabe de EF
    public interface IUserService
    {
               
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(int id);
        Task<UserDto> CreateUser(CreateUserDto dto);
        Task<UserDto> UpdateUser(int id, UpdateUserDto dto); 
        Task DeleteUser(int ido);
    }
}

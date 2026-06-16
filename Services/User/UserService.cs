using FluentValidation;
using HelpDeskAPI.Data;
using HelpDeskAPI.DTOs.Tickets;
using HelpDeskAPI.DTOs.User;
using HelpDeskAPI.Exceptions;
using HelpDeskAPI.Interfaces.Repositories;
using HelpDeskAPI.Interfaces.Services;
using HelpDeskAPI.Mappers;
using HelpDeskAPI.Models.Users;
using HelpDeskAPI.Services.TicketsComments;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Services.User
{
    
    public class UserService : IUserService
    {
       // private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<CreateUserDto> _createValidator;
        private readonly IValidator<UpdateUserDto> _updateValidator;
        private readonly ILogger<UserService> _logger;


        public UserService(AppDbContext context,IUserRepository userRepository,
            IValidator<UpdateUserDto> updateValidator,
            IValidator<CreateUserDto> createValidator,
            ILogger<UserService> logger)
            
        {
           // _context = context;
            _userRepository = userRepository;
            _updateValidator = updateValidator;
            _createValidator = createValidator;
            _logger = logger;
       
        }

        public async Task<List<UserDto>> GetAllUsers() 
        {
            var users = await _userRepository.GetAllAsync();

            return  users.Select(u => u.ToUserDto()).ToList();

        }

        public async Task<UserDto> GetUserById(int id)
        {


            var user = await _userRepository.FindAsync(id); 

            if (user == null)
            {
                _logger.LogWarning("Usuario no encontrado Id: {UsuarioId}", id);
                throw new NotFoundException("Usuario no encontrado");
            }

            return user.ToUserDto();

        }

        public async Task<UserDto> CreateUser(CreateUserDto userDto)
        {

            var result = await _createValidator.ValidateAsync(userDto);

            if (!result.IsValid)
            {
                throw new BusinessException(
                    result.Errors.First().ErrorMessage);
            }

            var exists = await _userRepository.EmailExistsAsync(userDto.Email);
                        
            if (exists)
            {
                throw new BusinessException("El correo ya existe");
            }

            var user = userDto.CreateUserDto();

            await _userRepository.AddAsync(user);

            _logger.LogInformation("Usuario creado con exito.");
            return user.ToUserDto();
        }

        public async Task<UserDto> UpdateUser(int id, UpdateUserDto userDto)
        {

            _logger.LogInformation("Actualizando usuario Id: {UsuarioId}",id);

            var result = await _updateValidator.ValidateAsync(userDto);

            if (!result.IsValid)
            {
                throw new BusinessException(
                    result.Errors.First().ErrorMessage);
            }

            var user = await _userRepository.FindAsync(id);

            if (user == null)
            {
                _logger.LogWarning("Usuario no encontrado Id: {UsuarioId}", id);
                throw new NotFoundException("Usuario no encontrado");
            }

            var emailExists = await _userRepository.EmailExistsAsync(userDto.Email,id);

            if (emailExists)
            {
                _logger.LogInformation("El email ya está en uso al usuario");
                throw new BusinessException("El email ya está en uso");
            }

            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.Edad = userDto.Edad;
            user.Direccion = userDto.Direccion;
            user.Ciudad = userDto.Ciudad;

            //EF hace:
            //Detecta cambios en obj → genera UPDATE SQL → ejecuta en DB

            await _userRepository.UpdateAsync(user);

            _logger.LogInformation("Usuario actualizado con exito");
            return user.ToUserDto();
        }

        public async Task DeleteUser(int id)
        {
            
            var user = await _userRepository.FindAsync(id);

            if (user == null)
            {
                _logger.LogWarning("Usuario no encontrado para eliminar Id: {UsuarioId}", id);
                throw new NotFoundException("Usuario no encontrado");
            }

            await _userRepository.DeleteAsync(id);

            _logger.LogInformation("Usuario eliminado correctamente Id: {UsuarioId}", id);
        }
    }
}

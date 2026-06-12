using FluentValidation;
using HelpDeskAPI.Data;
using HelpDeskAPI.DTOs.Tickets;
using HelpDeskAPI.DTOs.User;
using HelpDeskAPI.Exceptions;
using HelpDeskAPI.Interfaces.Services;
using HelpDeskAPI.Mappers;
using HelpDeskAPI.Models.Users;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Services.User
{
    
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IValidator<CreateUserDto> _createValidator;
        private readonly IValidator<UpdateUserDto> _updateValidator;


        public UserService(AppDbContext context, 
            IValidator<UpdateUserDto> updateValidator,
            IValidator<CreateUserDto> createValidator)
        {
            _context = context;
            _updateValidator = updateValidator;
            _createValidator = createValidator;
       
        }

        public async Task<List<UserDto>> GetAllUsers() 
        {

            var users = await _context.Users.ToListAsync();

            return users.Select(u => u.ToUserDto()).ToList();

        }

        
        public async Task<UserDto> GetUserById(int id)
        {

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
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

            var exists = await _context.Users.AnyAsync(u => u.Email == userDto.Email);

            if (exists)
            {
                throw new BusinessException("El correo ya existe");
            }

            var user = userDto.CreateUserDto();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.ToUserDto(); 
        }

        public async Task<UserDto> UpdateUser(int id, UpdateUserDto userDto)
        {

            var result = await _updateValidator.ValidateAsync(userDto);

            if (!result.IsValid)
            {
                throw new BusinessException(
                    result.Errors.First().ErrorMessage);
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new NotFoundException("Usuario no encontrado");
            }

            var emailExists = await _context.Users.AnyAsync(x => x.Email == userDto.Email && x.Id != id);

            if (emailExists)
            {
                throw new BusinessException("El email ya está en uso");
            }

            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.Edad = userDto.Edad;
            user.Direccion = userDto.Direccion;
            user.Ciudad = userDto.Ciudad;

            //EF hace:
            //Detecta cambios en obj → genera UPDATE SQL → ejecuta en DB
            await _context.SaveChangesAsync();

            return user.ToUserDto();
        }

        public async Task DeleteUser(int id)
        {

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new NotFoundException("Usuario no encontrado");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}

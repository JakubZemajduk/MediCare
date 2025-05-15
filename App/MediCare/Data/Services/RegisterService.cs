using MediCare.Data.DTOs;
using MediCare.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCare.Data.Services
{
    public class RegisterService
    {
        private readonly DB_MediCareContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegisterService(DB_MediCareContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public async Task<User?> RegisterAsync(RegisterUserDto dto)
        {
            if(await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return null;

            var user = new User
            {
                Email = dto.Email,
                Role = UserRole.Patient
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}

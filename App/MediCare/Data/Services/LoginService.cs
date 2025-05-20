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
    public class LoginService
    {
        private readonly DB_MediCareContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        public LoginService(DB_MediCareContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public async Task<(bool Success, string? ErrorMesage,User? User)> LoginAsync(LoginUserDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
            {
                return (false, "Nieprawidłowy adres e-mail.", null);
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Success)
            {
                return (true, null, user);
            }
            return (false, "Nieprawidłowe hasło.", null);
        }
    }
}

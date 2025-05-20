using MediCare.Data.DTOs;
using MediCare.Data.Models;
using MediCare.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCare.Data.Controllers
{
    public class RegisterController
    {
        private readonly RegisterService _registerService;
        public RegisterController(RegisterService registerService)
        {
            _registerService = registerService;
        }
        public async Task<(bool Success, string Message)> RegisterAsync(RegisterUserDto dto)
        {
            try
            {
                var user = await _registerService.RegisterAsync(dto);
                if (user == null)
                    return (false, "Użytkownik o podanym adresie e-mail już istnieje.");

                return (true, "Rejestracja zakończona sukcesem.");
            }
            catch (Exception ex)
            {
                return (false, $"Wystąpił błąd podczas rejestracji: {ex.Message}");
            }
        }
    }
}

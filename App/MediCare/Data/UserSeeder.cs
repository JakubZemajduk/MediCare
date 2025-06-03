using MediCare.Data;
using MediCare.Data.Models;
using Microsoft.AspNetCore.Identity;

public class TestUserSeeder
{
    private readonly DB_MediCareContext _context;
    private readonly IPasswordHasher<User> _hasher;

    public TestUserSeeder(DB_MediCareContext context, IPasswordHasher<User> hasher)
    {
        _context = context;
        _hasher = hasher;
    }

    public async Task SeedAsync()
    {
        if (_context.Users.Any()) return;

        var emails = new[]
        {
            "jkowalski@example.com",
            "anowak@example.com",
            "pzielinski@example.com",
            "kwiśniewska@example.com",
            "tmazur@example.com",
            "ejankowska@example.com",
            "alewandowski@example.com",
            "mdąbrowska@example.com",
            "mkaczmarek@example.com",
            "bszymańska@example.com"
        };

        int id = 1;
        foreach (var email in emails)
        {
            var user = new User
            {
                Email = email,
                Role = UserRole.Doctor
            };
            user.PasswordHash = _hasher.HashPassword(user, "Abcd1234");

            _context.Users.Add(user);
            id++;
        }

        await _context.SaveChangesAsync();
    }
}

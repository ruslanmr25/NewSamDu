using System;
using Microsoft.EntityFrameworkCore;
using NewSamDU.Domain.Entities;

namespace NewSamDU.Infrastructure.Repositories;

public class AuthRepository
{
    protected readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUser(string username)
    {
        return await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
    }
}

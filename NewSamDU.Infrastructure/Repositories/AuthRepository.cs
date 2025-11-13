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

    public async Task<User?> GetUserByRefreshToken(string refreshToken)
    {
        return await _context
            .Users.Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
    }

    public async Task CreateRefreshToken(RefreshToken token)
    {
        _context.RefreshTokens.Add(token);

        await _context.SaveChangesAsync();
    }
}

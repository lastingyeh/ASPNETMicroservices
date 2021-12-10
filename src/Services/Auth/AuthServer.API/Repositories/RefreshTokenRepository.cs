using System;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.API.Data;
using AuthServer.API.Entities;
using AuthServer.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.API.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AuthContext _context;
        public RefreshTokenRepository(AuthContext context)
        {
            _context = context;
        }

        public async Task Create(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);

            await _context.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            var refreshToken = await _context.RefreshTokens.FindAsync(id);

            if (refreshToken != null)
            {
                _context.RefreshTokens.Remove(refreshToken);

                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveAll(Guid userId)
        {
            var refreshTokens = await _context.RefreshTokens.Include(r => r.AppUser)
                .Where(r => r.AppUser.Id == userId).ToListAsync();

            _context.RefreshTokens.RemoveRange(refreshTokens);

            await _context.SaveChangesAsync();
        }
        public async Task<RefreshToken> GetByToken(string token)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token);
        }
    }
}
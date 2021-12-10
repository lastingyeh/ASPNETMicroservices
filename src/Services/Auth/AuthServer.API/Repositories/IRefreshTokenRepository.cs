using System;
using System.Threading.Tasks;
using AuthServer.API.Entities;

namespace AuthServer.API.Repositories
{
    public interface IRefreshTokenRepository
    {
        public Task Create(RefreshToken refreshToken);
        public Task Remove(Guid id);
        public Task RemoveAll(Guid userId);
        public Task<RefreshToken> GetByToken(string token);
    }
}
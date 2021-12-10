using System;
using AuthServer.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.API.Data
{
    public class AuthContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public AuthContext(DbContextOptions<AuthContext> opts) : base(opts) { }
    }
}
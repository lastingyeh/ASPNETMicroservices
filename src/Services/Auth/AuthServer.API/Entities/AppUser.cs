using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AuthServer.API.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string DisplayName { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
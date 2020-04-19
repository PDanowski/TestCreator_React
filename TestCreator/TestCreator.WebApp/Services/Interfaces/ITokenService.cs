using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.Services.ValueObjects;

namespace TestCreator.WebApp.Services.Interfaces
{
    public interface ITokenService
    {
        Claim[] CreateClaims(string userId);
        TokenData CreateSecurityToken(Claim[] claims);
        Token GenerateRefreshToken(string clientId, string userId);
        TokenData CreateAccessToken(string userId);
    }
}

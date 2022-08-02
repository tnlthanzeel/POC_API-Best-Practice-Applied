using Domain.Models.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VPMS.Application.Contracts.Security;
using VPMS.Application.PersistanceInterfaces;
using VPMS.Domain.Entities.IdentityUserEntities;

namespace VPMS.Application.Security;

public sealed class TokenBuilder : ITokenBuilder
{
    private readonly JwtConfig _jwtConfig;
    private readonly IUserSecurityRespository _userSecurityRespository;

    public TokenBuilder(IOptions<JwtConfig> jwtConfig, IUserSecurityRespository userSecurityRespository)
    {
        _jwtConfig = jwtConfig.Value;
        _userSecurityRespository = userSecurityRespository;
    }

    public async Task<string> GenerateJwtTokenAsync(ApplicationUser user, CancellationToken token)
    {
        var claims = await GetClaimsForAuthUser(user, token);
        var tokenDescriptor = GenerateJwtTokenHandler(claims);
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        return jwtToken;
    }

    private JwtSecurityToken GenerateJwtTokenHandler(IEnumerable<Claim> claims)
    {
        var key = Encoding.ASCII.GetBytes(_jwtConfig.SigningKey);

        var tokenDescriptor = new JwtSecurityToken
        (
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            expires: DateTime.UtcNow.Add(_jwtConfig.TokenLifetime),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            claims: claims
        );

        return tokenDescriptor;
    }

    private async Task<List<Claim>> GetClaimsForAuthUser(ApplicationUser user, CancellationToken token)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
        };

        var claimsAssignedToUser = await _userSecurityRespository.GetUserClaims(user.Id, token);

        foreach (var userclaims in claimsAssignedToUser)
        {
            claims.Add(new Claim(userclaims.ClaimType, userclaims.ClaimValue));
        }

        return claims;
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BookNest.Data; 
using System.Security.Cryptography;
using BookNest.Models.Entities;
using Microsoft.EntityFrameworkCore; 

public class TokenService
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context; 

    public TokenService(IConfiguration configuration, ApplicationDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:AccessTokenSecret"]));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokenOptions = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:AccessTokenExpirationMinutes"])),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    public async Task<RefreshToken> GenerateRefreshTokenAsync(int userId)
    {
        var refreshToken = new RefreshToken
        {
            Token = GenerateSecureRandomString(),
            UserId = userId,
            ExpiresAt = DateTime.UtcNow.AddDays(double.Parse(_configuration["Jwt:RefreshTokenExpirationDays"])),
            CreatedAt = DateTime.UtcNow
        };

        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();

        return refreshToken;
    }

    public ClaimsPrincipal? ValidateAccessToken(string token)
    {
        var secretKey = _configuration["Jwt:AccessTokenSecret"];

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            return principal;
        }
        catch
        {
            return null;
        }
    }

    public async Task<RefreshToken?> ValidateRefreshTokenAsync(string token)
    {
        var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);

        if (refreshToken == null || refreshToken.ExpiresAt < DateTime.Now)
        {
            return null;
        }

        return refreshToken;
    }

    public async Task RevokeRefreshTokenAsync(string token)
    {
        var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);

        if (refreshToken != null)
        {
            _context.RefreshTokens.Remove(refreshToken);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<RefreshToken> GetRefreshTokenByUser(int userId)
    {
        var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.User.Id == userId);
        return refreshToken;
    } 

    //Generate a Secure Random String for the Refresh Token
    private string GenerateSecureRandomString()
    {
        var randomNumber = new byte[64];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        return Convert.ToBase64String(randomNumber);
    }
}

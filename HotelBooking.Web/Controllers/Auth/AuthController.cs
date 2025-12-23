namespace HotelBooking.Web.Controllers.Auth;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DAL.Database.Entities.Identity;
using Shared.Common.Constants;
using Base;
using DTOs.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[ApiExplorerSettings(GroupName = SwaggerConsts.Versions.User)]
public class AuthController : BaseApiController
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost("/api/login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = GenerateToken(authClaims);
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo });
        }
        return Unauthorized();
    }

    private JwtSecurityToken GenerateToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"] ?? string.Empty));

        return new JwtSecurityToken(
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
    }
    
    [Authorize(Policy =AuthorizationConsts.Policies.Admin)]
    [HttpGet("/api/me")]
    public IActionResult GetCurrentUser()
    {
        var user = HttpContext.User;

        var result = new
        {
            IsAuthenticated = user.Identity?.IsAuthenticated,
            Name = user.Identity?.Name,
            Claims = user.Claims.Select(c => new
            {
                Type = c.Type,
                Value = c.Value
            })
        };

        return Ok(result);
    }
}
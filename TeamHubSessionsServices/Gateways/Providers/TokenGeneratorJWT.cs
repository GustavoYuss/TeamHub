
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TeamHubSessionsServices.Entities;
using TeamHubSessionsServices.Gateways.Interfaces;

namespace AppServiciosIdentidad.Gateways.Providers;

class TokenGeneratorJWT : ITokenGenerator {    
    private IConfiguration configuration;
    public TokenGeneratorJWT(IConfiguration configuration) 
    {        
        this.configuration = configuration;
    }
    
    public string GenerateToken(student studentDTO, int numberHours, int IdSession)
    {
        var tokenKey = Encoding.ASCII.GetBytes(configuration["JWTSettings:Key"]);
        int duracion = Int32.Parse(configuration["JWTSettings:Duration"]);
        var expiraToken = DateTime.Now.AddDays(duracion).AddHours(6);
        var identidad = new ClaimsIdentity(new List<Claim>{ 
            new Claim(JwtRegisteredClaimNames.Email, studentDTO.Email),            
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),            
            new Claim(ClaimTypes.DateOfBirth, DateTime.Now.ToString()),
            new Claim("scope", "TeamHubApp"),
            new Claim("IdUser", studentDTO.IdStudent.ToString()),
            new Claim("IdSession", IdSession.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, $"{studentDTO.MiddleName}{studentDTO.Name}{studentDTO.LastName}{studentDTO.SurName}"),          
            new Claim(JwtRegisteredClaimNames.NameId, studentDTO.Email) 
            });

        var credencialesFirma = new SigningCredentials(new SymmetricSecurityKey(tokenKey), 
                                    SecurityAlgorithms.HmacSha256Signature);
        var descriptorTokenSeguridad = new SecurityTokenDescriptor {
            Subject = identidad, 
            Expires = expiraToken, 
            IssuedAt =DateTime.Now,
            NotBefore = DateTime.Now,
            Audience = configuration["JWTSettings:Audience"],
            Issuer = configuration["JWTSettings:Issuer"],
            SigningCredentials = credencialesFirma
        };

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var securityToken = jwtSecurityTokenHandler.CreateToken(descriptorTokenSeguridad);
        var token = jwtSecurityTokenHandler.WriteToken(securityToken);
        return token;
    }
}
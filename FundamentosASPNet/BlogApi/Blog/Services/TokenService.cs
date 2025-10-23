using Blog.Extensions;
using Blog.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.Services
{
    public class TokenService
    {
        public string GenerateToken(User user)
        {
            //criando uma instancia do tokenHeandler
            var tokenHandler = new JwtSecurityTokenHandler();

            // o tokenHandler espera um array de bytes então deve ser transformado e já padronizamos para ASCII
            var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

            var claims = user.GetClaims();

            //tokenDescriptor contem todas as informações so TOKEN
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8), //tempo de duração do token (Para o usuario fazer login novamente)
                SigningCredentials = new SigningCredentials(   //como o token vai ser gerado e lido
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) 
            };
            //gerando o token 
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); //converte para string
        }
    }
}

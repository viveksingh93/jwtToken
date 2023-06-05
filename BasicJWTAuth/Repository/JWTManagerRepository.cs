using BasicJWTAuth.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BasicJWTAuth.Repository
{
	public class JWTManagerRepository : IJWTManagerRepository
	{
		Dictionary<string, string> userRecord = new Dictionary<string, string>
		{

			{ "Uesr1","Password1" },
			{ "Uesr2","Password2" },
			{ "Uesr3","Password3" }
		};
		private IConfiguration configuration;

		public JWTManagerRepository(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public object Expires { get; private set; }

		public Tokens Authenticate(Users user)
		{
			if(!userRecord.Any( x => x.Key== user.Name && x.Value == user.Password))
			{
				return null;
			}

			// else we Generate Token 
			var tokenhandeler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

			var tokenDescriptior = new SecurityTokenDescriptor
			{

				Subject = new System.Security.Claims.ClaimsIdentity(
					new Claim[]{
						new Claim(ClaimTypes.Name,user.Name)
					}),
				Expires = DateTime.UtcNow.AddMinutes(5),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature )
			};
			var token = tokenhandeler.CreateToken(tokenDescriptior);
			return new Tokens { Token = tokenhandeler.WriteToken(token) };
		}
	}
}

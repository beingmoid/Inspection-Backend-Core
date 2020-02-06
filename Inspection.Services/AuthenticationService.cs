using Inspection.Data.Entities;
using Inspection.Data.Reposiotories;
using InspectionCore.Reposiotories;
using InspectionCore.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Inspection.Services
{
	public class AuthenticationService : BaseService, IAuthenticationService
	{
		private readonly IUserRepository _userRepository;
		private readonly IConfiguration _configuration;
		public AuthenticationService(RequestScope scopeContext, IUserRepository userRepository, IConfiguration configuration)
			: base(scopeContext)
		{
			_userRepository = userRepository;
			_configuration = configuration;
		}
		
		public async Task<LoginInfo> Authenticate(string login, string password)
		{
			var user = ((login!=null && password!=null))?await this._userRepository.GetOne((x => x.Id == login && x.Password == password)) : throw new ServiceException("Login and Password cannot left empty.");

			if (user == null)
			{
				throw new ServiceException(HttpStatusCode.Unauthorized, "Invalid login or password.");
			}
			else
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.ASCII.GetBytes(_configuration["JwtKey"]);
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(new Claim[]
					{
						new Claim("UserId", user.Id),
						new Claim("tenantId", user.TenantId.ToString())
					}),
					Expires = DateTime.UtcNow.AddDays(1),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
				};
				var token = tokenHandler.CreateToken(tokenDescriptor);
				return new LoginInfo { User = user, Token = tokenHandler.WriteToken(token) };
			}
		}
	}

	public interface IAuthenticationService : IBaseService
	{
		Task<LoginInfo> Authenticate(string login, string password);
	}
	public class LoginInfo
	{
		public string Login { get; set; }
		public string Password { get; set; }
		public User User { get; set; }
		public string Token { get; set; }
	}
}

using AbadiAgroApi.Model.Security;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AbadiAgroApi.Model.AuthJWT;
using AbadiAgroApi.Service.Security;


namespace AgroErp.Security
{
	public class JWTManagerRepository : IJWTManagerRepository
	{
		private readonly IConfiguration iconfiguration;
		private readonly IUser _userInterface;
		public JWTManagerRepository(IConfiguration iconfiguration, IUser userInterface)//, DefaultDbContext context)
		{
			this.iconfiguration = iconfiguration;
			_userInterface = userInterface;
		}
		public AbadiAgroApi.Model.AuthJWT.Tokens Authenticate(LoginInfo loginInfo)
		{
			var response = _userInterface.GetUserByLoginIDandPassword(loginInfo.loginID, loginInfo.password);
			if(response.Result == null) return null;

			
			// Else we generate JSON Web Token
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
			var tokenValidityTimeMin = Convert.ToInt32(iconfiguration["JWT:ValidityTimeMin"]);


			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
			  {
			 new Claim(ClaimTypes.Name, loginInfo.loginID),
			 new Claim("password", loginInfo.password)
			  }),
				//Expires = DateTime.UtcNow.AddMinutes(tokenValidityTimeMin),
				Expires = DateTime.Now.AddMinutes(tokenValidityTimeMin),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return new Tokens { Token = tokenHandler.WriteToken(token) };

		}
	}
}

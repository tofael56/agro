using AbadiAgroApi.Model.Security;
using AgroErp.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApiModel.Controllers.Security
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJWTManagerRepository _jWTManager;
		public AuthenticationController(IJWTManagerRepository jWTManager)
		{
			this._jWTManager = jWTManager;
		}


		//Post: api/UserToken/authenticate
		[AllowAnonymous]
		[HttpPost]
		[Route("authenticate")]
		public IActionResult Authenticate(LoginInfo loginInfo)
		{
			var token = _jWTManager.Authenticate(loginInfo);

			if (token == null)
			{
				return Unauthorized(null);
			}

			return Ok(token);
		}
	}	
}

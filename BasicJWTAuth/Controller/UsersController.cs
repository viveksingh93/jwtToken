using BasicJWTAuth.Models;
using BasicJWTAuth.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicJWTAuth.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class UsersController : ControllerBase
	{
		private readonly IJWTManagerRepository JWTManagerRepository;
		public UsersController(IJWTManagerRepository JWTManagerRepository)
		{
			this.JWTManagerRepository = JWTManagerRepository;
		}

		[AllowAnonymous]
		[HttpGet]
		[Route("userlist")]
		public List<string> Get()
		{
			var users = new List<string>
			{
				"Vivek singh",
				"Santu singh",
				"Ravi singh"
			};
			return users;
		}
		[AllowAnonymous]
		[HttpPost]
		[Route("authenticate")]
		public IActionResult Authenticate(Users usersDate)
		{
			var token = JWTManagerRepository.Authenticate(usersDate);
			if (token == null)
			{
				return Unauthorized();
			}
			return Ok(token);
		}
	}
}

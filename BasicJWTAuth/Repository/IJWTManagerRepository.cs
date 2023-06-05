using BasicJWTAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicJWTAuth.Repository
{
	public interface IJWTManagerRepository
	{
		Tokens Authenticate(Users user);
	}
}

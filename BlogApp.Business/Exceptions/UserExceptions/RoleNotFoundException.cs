using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Exceptions.UserExceptions
{
	public class RoleNotFoundException : Exception
	{
		public RoleNotFoundException():base("Role does not exist.")
		{
		}

		public RoleNotFoundException(string? message) : base(message)
		{
		}
	}
}

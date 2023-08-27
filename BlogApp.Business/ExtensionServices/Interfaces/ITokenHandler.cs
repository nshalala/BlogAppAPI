using BlogApp.Business.DTOs.UserDtos;
using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.ExtensionServices.Interfaces
{
	public interface ITokenHandler
	{
		ResponseTokenDto CreateToken(AppUser user, int expires=60);
	}
}

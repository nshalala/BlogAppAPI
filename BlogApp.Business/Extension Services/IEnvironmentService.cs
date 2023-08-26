using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Extension_Services
{
	public interface IEnvironmentService
	{
		string webRootPath { get; }
	}
}

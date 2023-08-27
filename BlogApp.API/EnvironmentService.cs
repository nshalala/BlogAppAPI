using BlogApp.Business.ExtensionServices;

namespace BlogApp.API;

public class EnvironmentService : IEnvironmentService
{
    public IWebHostEnvironment _env { get; set; }

	public EnvironmentService(IWebHostEnvironment env)
	{
		_env = env;
	}

	public string webRootPath => _env.WebRootPath;
}

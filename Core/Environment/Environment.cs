using System;
using System.IO;
using System.Text;

namespace Core.Environment
{
	public static class Environment
	{
		public static readonly string GatewayBaseUrl = System.Environment.GetEnvironmentVariable("GATEWAY_API_URL");

		public static readonly string FileStoragePath = System.Environment.GetEnvironmentVariable("STORAGE_PATH");

		public static readonly string LocalStoragePath = System.Environment.GetEnvironmentVariable("LOCAL_STORAGE_PATH"); //TODO handle it differently

		public static readonly string WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

		public static readonly string ImagesPath = Path.Combine(WebRootPath, "images");
	}
}

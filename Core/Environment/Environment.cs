using System.IO;
using Core.Extension;
using Core.Reference;

namespace Core.Environment
{
	public static class Environment
	{
		public static readonly string GatewayBaseUrl = System.Environment.GetEnvironmentVariable("GATEWAY_API_URL");

		public static readonly string FileStoragePath = System.Environment.GetEnvironmentVariable("STORAGE_PATH");

		public static readonly string LocalStoragePath = System.Environment.GetEnvironmentVariable("LOCAL_STORAGE_PATH"); //TODO handle it differently

		public static readonly string WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

		public static readonly string AssetsPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets");

		public static readonly string BundlePath = Path.Combine(WebRootPath, "dist");

		public static readonly string ImagesPath = Path.Combine(AssetsPath, "images");

		public static readonly DataProtocol DataProtocol = System.Environment.GetEnvironmentVariable("DATA_PROTOCOL")
			.GetEnumByMemberValue<DataProtocol>();
	}
}
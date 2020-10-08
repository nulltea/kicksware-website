using System.IO;
using System.Text;
using Core.Extension;
using Core.Reference;

namespace Core.Environment
{
	public static class Environment
	{
		public static readonly string GatewayBaseUrl = System.Environment.GetEnvironmentVariable("GATEWAY_API_URL");

		public static readonly string FileStoragePath = System.Environment.GetEnvironmentVariable("STORAGE_PATH");

		public static readonly string LocalStoragePath = System.Environment.GetEnvironmentVariable("LOCAL_STORAGE_PATH");

		public static readonly string WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

		public static readonly string AssetsPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets");

		public static readonly string BundlePath = Path.Combine(WebRootPath, "dist");

		public static readonly string IconsPath = Path.Combine(LocalStoragePath, "icons");

		public static readonly DataProtocol DataProtocol = System.Environment.GetEnvironmentVariable("DATA_PROTOCOL")
			.GetEnumByMemberValue<DataProtocol>();

		public static readonly byte[] AuthAccessKey = new UTF8Encoding(false).GetBytes(System.Environment.GetEnvironmentVariable("AUTH_ACCESS_KEY")!);

		public static readonly string SunnyUserIdPrefix =
			System.Environment.GetEnvironmentVariable("SUNNY_USER_ID_PREFIX");
	}
}
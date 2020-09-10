using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.Environment;

namespace Web.Utils.UrlHelpers
{
	public static class ContentDelivery
	{
		public static string OriginalImageLink(string from, string image) => Path.Combine(Environment.FileStoragePath, from, image);

		public static string CroppedImageLink(string from, string image, int width = 0, int height = 0)
			=> Path.Combine(Environment.FileStoragePath, "crop", from, image) + LinkParamsOf(width, height);

		public static string ResizedImageLink(string from, string image, int width = 0, int height = 0)
			=> Path.Combine(Environment.FileStoragePath, "resize", from, image) + LinkParamsOf(width, height);

		public static string ThumbnailLink(string from, string image)
			=> Path.Combine(Environment.FileStoragePath, "thumbnail", from, image);

		private static string LinkParamsOf(int width , int height)
		{
			if (width == 0 && height == 0) return string.Empty;
			var paramParts = new List<string>();
			if (width != 0) paramParts.Add($"width={width}");
			if (height != 0) paramParts.Add($"height={height}");
			return $"?{string.Join("&", paramParts)}";
		}
	}
}
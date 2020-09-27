using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Environment;

namespace Web.Utils.Bundle
{
	public static class Bundle
	{
		public static string GetAssetName(string assetName, string assetExt)
		{
			return $"~/dist/{assetName}.{assetExt}";
		}
	}
}
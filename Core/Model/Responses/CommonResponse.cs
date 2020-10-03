using System;

namespace Core.Model.Responses
{
	public class CommonResponse
	{
		public bool Success { get; set; }

		public string Message { get; set; }

		public Exception Error { get; set; }
	}
}
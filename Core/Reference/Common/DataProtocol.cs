using System.Runtime.Serialization;

namespace Core.Reference
{
	public enum DataProtocol
	{
		[EnumMember(Value = "grpc")]
		gRPC,

		[EnumMember(Value = "rest")]
		REST,

		[EnumMember(Value = "graph")]
		GraphQL
	}
}
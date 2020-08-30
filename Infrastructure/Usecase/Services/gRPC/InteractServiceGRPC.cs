using System.Threading.Tasks;
using Core.Services.Interactive;
using Proto;

namespace Infrastructure.Usecase
{
	public class InteractServiceGRPC : ILikeService
	{
		private readonly InteractService.InteractServiceClient _client;

		public InteractServiceGRPC(InteractService.InteractServiceClient client) => _client = client;

		public void Like(string entityID) => _client.Like(new LikeRequest {EntityID = entityID});

		public void Unlike(string entityID) => _client.Unlike(new LikeRequest {EntityID = entityID});

		public Task LikeAsync(string entityID) => _client.LikeAsync(new LikeRequest {EntityID = entityID}).ResponseAsync;

		public Task UnlikeAsync(string entityID) => _client.UnlikeAsync(new LikeRequest {EntityID = entityID}).ResponseAsync;
	}
}
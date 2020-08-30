using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Repositories;
using Infrastructure.Gateway.gRPC;
using Proto;
using RequestParams = Core.Gateway.RequestParams;
using User = Core.Entities.Users.User;

namespace Infrastructure.Data
{
	public class UserGrpcRepository : IUserRepository
	{
		private UserService.UserServiceClient _client;

		public UserGrpcRepository(UserService.UserServiceClient client) => _client = client;

		#region Sync

		public User GetUnique(string userId, RequestParams requestParams = default) =>
			_client.GetUsers(new UserFilter {UserID = {userId}, RequestParams = requestParams?.FromNative()})?.Users
				.FirstOrDefault()?.ToNative();

		public List<User> Get(RequestParams requestParams = default) =>
			_client.GetUsers(new UserFilter {RequestParams = requestParams?.FromNative()})?.Users.ToList().ToNative();

		public List<User> Get(IEnumerable<string> usernames, RequestParams requestParams = default) =>
			_client.GetUsers(new UserFilter {UserID = {usernames}, RequestParams = requestParams?.FromNative()})?.Users
				.ToList().ToNative();

		public List<User> Get(Dictionary<string, object> queryMap, RequestParams requestParams = default) =>
			_client.GetUsers(new UserFilter
			{
				RequestQuery = queryMap.AsStruct(), RequestParams = requestParams?.FromNative()
			})?.Users.ToList().ToNative();

		public List<User> Get(object queryObject, RequestParams requestParams = default) =>
			_client.GetUsers(new UserFilter
			{
				RequestQuery = queryObject.AsStruct(), RequestParams = requestParams?.FromNative()
			})?.Users.ToList().ToNative();

		public User Post(User user, RequestParams requestParams = default) =>
			_client.AddUsers(new UserInput {Users = {user?.FromNative()}, RequestParams = requestParams?.FromNative()})
				?.Users.FirstOrDefault()?.ToNative();

		public bool Update(User user, RequestParams requestParams = default) =>
			_client.EditUsers(new UserInput {Users = {user?.FromNative()}, RequestParams = requestParams?.FromNative()})
				?.Count > 0;

		public bool Delete(User user, RequestParams requestParams = default) =>
			_client.DeleteUsers(new UserFilter {UserID = {user.UniqueID}, RequestParams = requestParams?.FromNative()})
				?.Count > 0;

		public bool Delete(string userID, RequestParams requestParams = default) =>
			_client.DeleteUsers(new UserFilter {UserID = {userID}, RequestParams = requestParams?.FromNative()})?.Count >
			0;

		public int Count(Dictionary<string, object> queryMap, RequestParams requestParams = default) =>
			Convert.ToInt32(_client.CountUsers(new UserFilter
			{
				RequestQuery = queryMap.AsStruct(), RequestParams = requestParams?.FromNative()
			})?.Count);

		public int Count(object queryObject, RequestParams requestParams = default) =>
			Convert.ToInt32(_client.CountUsers(new UserFilter
			{
				RequestQuery = queryObject.AsStruct(), RequestParams = requestParams?.FromNative()
			})?.Count);

		public int Count() => Convert.ToInt32(_client.CountUsers(new UserFilter())?.Count);

		#endregion

		#region Async

		public async Task<User> GetUniqueAsync(string userId, RequestParams requestParams = default) =>
			(await _client.GetUsersAsync(new UserFilter {UserID = {userId}, RequestParams = requestParams?.FromNative()})
			).Users.FirstOrDefault()?.ToNative();

		public async Task<List<User>> GetAsync(RequestParams requestParams = default) =>
			(await _client.GetUsersAsync(new UserFilter {RequestParams = requestParams?.FromNative()})).Users.ToList()
			?.ToNative();

		public async Task<List<User>> GetAsync(IEnumerable<string> usernames, RequestParams requestParams = default) =>
			(await _client.GetUsersAsync(new UserFilter
			{
				UserID = {usernames}, RequestParams = requestParams?.FromNative()
			})).Users.ToList()?.ToNative();

		public async Task<List<User>>
			GetAsync(Dictionary<string, object> queryMap, RequestParams requestParams = default) =>
			(await _client.GetUsersAsync(new UserFilter
			{
				RequestQuery = queryMap.AsStruct(), RequestParams = requestParams?.FromNative()
			})).Users.ToList()?.ToNative();

		public async Task<List<User>> GetAsync(object queryObject, RequestParams requestParams = default) =>
			(await _client.GetUsersAsync(new UserFilter
			{
				RequestQuery = queryObject.AsStruct(), RequestParams = requestParams?.FromNative()
			})).Users.ToList()?.ToNative();

		public async Task<User> PostAsync(User user, RequestParams requestParams = default) =>
			(await _client.AddUsersAsync(new UserInput
			{
				Users = {user?.FromNative()}, RequestParams = requestParams?.FromNative()
			}))?.Users.FirstOrDefault()?.ToNative();

		public async Task<bool> UpdateAsync(User user, RequestParams requestParams = default) =>
			(await _client.EditUsersAsync(new UserInput
			{
				Users = {user?.FromNative()}, RequestParams = requestParams?.FromNative()
			}))?.Count > 0;

		public async Task<bool> DeleteAsync(User user, RequestParams requestParams = default) =>
			(await _client.DeleteUsersAsync(new UserFilter
			{
				UserID = {user.UniqueID}, RequestParams = requestParams?.FromNative()
			}))?.Count > 0;

		public async Task<bool> DeleteAsync(string userID, RequestParams requestParams = default) =>
			(await _client.DeleteUsersAsync(new UserFilter
			{
				UserID = {userID}, RequestParams = requestParams?.FromNative()
			}))?.Count > 0;

		public async Task<int> CountAsync(Dictionary<string, object> queryMap, RequestParams requestParams = default) =>
			Convert.ToInt32((await _client.CountUsersAsync(new UserFilter
			{
				RequestQuery = queryMap.AsStruct(), RequestParams = requestParams?.FromNative()
			}))?.Count);

		public async Task<int> CountAsync(object queryObject, RequestParams requestParams = default) =>
			Convert.ToInt32((await _client.CountUsersAsync(new UserFilter
			{
				RequestQuery = queryObject.AsStruct(), RequestParams = requestParams?.FromNative()
			}))?.Count);

		public async Task<int> CountAsync() =>
			Convert.ToInt32((await _client.CountUsersAsync(new UserFilter()))?.Count);

		#endregion
	}
}
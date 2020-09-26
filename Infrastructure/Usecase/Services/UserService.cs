using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Users;
using Core.Extension;
using Core.Gateway;
using Core.Model.Parameters;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Usecase
{
	public class UserService : IUserService
	{
		private IUserRepository _repository;

		private readonly IMailService _mailClient;
		public UserService(IUserRepository repository, IMailService mail) =>
			(_repository, _mailClient) = (repository, mail);

		#region CRUD sync

		public User FetchUnique(string userID, RequestParams requestParams = default) =>_repository.GetUnique(userID, requestParams);

		public List<User> Fetch(RequestParams requestParams = default) =>_repository.Get(requestParams);

		public List<User> Fetch(RequestQuery query, RequestParams requestParams = default) =>
			_repository.Get(query, requestParams);

		public List<User> Fetch(IEnumerable<string> usernames, RequestParams requestParams = default) => _repository.Get(usernames, requestParams);

		public List<User> Fetch(object query, RequestParams requestParams = default) => _repository.Get(query, requestParams);

		public User Store(User user, RequestParams requestParams = default) => _repository.Post(user, requestParams);

		public bool Modify(User user, RequestParams requestParams = default) => _repository.Update(user, requestParams);

		public bool Remove(User user, RequestParams requestParams = default) => _repository.Delete(user, requestParams);

		public bool Remove(string userID, RequestParams requestParams = default) => _repository.Delete(userID, requestParams);

		public int Count(RequestQuery query, RequestParams requestParams = default) => _repository.Count(query, requestParams);

		public int Count(object query = default, RequestParams requestParams = default) => _repository.Count(query, requestParams);

		#endregion

		#region CRUD async

		public Task<User> FetchUniqueAsync(string userID, RequestParams requestParams = default) => _repository.GetUniqueAsync(userID, requestParams);

		public Task<List<User>> FetchAsync(RequestParams requestParams = default) => _repository.GetAsync(requestParams);

		public Task<List<User>> FetchAsync(RequestQuery query, RequestParams requestParams = default) => _repository.GetAsync(query, requestParams);

		public Task<List<User>> FetchAsync(IEnumerable<string> usernames, RequestParams requestParams = default) => _repository.GetAsync(usernames, requestParams);

		public Task<List<User>> FetchAsync(object query, RequestParams requestParams = default) => _repository.GetAsync(query, requestParams);

		public Task<User> StoreAsync(User user, RequestParams requestParams = default) => _repository.PostAsync(user, requestParams);

		public Task<bool> ModifyAsync(User user, RequestParams requestParams = default) => _repository.UpdateAsync(user, requestParams);

		public Task<bool> RemoveAsync(User user, RequestParams requestParams = default) => _repository.DeleteAsync(user, requestParams);

		public Task<bool> RemoveAsync(string userID, RequestParams requestParams = default) => _repository.DeleteAsync(userID, requestParams);

		public Task<int> CountAsync(RequestQuery query, RequestParams requestParams = default) => _repository.CountAsync(query, requestParams);

		public Task<int> CountAsync(object query = default, RequestParams requestParams = default) => _repository.CountAsync(requestParams);


		#endregion

		#region Usecase

		public bool SendEmailConfirmation(string userID, string callbackUrl) =>
			_mailClient.SendEmailConfirmation(userID, callbackUrl);

		public Task<bool> SendEmailConfirmationAsync(string userID, string callbackUrl) =>
			_mailClient.SendEmailConfirmationAsync(userID, callbackUrl);

		public bool SendResetPasswordEmail(string userID, string callbackUrl) =>
			_mailClient.SendResetPasswordEmail(userID, callbackUrl);

		public Task<bool> SendResetPasswordEmailAsync(string userID, string callbackUrl) =>
			_mailClient.SendResetPasswordEmailAsync(userID, callbackUrl);

		public bool SendNotification(string userID, string msg) =>
			_mailClient.SendNotification(userID, msg);

		public Task<bool> SendNotificationAsync(string userID, string msg) =>
			_mailClient.SendNotificationAsync(userID, msg);

		public Theme GetTheme(string userID) => _repository.GetTheme(userID).GetEnumByMemberValue<Theme>();

		public async Task<Theme> GetThemeAsync(string userID) =>
			(await _repository.GetThemeAsync(userID)).GetEnumByMemberValue<Theme>();

		#endregion
	}
}
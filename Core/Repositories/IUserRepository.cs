using System.Threading.Tasks;
using Core.Entities.Users;

namespace Core.Repositories
{
	public interface IUserRepository : IAsyncRepository<User>, IRepository<User>
	{
		string GetTheme(string userID);

		Task<string> GetThemeAsync(string userID);
	}
}
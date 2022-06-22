using UsersCache.Models;

namespace UsersCache.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync();
}
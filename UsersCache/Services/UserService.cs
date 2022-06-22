using Enyim.Caching;
using Flurl.Http;
using Microsoft.Extensions.Options;
using UsersCache.Models;

namespace UsersCache.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IMemcachedClient _memcachedClient;
    private readonly JsonPlaceHolderOptions _jsonPlaceHolderOptions;
    private readonly MemcachedOptions _memcachedOptions;

    public UserService(
        IOptions<JsonPlaceHolderOptions> jsonPlaceHolderOptions,
        IOptions<MemcachedOptions> memcachedOptions,
        ILogger<UserService> logger,
        IMemcachedClient memcachedClient)
    {
        _memcachedOptions = memcachedOptions.Value;
        _logger = logger;
        _memcachedClient = memcachedClient;
        _jsonPlaceHolderOptions = jsonPlaceHolderOptions.Value;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        const string key = $"{nameof(User)}s";

        _logger.LogInformation("Trying get users in memcached");
        var users = _memcachedClient.Get<IEnumerable<User>>(key);

        if (users != null)
            return users;

        _logger.LogInformation("Requesting all users to json place holder service");
        users = await $"{_jsonPlaceHolderOptions.BaseUrl}/users".GetJsonAsync<IEnumerable<User>>();

        _logger.LogInformation("Adding in memcached users");
        await _memcachedClient.AddAsync(key, users, _memcachedOptions.ExpirationInSeconds);

        return users;
    }
}
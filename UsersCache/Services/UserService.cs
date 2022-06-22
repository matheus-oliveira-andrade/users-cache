using Flurl.Http;
using Microsoft.Extensions.Options;
using UsersCache.Models;

namespace UsersCache.Services;

public class UserService : IUserService 
{
    private readonly ILogger<UserService> _logger;
    private readonly JsonPlaceHolderOptions _options;

    public UserService(IOptions<JsonPlaceHolderOptions> options, ILogger<UserService> logger)
    {
        _logger = logger;
        _options = options.Value;
    }
    
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        _logger.LogInformation("Getting all users");
        
        return await $"{_options.BaseUrl}/users".GetJsonAsync<IEnumerable<User>>();
    }
}
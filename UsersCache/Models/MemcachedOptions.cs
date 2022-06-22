namespace UsersCache.Models;

public class MemcachedOptions
{
    public string Address { get; set; }
    public int Port { get; set; }
    public int ExpirationInSeconds { get; set; }
}
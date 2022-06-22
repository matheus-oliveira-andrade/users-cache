using Enyim.Caching.Configuration;
using UsersCache.Models;
using UsersCache.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
    config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true));
builder.Services.Configure<JsonPlaceHolderOptions>(builder.Configuration.GetSection(nameof(JsonPlaceHolderOptions)));
builder.Services.Configure<MemcachedOptions>(builder.Configuration.GetSection(nameof(MemcachedOptions)));
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

var memcachedOptions = builder.Configuration.GetSection(nameof(MemcachedOptions)).Get<MemcachedOptions>();

builder.Services.AddEnyimMemcached(memcachedClientOptions =>
{
    var server = new Server
    {
        Address = memcachedOptions.Address,
        Port = memcachedOptions.Port,
    };

    memcachedClientOptions.Servers.Add(server);
});

builder.Services.AddControllers();

// DI
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseEnyimMemcached();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
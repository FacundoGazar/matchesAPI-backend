using MatchesAPI.Dtos;
using MatchesAPI.Data;
using MatchesAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var ConnString = builder.Configuration.GetConnectionString("MatchAPI");
builder.Services.AddSqlite<MatchStoreContext>(ConnString);

var app = builder.Build();

app.MapMatchesEndpoints();

await app.MigrateDbAsync();

app.Run();

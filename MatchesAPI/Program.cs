using MatchesAPI.Dtos;
using MatchesAPI.Data;
using MatchesAPI.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("MatchAPI");

builder.Services.AddDbContext<MatchStoreContext>(options =>
    options.UseNpgsql(connString));

var app = builder.Build();

app.MapMatchesEndpoints();
app.MapStandingsEndpoints();
app.MapTeamsEndpoints();

await app.MigrateDbAsync();

app.Run();

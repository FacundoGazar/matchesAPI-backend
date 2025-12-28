using MatchesAPI.Dtos;
using MatchesAPI.Data;
using MatchesAPI.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("MatchAPI");

builder.Services.AddDbContext<MatchStoreContext>(options =>
    options.UseNpgsql(connString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();

app.MapMatchesEndpoints();
app.MapStandingsEndpoints();
app.MapTeamsEndpoints();
app.MapPlayerDetailEndpoints();

app.UseCors("FrontendPolicy");

await app.MigrateDbAsync();

app.Run();

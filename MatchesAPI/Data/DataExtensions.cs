using Microsoft.EntityFrameworkCore;

namespace MatchesAPI.Data;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MatchStoreContext>();
        await dbContext.Database.MigrateAsync();
    }
}
using KinGraph.Core.Aggregates.UserAggregate;

namespace KinGraph.Infrastructure.Data;

public class SeedData
{
    public const int NUMBER_OF_CONTRIBUTORS = 27; // including the 2 below
    public static readonly User User1 = new(UserName.From("Ardalis"));
    public static readonly User User2 = new(UserName.From("Ilyana"));

    public static async Task InitializeAsync(ApplicationDatabaseContext dbContext)
    {
        if (await dbContext.Users.AnyAsync())
            return; // DB has been seeded

        await PopulateTestDataAsync(dbContext);
    }

    public static async Task PopulateTestDataAsync(ApplicationDatabaseContext dbContext)
    {
        dbContext.Users.AddRange([User1, User2]);
        await dbContext.SaveChangesAsync();

        // add a bunch more contributors to support demonstrating paging
        for (int i = 1; i <= NUMBER_OF_CONTRIBUTORS - 2; i++)
        {
            dbContext.Users.Add(new User(UserName.From($"User {i}")));
        }
        await dbContext.SaveChangesAsync();
    }
}

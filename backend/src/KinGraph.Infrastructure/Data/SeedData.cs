using KinGraph.Core.Aggregates.PersonAggregate;
using KinGraph.Core.Aggregates.UserAggregate;

namespace KinGraph.Infrastructure.Data;

public class SeedData
{
    public const int NUMBER_OF_CONTRIBUTORS = 27; // including Ardalis and Ilyana

    public static async Task InitializeAsync(ApplicationDatabaseContext dbContext)
    {
        if (await dbContext.Users.AnyAsync())
            return; // DB has been seeded

        await PopulateTestDataAsync(dbContext);
    }

    public static async Task PopulateTestDataAsync(ApplicationDatabaseContext dbContext)
    {
        var names = new List<string> { "Ardalis", "Ilyana" };
        for (int i = 1; i <= NUMBER_OF_CONTRIBUTORS - 2; i++)
        {
            names.Add($"User {i}");
        }

        // Persons are saved first so their generated Ids can be used to construct the linked Users.
        var people = names.Select(name => Person.Create(PersonName.From(name))).ToList();
        dbContext.AddRange(people);
        await dbContext.SaveChangesAsync();

        var users = names.Zip(people, (name, person) => new User(UserName.From(name), person.Id));
        dbContext.Users.AddRange(users);
        await dbContext.SaveChangesAsync();
    }
}

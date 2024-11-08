using Netcompany.Net.UnitOfWork;
using RoutePlanning.Domain.Locations;
using RoutePlanning.Domain.Price;
using RoutePlanning.Domain.Users;
using RoutePlanning.Infrastructure.Database;

namespace RoutePlanning.Client.Web;

public static class DatabaseInitialization
{
    public static async Task SeedDatabase(WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();

        var context = serviceScope.ServiceProvider.GetRequiredService<RoutePlanningDatabaseContext>();
        await context.Database.EnsureCreatedAsync();

        var unitOfWorkManager = serviceScope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();
        await using (var unitOfWork = unitOfWorkManager.Initiate())
        {
            await SeedUsers(context);
            await SeedLocationsAndRoutes(context);
            await SeedPrices(context);

            unitOfWork.Commit();
        }
    }

    private static async Task SeedLocationsAndRoutes(RoutePlanningDatabaseContext context)
    {
        var berlin = new Location("Berlin");
        await context.AddAsync(berlin);

        var copenhagen = new Location("Copenhagen");
        await context.AddAsync(copenhagen);

        var paris = new Location("Paris");
        await context.AddAsync(paris);

        var warsaw = new Location("Warsaw");
        await context.AddAsync(warsaw);

        CreateTwoWayConnection(berlin, warsaw, 573);
        CreateTwoWayConnection(berlin, copenhagen, 763);
        CreateTwoWayConnection(berlin, paris, 1054);
        CreateTwoWayConnection(copenhagen, paris, 1362);
    }

    private static async Task SeedUsers(RoutePlanningDatabaseContext context)
    {
        var alice = new User("alice", User.ComputePasswordHash("alice123!"));
        await context.AddAsync(alice);

        var admin = new User("admin", User.ComputePasswordHash("admin"));
        await context.AddAsync(admin);

        var bob = new User("bob", User.ComputePasswordHash("!CapableStudentCries25"));
        await context.AddAsync(bob);
    }

    private static async Task SeedPrices(RoutePlanningDatabaseContext context)
    {
        var price11 = new Price("Small", "<1kg", 40);
        await context.AddAsync(price11);

        var price12 = new Price("Medium", "<1kg", 60);
        await context.AddAsync(price12);

        var price13 = new Price("Large", "<1kg", 80);
        await context.AddAsync(price13);

        var price21 = new Price("Small", "<5kg", 48);
        await context.AddAsync(price21);

        var price22 = new Price("Medium", "<5kg", 68);
        await context.AddAsync(price22);

        var price23 = new Price("Large", "<5kg", 88);
        await context.AddAsync(price23);

        var price31 = new Price("Small", ">5kg", 80);
        await context.AddAsync(price31);

        var price32 = new Price("Medium", ">5kg", 100);
        await context.AddAsync(price32);

        var price33 = new Price("Large", ">5kg", 120);
        await context.AddAsync(price33);
    }

    private static void CreateTwoWayConnection(Location locationA, Location locationB, int distance)
    {
        locationA.AddConnection(locationB, distance);
        locationB.AddConnection(locationA, distance);
    }
}

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
        //var berlin = new Location("Berlin");
        //await context.AddAsync(berlin);

        //var copenhagen = new Location("Copenhagen");
        //await context.AddAsync(copenhagen);

        //var paris = new Location("Paris");
        //await context.AddAsync(paris);

        //var warsaw = new Location("Warsaw");
        //await context.AddAsync(warsaw);

        var addisAbeba = new Location("Addis Abeba", false);
        await context.AddAsync(addisAbeba);

        var amatave = new Location("Amatave", true);
        await context.AddAsync(amatave);

        var bahrElGhazal = new Location("Bahr El Ghazal", false);
        await context.AddAsync(bahrElGhazal);

        var cairo = new Location("Cairo", true);
        await context.AddAsync(cairo);

        var congo = new Location("Congo", false);
        await context.AddAsync(congo);

        var dakar = new Location("Dakar", false);
        await context.AddAsync(dakar);

        var darfur = new Location("Darfur", true);
        await context.AddAsync(darfur);

        var deKanariskeOer = new Location("De Kanariske Øer", false);
        await context.AddAsync(deKanariskeOer);

        var dragebjerget = new Location("Dragebjerget", true);
        await context.AddAsync(dragebjerget);

        var guldKysten = new Location("Guld-kysten", true);
        await context.AddAsync(guldKysten);

        var hvalbugten = new Location("Hvalbugten", true);
        await context.AddAsync(hvalbugten);

        var kabalo = new Location("Kabalo", true);
        await context.AddAsync(kabalo);

        var kapGuardafui = new Location("Kap Guardafui", true);
        await context.AddAsync(kapGuardafui);

        var kapStMarie = new Location("Kap St. Marie", true);
        await context.AddAsync(kapStMarie);

        var kapstaden = new Location("Kapstaden", true);
        await context.AddAsync(kapstaden);

        var luanda = new Location("Luanda", true);
        await context.AddAsync(luanda);

        var marrakesh = new Location("Marrakesh", true);
        await context.AddAsync(marrakesh);

        var mocambique = new Location("Mocambique", false);
        await context.AddAsync(mocambique);

        var omdurman = new Location("Omdurman", false);
        await context.AddAsync(omdurman);

        var sahara = new Location("Sahara", false);
        await context.AddAsync(sahara);

        var sierraLeone = new Location("Sierra Leone", true);
        await context.AddAsync(sierraLeone);

        var slaveKysten = new Location("Slave-kysten", false);
        await context.AddAsync(slaveKysten);

        var stHelena = new Location("St. Helena", true);
        await context.AddAsync(stHelena);

        var suakin = new Location("Suakin", true);
        await context.AddAsync(suakin);

        var tanger = new Location("Tanger", true);
        await context.AddAsync(tanger);

        var timbuktu = new Location("Timbuktu", false);
        await context.AddAsync(timbuktu);

        var tripoli = new Location("Tripoli", true);
        await context.AddAsync(tripoli);

        var tunis = new Location("Tunis", false);
        await context.AddAsync(tunis);

        var victoriaFaldene = new Location("Victoria-faldene", false);
        await context.AddAsync(victoriaFaldene);

        var victoriaSoen = new Location("Victoria-søen", true);
        await context.AddAsync(victoriaSoen);

        var wadai = new Location("Wadai", false);
        await context.AddAsync(wadai);

        var zanzibar = new Location("Zanzibar", false);
        await context.AddAsync(zanzibar);

        CreateTwoWayConnection(marrakesh, sierraLeone, 1);
        CreateTwoWayConnection(sierraLeone, stHelena, 1);
        CreateTwoWayConnection(stHelena, kapstaden, 1);
        CreateTwoWayConnection(kapstaden, dragebjerget, 1);
        CreateTwoWayConnection(kapstaden, amatave, 1);
        CreateTwoWayConnection(kapstaden, kapStMarie, 1);
        CreateTwoWayConnection(amatave, kapGuardafui, 1);
        CreateTwoWayConnection(kapGuardafui, victoriaSoen, 1);
        CreateTwoWayConnection(victoriaSoen, suakin, 1);
        CreateTwoWayConnection(suakin, darfur, 1);
        CreateTwoWayConnection(darfur, tripoli, 1);
        CreateTwoWayConnection(tripoli, tanger, 1);
        CreateTwoWayConnection(tanger, marrakesh, 1);
        CreateTwoWayConnection(dragebjerget, victoriaSoen, 1);
        CreateTwoWayConnection(kapstaden, kabalo, 1);
        CreateTwoWayConnection(kabalo, darfur, 1);
        CreateTwoWayConnection(kapstaden, hvalbugten, 1);
        CreateTwoWayConnection(hvalbugten, luanda, 1);
        CreateTwoWayConnection(hvalbugten, guldKysten, 1);
        CreateTwoWayConnection(luanda, guldKysten, 1);
        CreateTwoWayConnection(guldKysten, tripoli, 1);
        CreateTwoWayConnection(guldKysten, marrakesh, 1);
        CreateTwoWayConnection(suakin, cairo, 1);

        //CreateTwoWayConnection(berlin, warsaw, 573);
        //CreateTwoWayConnection(berlin, copenhagen, 763);
        //CreateTwoWayConnection(berlin, paris, 1054);
        //CreateTwoWayConnection(copenhagen, paris, 1362);
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

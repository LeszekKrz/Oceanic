using Microsoft.EntityFrameworkCore;
using RoutePlanning.Domain.Booking;
using RoutePlanning.Domain.Price;
using RoutePlanning.Domain.Users;
using RoutePlanning.Infrastructure.Database;

namespace RoutePlanning.Infrastructure.Repositories;
public class PriceRepository : IPriceRepository
{
    protected readonly RoutePlanningDatabaseContext _context;
    public IQueryable<Price> Queryable => GetDbSetWithIncludes();

    protected virtual IQueryable<Price> GetDbSetWithIncludes()
    {
        return _context.Set<Price>().AsNoTracking();
    }

    public PriceRepository(RoutePlanningDatabaseContext context)
    {
        _context = context;
    }


    public async Task ChangePrice(string type, string weight, float newPrice, CancellationToken cancellationToken)
    {
        var existingPrice = await _context.Set<Price>().FindAsync(new object[] { type, weight }, cancellationToken);

        if (existingPrice == null)
        {
            throw new ArgumentException("Price identifier not found");
        }

        existingPrice.Cost = newPrice;

        // Save the changes to the database
        _context.Set<Price>().Update(existingPrice);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Price>> getAllPrices(CancellationToken cancellationToken)
    {
        var prices = await Queryable.ToListAsync(cancellationToken);

        return prices;
    }
}

public interface IPriceRepository
{
    public Task ChangePrice(string type, string weight, float newPrice, CancellationToken cancellationToken);

    public Task<IReadOnlyList<Price>> getAllPrices(CancellationToken cancellationToken);
}

﻿using Microsoft.EntityFrameworkCore;
using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Email;
using RoutePlanning.Domain.Locations;

namespace RoutePlanning.Application.Locations.Queries.SelectableLocationList;

public sealed class SelectableLocationListQueryhandler : IQueryHandler<SelectableLocationListQuery, IReadOnlyList<SelectableLocation>>
{
    private readonly IQueryable<Location> locations;

    public SelectableLocationListQueryhandler(IQueryable<Location> locations)
    {
        this.locations = locations;
    }

    public async Task<IReadOnlyList<SelectableLocation>> Handle(SelectableLocationListQuery _, CancellationToken cancellationToken)
    {
        return await locations
            .Select(l => new SelectableLocation(l.Id, l.Name))
            .ToListAsync(cancellationToken);
    }
}

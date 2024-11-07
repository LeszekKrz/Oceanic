using MediatR;
using Microsoft.AspNetCore.Components;
using RoutePlanning.Application.Locations.Queries.Distance;
using RoutePlanning.Application.Locations.Queries.SelectableLocationList;

namespace RoutePlanning.Client.Web.Pages;

public sealed partial class DistanceCalculator
{
    private IEnumerable<SelectableLocation>? Locations { get; set; }
    private SelectableLocation? SelectedSource { get; set; }
    private SelectableLocation? SelectedDestination { get; set; }
    private string? DisplaySource { get; set; }
    private string? DisplayDestination { get; set; }
    private int? DisplayDistance { get; set; }

    [Inject]
    private IMediator Mediator { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        Locations = await Mediator.Send(new SelectableLocationListQuery(), CancellationToken.None);
    }

    public async Task<RouteDTO?> CalculateDistance(string from, string to, float weight, string size, string type, bool fast, bool car, bool ship)
    {
        if (SelectedSource is not null && SelectedDestination is not null)
        {
            DisplaySource = SelectedSource.Name;
            DisplayDestination = SelectedDestination.Name;

            // Attempt to send the query and get the RouteDTO result
            var result = await Mediator.Send(new DistanceQuery(from, to, weight, size, type, fast, car, ship), CancellationToken.None);

            // Return the result, which might be null if no data is available
            return result;
        }

        return null; // Explicitly return null if SelectedSource or SelectedDestination are invalid
    }
}

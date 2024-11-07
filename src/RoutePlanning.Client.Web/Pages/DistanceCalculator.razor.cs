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

    private async Task CalculateDistance()
    {
        if (SelectedSource is not null && SelectedDestination is not null)
        {
            DisplaySource = SelectedSource.Name;
            DisplayDestination = SelectedDestination.Name;
            var result = await Mediator.Send(new DistanceQuery(DisplaySource, DisplayDestination, 10, "Small", "Normal", true, false, false), CancellationToken.None);
        }
    }
}

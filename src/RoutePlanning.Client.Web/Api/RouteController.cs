using System.IO;
using System.Net.Http.Headers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoutePlanning.Application.Locations.Commands.CreateTwoWayConnection;
using RoutePlanning.Application.Locations.Commands.GetRoute;
using RoutePlanning.Client.Web.Authorization;

namespace RoutePlanning.Client.Web.Api;

[Route("api/[controller]")]
[ApiController]
[Authorize(nameof(TokenRequirement))]
public sealed class RoutesController : ControllerBase
{
    private readonly IMediator mediator;

    public RoutesController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("[action]")]
    public Task<string> Health()
    {
        return Task.FromResult("Health Check Succesfull");
    }

    [HttpPost("[action]")]
    public async Task AddTwoWayConnection(CreateTwoWayConnectionCommand command)
    {
        await mediator.Send(command);
    }

    [HttpPost("[action]")]
    public async Task<RouteInfoDTO> GetRoute(GetRouteRequest request, CancellationToken cancellationToken)
    {
        return await mediator.Send(request, cancellationToken);
    }

    [HttpGet("[action]")]
    public async Task<string> TryHealth()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://wa-eit-dk1.azurewebsites.net/");
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("token", "TheSecretApiToken");
        var response = await client.GetAsync("api/v1/Health");
        //var response = await client.PostAsJsonAsync("api/Routes/Getroute", new GetRouteRequest("Warsaw", "London", 2));
        var test = "";
        if (response.IsSuccessStatusCode)
        {
            var test1 = await response.Content.ReadFromJsonAsync(typeof(RouteInfoDTO));
            test = "3";
        }
        return test;
    }
}

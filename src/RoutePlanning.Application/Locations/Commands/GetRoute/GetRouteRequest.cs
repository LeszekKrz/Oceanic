using MediatR;
using Netcompany.Net.Cqs.Commands;
using Netcompany.Net.Cqs.Queries;

namespace RoutePlanning.Application.Locations.Commands.GetRoute;
public sealed record GetRouteRequest(string From, string To, string weight, string size, int ParcelType) : IQuery<RouteInfoDTO>;


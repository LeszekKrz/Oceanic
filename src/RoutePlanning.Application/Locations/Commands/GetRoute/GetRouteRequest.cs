using MediatR;
using Netcompany.Net.Cqs.Commands;
using Netcompany.Net.Cqs.Queries;

namespace RoutePlanning.Application.Locations.Commands.GetRoute;
public sealed record GetRouteRequest(string From, string To, int ParcelType) : IQuery<RouteInfoDTO>;


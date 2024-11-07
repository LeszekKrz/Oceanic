using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePlanning.Application.Locations.Queries.Distance;
public sealed record RouteDTO(int time, float cost, List<(string, string, int, float)> routeSegments);

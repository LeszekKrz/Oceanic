﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePlanning.Application.Locations.Commands.GetRoute;
public sealed record RouteInfoDTO(bool isAvailable, string from, string to, int time, float price);

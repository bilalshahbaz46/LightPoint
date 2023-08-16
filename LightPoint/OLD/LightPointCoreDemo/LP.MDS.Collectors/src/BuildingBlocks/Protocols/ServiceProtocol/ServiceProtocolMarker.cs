using Base.Marker;
using Microsoft.Extensions.DependencyInjection;
using ServiceProtocol.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProtocol
{
    public class ServiceProtocolMarker : BaseMarker
    {
        public override void init(IServiceCollection serviceProvider)
        {
            serviceProvider.AddTransient<IReferenceDataService, ReferenceDataService>(); 
        }
    }
}

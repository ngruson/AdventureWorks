using MediatR;
using System.Collections.Generic;

namespace AW.Services.ReferenceData.Core.Handlers.Territory.GetTerritories
{
    public class GetTerritoriesQuery : IRequest<List<Territory>>
    {
    }
}
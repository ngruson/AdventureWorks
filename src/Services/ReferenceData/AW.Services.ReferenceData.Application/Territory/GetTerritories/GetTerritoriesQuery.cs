using MediatR;
using System.Collections.Generic;

namespace AW.Services.ReferenceData.Application.Territory.GetTerritories
{
    public class GetTerritoriesQuery : IRequest<List<Territory>>
    {
    }
}
using AW.SharedKernel.Caching;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetContactTypes
{
    public class GetContactTypesQueryHandler : IRequestHandler<GetContactTypesQuery, List<ContactType>?>
    {
        private readonly ILogger<GetContactTypesQueryHandler> logger;
        private readonly ICache<ContactType> cache;

        public GetContactTypesQueryHandler(ILogger<GetContactTypesQueryHandler> logger, ICache<ContactType> cache) => (this.logger, this.cache) = (logger, cache);

        public async Task<List<ContactType>?> Handle(GetContactTypesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all contact types from cache");
            return await cache.GetData();
        }
    }
}
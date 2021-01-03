using AutoMapper;
using AW.Core.Abstractions.Api.ContactTypeApi;
using AW.Core.Abstractions.Api.ContactTypeApi.ListContactTypes;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AW.Infrastructure.Api.WCF
{
    public class ContactTypeServiceAdapter : IContactTypeApi
    {
        private readonly ILogger<ContactTypeServiceAdapter> logger;
        private readonly IMapper mapper;
        private readonly ContactTypeService.IContactTypeService contactTypeService;

        public ContactTypeServiceAdapter(
            ILogger<ContactTypeServiceAdapter> logger,
            IMapper mapper,
            ContactTypeService.IContactTypeService contactTypeService
        ) => (this.logger, this.mapper, this.contactTypeService) = (logger, mapper, contactTypeService);
        

        public async Task<ListContactTypesResponse> ListContactTypesAsync()
        {
            logger.LogInformation("Calling ListContactTypes operation of ContactType web service");
            var response = await contactTypeService.ListContactTypesAsync();
            logger.LogInformation("ListContactTypes operation executed succesfully");

            return mapper.Map<ListContactTypesResponse>(response);
        }
    }
}
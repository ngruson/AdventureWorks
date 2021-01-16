using AutoMapper;
using AW.Core.Abstractions.Api.AddressTypeApi;
using AW.Core.Abstractions.Api.AddressTypeApi.ListAddressTypes;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AW.Infrastructure.Api.WCF
{
    public class AddressTypeServiceWCF : IAddressTypeApi
    {
        private readonly ILogger<AddressTypeServiceWCF> logger;
        private readonly IMapper mapper;
        private readonly AddressTypeService.IAddressTypeService addressTypeService;

        public AddressTypeServiceWCF(
            ILogger<AddressTypeServiceWCF> logger,
            IMapper mapper,
            AddressTypeService.IAddressTypeService addressTypeService
        ) => (this.logger, this.mapper, this.addressTypeService) = (logger, mapper, addressTypeService);
        

        public async Task<ListAddressTypesResponse> ListAddressTypesAsync()
        {
            logger.LogInformation("Calling ListAddressTypes operation of AddressType web service");
            var response = await addressTypeService.ListAddressTypesAsync();
            logger.LogInformation("ListAddressTypes operation executed succesfully");

            return mapper.Map<ListAddressTypesResponse>(response);
        }
    }
}
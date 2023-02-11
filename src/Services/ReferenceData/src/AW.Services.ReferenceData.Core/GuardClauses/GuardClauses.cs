using Ardalis.GuardClauses;
using AW.Services.ReferenceData.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace AW.Services.ReferenceData.Core.GuardClauses
{
    public static class GuardClauses
    {
        public static void AddressTypesNullOrEmpty(this IGuardClause guardClause, List<Entities.AddressType> addressTypes, ILogger logger)
        {
            if (addressTypes == null || addressTypes.Count == 0)
            {
                var ex = new AddressTypesNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void ContactTypesNullOrEmpty(this IGuardClause guardClause, List<Entities.ContactType> contactTypes, ILogger logger)
        {
            if (contactTypes == null || contactTypes.Count == 0)
            {
                var ex = new ContactTypesNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void CountriesNullOrEmpty(this IGuardClause guardClause, List<Entities.CountryRegion> countries, ILogger logger)
        {
            if (countries == null || countries.Count == 0 )
            {
                var ex = new CountriesNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void ShipMethodsNullOrEmpty(this IGuardClause guardClause, List<Entities.ShipMethod> shipMethods, ILogger logger)
        {
            if (shipMethods == null || shipMethods.Count == 0 )
            {
                var ex = new ShipMethodsNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void StatesProvincesNullOrEmpty(this IGuardClause guardClause, List<Entities.StateProvince> statesProvinces, ILogger logger)
        {
            if (statesProvinces == null || statesProvinces.Count == 0 )
            {
                var ex = new StatesProvincesNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void TerritoriesNullOrEmpty(this IGuardClause guardClause, List<Entities.Territory> territories, ILogger logger)
        {
            if ((territories == null) || territories.Count == 0)
            {
                var ex = new TerritoriesNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }
    }
}
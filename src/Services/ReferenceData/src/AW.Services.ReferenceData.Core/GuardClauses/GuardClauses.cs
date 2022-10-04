using Ardalis.GuardClauses;
using AW.Services.ReferenceData.Core.Exceptions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AW.Services.ReferenceData.Core.GuardClauses
{
    public static class GuardClauses
    {
        public static void AddressTypesNull(this IGuardClause guardClause, List<Entities.AddressType> addressTypes, ILogger logger)
        {
            if (addressTypes == null)
            {
                var ex = new AddressTypesNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void ContactTypesNull(this IGuardClause guardClause, List<Entities.ContactType> contactTypes, ILogger logger)
        {
            if (contactTypes == null)
            {
                var ex = new ContactTypesNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void CountriesNull(this IGuardClause guardClause, List<Entities.CountryRegion> countries, ILogger logger)
        {
            if (countries == null)
            {
                var ex = new CountriesNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void ShipMethodsNull(this IGuardClause guardClause, List<Entities.ShipMethod> shipMethods, ILogger logger)
        {
            if (shipMethods == null)
            {
                var ex = new ShipMethodsNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void StatesProvincesNull(this IGuardClause guardClause, List<Entities.StateProvince> statesProvinces, ILogger logger)
        {
            if (statesProvinces == null)
            {
                var ex = new StatesProvincesNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void TerritoriesNull(this IGuardClause guardClause, List<Entities.Territory> territories, ILogger logger)
        {
            if (territories == null)
            {
                var ex = new TerritoriesNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }
    }
}
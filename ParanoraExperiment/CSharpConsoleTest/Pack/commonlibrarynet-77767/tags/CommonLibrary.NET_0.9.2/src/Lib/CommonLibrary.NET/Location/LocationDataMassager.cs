using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.DomainModel;


namespace CommonLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class LocationDataMassager
    {
        /// <summary>
        /// Massage the address by setting it's cityid, stateid, countryid 
        /// from the city, state, country name.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="entityAction"></param>
        public static void Massage(Address address, EntityAction entityAction)
        {
            List<string> errors = new List<string>();
            CityLookUp cityLookup = IocContainer.GetObject<ICityDao>("cityDao").GetLookUp();
            StatesLookUp stateLookup = IocContainer.GetObject<IStateDao>("stateDao").GetLookUp();
            CountryLookUp countryLookup = IocContainer.GetObject<ICountryDao>("countryDao").GetLookUp();
            LocationUtils.ApplyCountry(address, countryLookup, errors);
            LocationUtils.ApplyState(address, stateLookup, errors);
            LocationUtils.ApplyCity(address, cityLookup, stateLookup, countryLookup);
        }
    }
}

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;


using CommonLibrary.Tests;

namespace CommonLibrary.Tests.Location
{
    /*
    [TestFixture]
    public class LocationServiceTests
    {
        private DataAccessObjectBuilder _daoBuilder;


        private LocationService GetLocationService()
        {
            _daoBuilder = new DataAccessObjectBuilder(true);
            IZipCodeDao zipCodeDao = _daoBuilder.GetZipCodeDao();
            IStateDao stateDao = _daoBuilder.GetStateDao();
            ILocationShortNameDao shortNameDao = _daoBuilder.GetShortNameDao();
            ICountryDao countryDao = _daoBuilder.GetCountryDao();
            ICityDao cityDao = _daoBuilder.GetCityDao();
            return new LocationService(countryDao, stateDao, cityDao, shortNameDao);
        }
        

        [Ignore]
        public void CanParseZip()
        {
            ILocationService locationService = GetLocationService();

            LocationLookUpResult lookupResult = locationService.Parse("11375");
            Assert.IsTrue(lookupResult.IsLookUpByZip);
            Assert.IsTrue(lookupResult.IsValid);
            Assert.IsTrue(lookupResult.Zip == "11375");

            lookupResult = locationService.Parse("07024");
            Assert.IsTrue(lookupResult.IsLookUpByZip);
            Assert.IsTrue(lookupResult.IsValid);
            Assert.IsTrue(lookupResult.Zip == "07024");
        }


        [Test]
        public void CanParseCityAllLowerCase()
        {
            ILocationService locationService = GetLocationService();
            LocationLookUpResult lookupResult = locationService.Parse("bronx");

            Assert.IsTrue(lookupResult.LookUpType == LocationLookUpType.City);
            Assert.IsTrue(lookupResult.IsValid);
            Assert.AreEqual(lookupResult.State, "New York");
            Assert.AreEqual(lookupResult.City, "Bronx");
            Assert.AreEqual(lookupResult.StateAbbr, "NY");
            Assert.AreEqual(lookupResult.StateId, DAOConstantsState.StateId_NY);
            Assert.AreEqual(lookupResult.CountryId, LocationConstants.CountryId_USA);
        }


        [Test]
        public void CanParseCityAllCAPS()
        {
            ILocationService locationService = GetLocationService();
            LocationLookUpResult lookupResult = locationService.Parse("BRONX");

            Assert.IsTrue(lookupResult.LookUpType == LocationLookUpType.City);
            Assert.IsTrue(lookupResult.IsValid);
            Assert.AreEqual(lookupResult.State, "New York");
            Assert.AreEqual(lookupResult.City, "Bronx");
            Assert.AreEqual(lookupResult.StateAbbr, "NY");
            Assert.AreEqual(lookupResult.StateId, DAOConstantsState.StateId_NY);
            Assert.AreEqual(lookupResult.CountryId, LocationConstants.CountryId_USA);
        }


        [Test]
        public void CanParseKnownCityInEuropeWithMixedCase()
        {
            ILocationService locationService = GetLocationService();
            LocationLookUpResult lookupResult = locationService.Parse("veNice");

            Assert.IsTrue(lookupResult.LookUpType == LocationLookUpType.City);
            Assert.IsTrue(lookupResult.IsValid);
            Assert.AreEqual(lookupResult.State, "ItalianState1");
            Assert.AreEqual(lookupResult.City, "Venice");
            Assert.AreEqual(lookupResult.StateAbbr, "IT1");
            Assert.AreEqual(lookupResult.StateId, DAOConstantsState.StateId_ItalyState1);
            Assert.AreEqual(lookupResult.CountryId, DAOConstantsCountry.CountryId_Italy);
        }


        [Test]
        public void CanParseByCityStateFullName()
        {
            ILocationService locationService = GetLocationService();
            LocationLookUpResult lookupResult = locationService.Parse("nEw YOrk,new york");

            Assert.IsTrue(lookupResult.IsLookUpByCity);
            Assert.IsTrue(lookupResult.IsValid);
            Assert.AreEqual(lookupResult.State, "New York");
            Assert.AreEqual(lookupResult.City, "New York");
            Assert.AreEqual(lookupResult.StateAbbr, "NY");
            Assert.AreEqual(lookupResult.StateId, DAOConstantsState.StateId_NY);
            Assert.AreEqual(lookupResult.CountryId, LocationConstants.CountryId_USA);
        }


        [Test]
        public void CanParseByCityCountryInEuropeFullName()
        {
            ILocationService locationService = GetLocationService();
            LocationLookUpResult lookupResult = locationService.Parse(" Venice , Italy ");

            Assert.IsTrue(lookupResult.IsLookUpByCity);
            Assert.IsTrue(lookupResult.IsValid);            
            Assert.AreEqual(lookupResult.City, "Venice");
            Assert.AreEqual(lookupResult.CountryId, DAOConstantsCountry.CountryId_Italy);
        }


        [Test]
        public void CanParseByCityStateInEuropeWithInvalidCountry()
        {
            ILocationService locationService = GetLocationService();
            LocationLookUpResult lookupResult = locationService.Parse(" Venice , United States ");

            Assert.IsTrue(lookupResult.IsLookUpByCityCountry);
            Assert.IsTrue(lookupResult.IsValid);
            Assert.AreEqual(lookupResult.City, "Venice");
            Assert.AreEqual(lookupResult.CountryId, DAOConstantsCountry.CountryId_USA);
        }


        [Test]
        public void CanParseByKnownCityInWithDifferentState()
        {
            ILocationService locationService = GetLocationService();
            LocationLookUpResult lookupResult = locationService.Parse(" bronx , Connecticut ");

            Assert.IsTrue(lookupResult.IsLookUpByCityState);
            Assert.IsTrue(lookupResult.IsValid);
            Assert.AreEqual(lookupResult.City, "bronx");
            Assert.AreEqual(lookupResult.StateId, DAOConstantsState.StateId_CT);
            Assert.AreEqual(lookupResult.CountryId, DAOConstantsCountry.CountryId_USA);
        }


        [Test]
        public void CanParseByKnownCityWithStateResultingInSimpleCitySearch()
        {
            ILocationService locationService = GetLocationService();
            LocationLookUpResult lookupResult = locationService.Parse(" bronx , New York ");

            Assert.IsTrue(lookupResult.IsLookUpByCity);
            Assert.IsTrue(lookupResult.IsValid);
            Assert.AreEqual(lookupResult.City, "Bronx");
            Assert.AreEqual(lookupResult.StateId, DAOConstantsState.StateId_NY);
            Assert.AreEqual(lookupResult.CountryId, DAOConstantsCountry.CountryId_USA);
        }


        [Test]
        public void CanParseByStateAbbr()
        {
            ILocationService locationService = GetLocationService();

            LocationLookUpResult lookupResult = locationService.Parse("ny");
            Assert.IsTrue(lookupResult.IsLookUpByState);
            Assert.IsTrue(lookupResult.IsValid);
            Assert.AreEqual(lookupResult.State.ToLower(), "new york");
            Assert.AreEqual(lookupResult.StateId, DAOConstantsState.StateId_NY);
        }


        [Test]
        public void CanParseByStateFullName()
        {
            ILocationService locationService = GetLocationService();
            LocationLookUpResult lookupResult = locationService.Parse("nEw Jersey");

            Assert.IsTrue(lookupResult.IsLookUpByState);
            Assert.IsTrue(lookupResult.IsValid);
            Assert.AreEqual(lookupResult.State.ToLower(), "new jersey");
            Assert.AreEqual(lookupResult.StateId, DAOConstantsState.StateId_NJ);
        }


        [Test]
        public void CanParseByCityStateAbbr()
        {
            ILocationService locationService = GetLocationService();
            LocationLookUpResult lookupResult = locationService.Parse("nEw YOrk,ny");

            Assert.IsTrue(lookupResult.IsLookUpByCity);
            Assert.IsTrue(lookupResult.IsValid);
            Assert.AreEqual(lookupResult.State, "New York");
            Assert.AreEqual(lookupResult.City, "New York");
            Assert.AreEqual(lookupResult.StateAbbr, "NY");
            Assert.AreEqual(lookupResult.StateId, DAOConstantsState.StateId_NY);
            Assert.AreEqual(lookupResult.CountryId, LocationConstants.CountryId_USA);
        }


        [Test]
        public void CanParseByCountry()
        {
            ILocationService locationService = GetLocationService();
            LocationLookUpResult lookupResult = locationService.Parse("Italy");

            Assert.IsTrue(lookupResult.IsLookUpByCountry);
            Assert.IsTrue(lookupResult.IsValid);
            Assert.AreEqual(lookupResult.Country, "Italy");
            Assert.AreEqual(lookupResult.CountryId, DAOConstantsCountry.CountryId_Italy);
        }
    }
     */
}

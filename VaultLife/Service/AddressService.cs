using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;

namespace Vaultlife.Service
{
    public class AddressService
    {
        private VaultLifeApplicationEntities db;
        public AddressService(VaultLifeApplicationEntities db)
        {
            this.db = db;
        }

        public IEnumerable<Country> getCountries(int? countryId)
        {
            IEnumerable<Country> countries;
            if (countryId != null)
            {
                countries = db.Countries.AsEnumerable().Where(x => x.CountryID == countryId);
            }
            else
            {
                countries = db.Countries.AsEnumerable();
            }
            return countries;
        }

        public IEnumerable<CountryState> getStates(int? countryId)
        {
            AddressService service = new AddressService(db);
            IEnumerable<CountryState> states = db.CountryStates.AsEnumerable().Where(x=> x.CountryID == countryId);
            return states;
        }

        public IEnumerable<CountryCity> getCities(int? countryId, int? stateId)
        {
            IEnumerable<CountryCity> cities;
            if (stateId != null) {
                cities = db.CountryCities.Where(x => x.CountryID == countryId && x.StateID == stateId);
            }
            else
            {
                cities = db.CountryCities.Where(x => x.CountryID == countryId);
            }

            return cities;
        }

        public Country getCountry(String country)
        {
            return db.Countries.First(x => x.CountryName == country);
        }

        public CountryState getState(String state)
        {
            return db.CountryStates.FirstOrDefault(x => x.StateName == state);
        }

        public CountryCity getCity(String city)
        {
            return db.CountryCities.FirstOrDefault(x => x.CityName == city);
        }

    }
}
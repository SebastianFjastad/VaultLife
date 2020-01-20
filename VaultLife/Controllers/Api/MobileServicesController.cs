using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Xml;
using Vaultlife.Dao;
using Vaultlife.Managers;
using Vaultlife.Models;
using Vaultlife.Models.Mobile;
using Vaultlife.Service;

namespace Vaultlife.Controllers.Api
{
    public class MobileServicesController : ApiController
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        [HttpPost]
        [ActionName("GetCountry")]
        [AllowAnonymous]
        public List<dynamic> GetCountry(FormDataCollection formData)
        {
            AddressService service = new AddressService(db);

            int? country = null;
            
            if (formData != null )
            {
                string countryString = formData.Get("Country");
                if (countryString != null)
                {
                    country = Convert.ToInt32(countryString);
                }
            }
            List<Country> countries = service.getCountries(country).ToList();
            var s = countries.Select(x => new { CountryID = x.CountryID, CountryName = x.CountryName });
            return s.ToList<dynamic>();
           
        }

        [HttpPost]
        [ActionName("GetStates")]
        [AllowAnonymous]
        public List<dynamic> GetStates(FormDataCollection formData)
        {
            AddressService service = new AddressService(db);

            int? country = null;

            if (formData != null)
            {
                string countryString = formData.Get("Country");
                if (countryString != null)
                {
                    country = Convert.ToInt32(countryString);
                }
            }
            List<CountryState> states = service.getStates(country).ToList();
            var s = states.Select(x => new { StateID = x.StateID, StateName = x.StateName }); 
            return s.ToList<dynamic>();
        }

        [HttpPost]
        [ActionName("GetBillStates")]
        [AllowAnonymous]
        public List<dynamic> GetBillStates(FormDataCollection formData)
        {
            AddressService service = new AddressService(db);

            int? country = null;

            if (formData != null)
            {
                string countryString = formData.Get("CountryBill");
                if (countryString != null)
                {
                    country = Convert.ToInt32(countryString);
                }
            }
            List<CountryState> states = service.getStates(country).ToList();
            var s = states.Select(x => new { StateID = x.StateID, StateName = x.StateName });
            return s.ToList<dynamic>();
        }

        [HttpPost]
        [ActionName("GetCities")]
        [AllowAnonymous]
        public List<dynamic> GetCities(FormDataCollection formData)
        {
            AddressService service = new AddressService(db);

            int? country = null;
            int? state = null;
            if (formData != null)
            {
                string countryString = formData.Get("Country");
                if (countryString != null)
                {
                    country = Convert.ToInt32(countryString);
                }

                string stateString = formData.Get("State");
                if ( stateString != null)
                {
                    state = Convert.ToInt32(stateString);
                }
            }
            IEnumerable<CountryCity> cities = service.getCities(country, state);
            var s = cities.Select(x => new { CityID = x.CityID, CityName = x.CityName });
            return s.ToList<dynamic>();
        }

        [HttpPost]
        [ActionName("GetBillCities")]
        [AllowAnonymous]
        public List<dynamic> GetBillCities(FormDataCollection formData)
        {
            AddressService service = new AddressService(db);

            int? country = null;
            int? state = null;
            if (formData != null)
            {
                string countryString = formData.Get("CountryBill");
                if (countryString != null)
                {
                    country = Convert.ToInt32(countryString);
                }

                string stateString = formData.Get("StateBill");
                if ( stateString != null)
                {
                    state = Convert.ToInt32(stateString);
                }
            }
            IEnumerable<CountryCity> cities = service.getCities(country, state);
            var s = cities.Select(x => new { CityID = x.CityID, CityName = x.CityName });
            return s.ToList<dynamic>();
        }

        [HttpPost]
        [ActionName("ProcessUpgrade")]
        [Authorize]
        public dynamic ProcessUpgrade(FormDataCollection formData)
        {
            if (formData != null)
            {
                List<string> errors = new List<string>();

                string CustIP = formData.Get("CustIP");
                string CardNumber = formData.Get("CardNumber");
                string CVCNumber = formData.Get("CVCNumber");
                string ExpiryDateY = formData.Get("ExpiryDateY");
                string ExpiryDateM = formData.Get("ExpiryDateM");
                string NameOnCard = formData.Get("NameOnCard");
                string MembershipSubscriptionType = formData.Get("MembershipSubscriptionType");
                int membershipSubscriptionType = 0;

                if (string.IsNullOrEmpty(CustIP))
                {
                    errors.Add("Customer IP is required.");
                }

                if (string.IsNullOrEmpty(CardNumber))
                {
                    errors.Add("CardNumber is required.");
                }

                if (string.IsNullOrEmpty(CVCNumber))
                {
                    errors.Add("CVCNumber is required.");
                }

                if (string.IsNullOrEmpty(ExpiryDateY))
                {
                    errors.Add("ExpiryDateY is required.");
                }

                if (string.IsNullOrEmpty(ExpiryDateM))
                {
                    errors.Add("ExpiryDateM is required.");
                }

                if (string.IsNullOrEmpty(NameOnCard))
                {
                    errors.Add("NameOnCard is required.");
                }

                if (string.IsNullOrEmpty(MembershipSubscriptionType))
                {
                    errors.Add("MembershipSubscriptionType is required.");
                }

                if (errors.Count > 0)
                {
                    var e = Enumerable.Range(0, errors.Count).ToDictionary(x => "Error" + x, x => errors[x]);
                    return e;
                }

                bool convertMembershipSusbTypeOutcome = Int32.TryParse(MembershipSubscriptionType, out membershipSubscriptionType);
                if (!convertMembershipSusbTypeOutcome)
                {
                    return new { Error = "MembershipSubscriptionType is invalid." };
                }


                PaymentsModel paymentsModel = new PaymentsModel();
                paymentsModel.CardNumber = CardNumber;
                paymentsModel.CVCNumber = CVCNumber;
                paymentsModel.ExpiryDateM = ExpiryDateM;
                paymentsModel.ExpiryDateY = ExpiryDateY;
                paymentsModel.NameOnCard = NameOnCard;

                Vaultlife.Service.PaymentService paymentService = new Vaultlife.Service.PaymentService(new VaultLifeApplicationEntities());
                bool success = paymentService.pay(paymentsModel, membershipSubscriptionType, User.Identity.Name, this.GetIPAddress(), CustIP);

                return new { PaymentSuccessful = "" + success };
            }

            return new { Error = "No data received." };
        }

        [HttpPost]
        [ActionName("GetMembershipTypes")]
        [AllowAnonymous]
        public List<dynamic> GetMembershipTypes(FormDataCollection formData)
        {
            var s = db.MemberSubscriptionTypes.Select(x => new { MemberSubscriptionTypeID = x.MemberSubscriptionTypeID, MemberSubscriptionTypeDescription = x.MemberSubscriptionTypeDescription });
            return s.ToList<dynamic>();
        }

        [HttpGet]
        [ActionName("RecentWinners")]
        [AllowAnonymous]
        public dynamic RecentWinners()
        {
            GameDao gd = new GameDao(db);
            return gd.findRecentWinners();
        }

        private string GetIPAddress()
        {
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            return ipAddress.ToString();
        }
    }
}
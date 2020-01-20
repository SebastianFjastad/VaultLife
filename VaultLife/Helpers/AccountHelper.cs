using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;
using Vaultlife.ViewModels;

namespace Vaultlife.Helpers
{
    public class AccountHelper
    {
        public AccountHelper()
        {
        }

        // member
        public Member ToMember(AccountViewModel avm)
        {
            Member member = new Member();

            member.EmailAddress = avm.Email;
            member.IdentityType = "ID Document";
            member.MemberSubscriptionTypeID = Convert.ToInt32(1);  //TODO very suckey... create enum to represent db primary keys
            member.FirstName = avm.FirstName;
            member.LastName = avm.LastName;
            member.TelephoneMobile = avm.MobileNumber;
            member.MemberAcquisitionCampaignCode = avm.PromoCode;
            member.CountryID = Convert.ToInt32(avm.CountryID);
            member.StateID = avm.StateID;
            member.CityID = avm.CityID;
            member.AddressID = -1;
            member.DateInserted = DateTime.Today;
            member.DateUpdated = DateTime.Today;
            member.USR = avm.Email;
            member.ActiveIndicator = true;
            member.OwnerID = avm.OwnerID;
            // Setting Date Of Birth to Minimum Date...
            member.DateOfBirth = DateTime.MinValue;
            return member;

        }
    }
}
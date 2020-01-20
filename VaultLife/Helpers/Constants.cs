using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;

namespace Vaultlife.Helpers
{
    public static class Constants
    {
        //public static string connectionString = System.Configuration.confi


        #region -- Page Redirection --

        //Admin Links
        public static string LoginRedirect = "~/POLLogin.aspx";
        public static string AdminDefaultRedirect = "~/Admin/Default.aspx";
        public static string AdminCreateUserRedirect = "~/Admin/UserDetails.aspx";
        public static string AdminCreateUserCountryRedirect = "~/Admin/CountryAdminUserDetails.aspx";
        public static string AdminManageCountryAdminUserRedirect = "~/Admin/ManageCountryAdmin.aspx";
        public static string AdminManageUserRedirect = "~/Admin/ManageUsers.aspx";
        public static string AdminAddItemsRedirect = "~/Admin/AddItems.aspx";
        public static string AdminManageItemsRedirect = "~/Admin/ManageItems.aspx";
        public static string AdminAssignItemsScheduleRedirect = "~/Admin/ScheduleItem.aspx";
        public static string AdminAssignItemsStandoffRedirect = "~/Admin/StandoffItem.aspx";
        public static string AdminAssignItemsImages = "~/Admin/AddImages.aspx";
        public static string AdminAddEvents = "~/Admin/AddEvents.aspx";
        public static string AdminManageEvents = "~/Admin/ManageEvents.aspx";
        public static string AdminAddEventsImages = "~/Admin/AddEventImages.aspx";
        public static string AdminPostEventsImages = "~/Admin/AddPostEventImages.aspx";
        public static string AdminAddScreensRedirect = "~/Admin/AddScreens.aspx";
        public static string AdminManageCountries = "~/Admin/ManageCountries.aspx";
        public static string AdminAssignCountryImages = "~/Admin/AddCountryImages.aspx";
        public static string AdminManageLinks = "~/Admin/ManageLinks.aspx";

        //Public Menu Links
        public static string PublicDefaultRedirect = "~/Home.aspx";
        public static string ShowroomItemsRedirect = "~/Forms/ShowroomItems.aspx";
        public static string ItemsByCategoryRedirect = "~/Forms/ItemsByCategory.aspx";
        public static string HowItWorksRedirect = "~/Forms/HowItWorks.aspx";
        public static string ContactUsRedirect = "~/Forms/ContactUs.aspx";
        public static string MyAccountRedirect = "~/Forms/MyAccount.aspx";
        public static string PurchasesRedirect = "~/Forms/Purchases.aspx";
        public static string AboutUsRedirect = "~/Forms/AboutUs.aspx";
        public static string WeeklyNewsRedirect = "~/Forms/WeeklyNews.aspx";
        public static string UpgradeSubscription = "~/Forms/UpgradeSubscription.aspx";
        public static string FAQRedirect = "~/Forms/FAQ.aspx";
        public static string ShoppingRedirect = "~/Forms/VaultShopping.aspx";
        public static string PublicIndexRedirect = "~/Index.aspx";
        public static string TOCRedirect = "~/Forms/TOC.aspx";
       

        //Additional Links
        public static string ItemDetailsRedirect = "~/Forms/ItemDetails.aspx";
        public static string UpgradeItemRedirect = "~/Forms/UpgradeSubscription.aspx";
        public static string ProcessPaymentRedirect = "~/Forms/ProcessPayment.aspx";
        public static string JoinUsRedirect = "~/Squeeze.aspx";//"~/Forms/JoinUs.aspx";
        public static string RegistrationRedirect = "~/Forms/Registration.aspx";
        public static string MessageRedirect = "~/Forms/Message.aspx";
        public static string SqueezeRedirect = "~/Squeeze.aspx";


        public static string TheVaultRedirect = "~/Forms/TheVault.aspx";

        //Events Link
        public static string EventDetailsRedirect = "~/Forms/EventDetails.aspx";
        public static string BookEventRedirect = "~/Forms/BookEvent.aspx";
        public static string CancelBookingRedirect = "~/Forms/CancelBooking.aspx";
        public static string PastEventsRedirect = "~/Forms/PastEvents.aspx";
        public static string BookingHistoryRedirect = "~/Forms/BookingHistory.aspx";
        public static string GalleryRedirect = "~/Forms/Gallery.aspx";

        #endregion


        #region -- Admin Section Session Names --
        public static string SessionAdminSubscription = "SessionAdminSubscription";
        public static string SessionAdminSubscriptionID = "SessionAdminSubscriptionID";
        public static string SessionAdminCategories = "SessionAdminCategories";
        public static string SessionAdminCategoriesID = "SessionAdminCategoriesID";
        public static string SessionAdminCoutriesID="SessionAdminCountriesID";
        public static string SessionAdminUsers = "SessionAdminUsers";

        public static string SessionAdminSchedule = "SessionAdminCategories";
        public static string SessionAdminScheduleID = "SessionAdminCategoriesID";

        public static string SessionAdminDMID = "SessionAdminDMID";
        public static string SessionAdminDMLIST = "SessionAdminDMLIST";

        public static string SessionScheduleParentCountriesListing = "SessionScheduleParentCountriesListing";
        public static string SessionScheduleChildRegionsListing = "SessionScheduleChildCountriesListing";

        public static string SessionAdminUsersCountryAdmin = "SessionAdminUsersCountryAdmin";
        public static string SessionUserScreenAssignment = "SessionUSerScreenAssignment";
        public static string SessionCodeID = "SessionCodeID";

        public static string SessionAdminScheduleCountry = "SessionAdminScheduleCountry";
        #endregion


        #region -- Session Constants --

        public static string SessionLoggedInUser = "LoggedInUser";

        public static string LoggedInUserIDForProfile = "LoggedInUserIDForProfile";

        //For Audit Logging
        public static string SessionLoggedInUserType = "LoggedInUserType";

        //For LockoutBuy

        public static string MasterPassword = "3F7619176B6B4FB89632A4989B7DA3B";

        #endregion

        public static string SessionUser = "User";
        public static string SessionSubscription = "Subscription";
        public static string SessionUserSubscription = "UserSubscription";
        public static string SessionPaymentHandler = "PaymentHandler";


        public static string SessionItemByCategory = "SessionItemByCategory";
        public static string SessionUserPurchases = "SessionUserPurchases";
        public static string SessionShowroomItems = "SessionShowroomItems";
        public static string SessionIndexItems = "SessionIndexItems";
        public static string SessionHome = "SessionHome";

        public static string SessionPaymentDetails = "SessionPaymentDetails";
        public static string SessionVaultSoldQueryString = "SessionVaultSoldQueryString";

        public static string SessionEvents = "SessionEvents";
        public static string SessionPastEvents = "SessionPastEvents";
        public static string SessionEventQueryString = "SessionEventQueryString";
        public static string SessionBookingHistory = "SessionBookingHistory";
        public static string SessionHistoryVaultItem = "HistoryVaultItem";
        public static string SessionDetailedBookingHistory = "SessionDetailedBookingHistory";
        public static string SessionGeoLocation = "SessionGeoLocation";
        public static string SessionGeoCode = "SessionGeoCode";


        public static string SessionAssets = "SessionAssets";
        public static string SessionAssetsValues = "SessionAssetsValues";


        #region -- P/w for cryptography --
        public static string Password = "E830258FD794480CB65BD42A3EC6CBE7";
        #endregion

        #region -- Error Message --
        //public static string Execption = "Som";

        public static string MessageUserRegistraion = "CONGRATULATIONS ! your registration process is complete, please check your mails for Login Details.";
        public static string MessageItemSold = "CONGRATULATIONS ! YOUR PAYMENT IS APPROVED.";
        public static string MessageItemSoldError = "YOUR PAYMENT WAS NOT APPROVED.";
        public static string MessageItemSoldErrorGuide = "PLEASE MAKE SURE YOUR DETAILS ARE CORRECT AND CONTACT YOUR BANK.";
        public static string MessageItemSoldViewDetails = "EMAIL CONFIRMATION SENT REGARDING YOUR PURCHASE MADE";

        //Tycoon Status        was Titanium
        //Luxury Status        was Platinum
        //Loving Life Status   was Gold
        //Free Status

        public static string MessageCurrentlyFree = "<span class='spFree'>You are currently on Free Status.</span>";
        public static string MessageFreeToGold = "<span class='spGold'>For {0}, upgrade to Loving Life Status.</span>";
        public static string MessageFreeToPlatinum = "<span class='spPlatinum'>For {0}, upgrade to Luxury Status.</span>";
        public static string MessageFreeToTitanium = "<span class='spTitanium'>For {0}, upgrade to Tycoon Status.</span>";

        public static string MessageCurrentlyGold = "<span class='spGold'>You are currently on Loving Life Status.</span>";
        public static string MessageGoldToPlatinum = "<span class='spPlatinum'>For {0}, upgrade to Luxury Status.</span>";
        public static string MessageGoldToTitanium = "<span class='spTitanium'>For {0}, upgrade to Tycoon Status.</span>";

        public static string MessageCurrentlyPlatinum = "<span class='spPlatinum'>You are currently on Luxury Status.</span>";
        public static string MessagePlatinumToTitanium = "<span class='spTitanium'>For {0}, upgrade to Tycoon Status.</span>";

        public static string MessageUpgradeFreeToGold = "<span class='spGold'>Loving Life status at {0}</span>";
        public static string MessageUpgradeFreeToPlatinum = "<span class='spPlatinum'>Luxury Status at {0}</span>";
        public static string MessageUpgradeFreeToTitanium = "<span class='spTitanium'>Tycoon Status at {0}</span>";

        public static string MessageUpgradeGoldToPlatinum = "<span class='spPlatinum'>Luxury Status at {0}</span>";
        public static string MessageUpgradeGoldToTitanium = "<span class='spTitanium'>Tycoon Status at {0}</span>";
        public static string MessageUpgradePlatinumToTitanium = "<span class='spTitanium'>Tycoon Status at {0}</span>";




        //public static string MessageCurrentlyFree = "<span class='spFree'>You are currently on FREE status.</span>";
        //public static string MessageFreeToGold = "<span class='spGold'>For R1 499, upgrade to GOLD.</span>";
        //public static string MessageFreeToPlatinum = "<span class='spPlatinum'>For R4 999, upgrade to PLATINUM.</span>";
        //public static string MessageFreeToTitanium = "<span class='spTitanium'>For R8 999, upgrade to TITANIUM.</span>";

        //public static string MessageCurrentlyGold = "<span class='spGold'>You are currently on GOLD status.</span>";
        //public static string MessageGoldToPlatinum = "<span class='spPlatinum'>For R3 500, upgrade to PLATINUM.</span>";
        //public static string MessageGoldToTitanium = "<span class='spTitanium'>For R7 500, upgrade to TITANIUM.</span>";

        //public static string MessageCurrentlyPlatinum = "<span class='spPlatinum'>You are currently on PLATINUM status.</span>";
        //public static string MessagePlatinumToTitanium = "<span class='spTitanium'>For R4 000, upgrade to TITANIUM.</span>";

        //public static string MessageUpgradeFreeToGold = "<span class='spGold'>Gold status at R1 499</span>";
        //public static string MessageUpgradeFreeToPlatinum = "<span class='spPlatinum'>Platinum status at R4 999</span>";
        //public static string MessageUpgradeFreeToTitanium = "<span class='spTitanium'>Titanium status at R8 999</span>";

        //public static string MessageUpgradeGoldToPlatinum = "<span class='spPlatinum'>Platinum status at R3 500</span>";
        //public static string MessageUpgradeGoldToTitanium = "<span class='spTitanium'>Titanium status at R7 500</span>";
        //public static string MessageUpgradePlatinumToTitanium = "<span class='spTitanium'>Titanium status at R4 000</span>";

        #endregion


    }



   
}



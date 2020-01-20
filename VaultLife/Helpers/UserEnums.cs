using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vaultlife.Helpers
{
    public static class UserEnums
    {

        // For Auditlogging
        public enum UserType { Normal, SiteAdmin, Marketing, Trial, CountryAdmin }

        public enum OperationType { Login, LogOut, LoginFail, UserCreationSuccessful, UserVerificationSuccessful, PaymentSuccessFull }

        public enum ItemType { VaultItem = 1, SurpriseItem = 2, StandOffItem = 3 }

        public enum SubscriptionType { Free = 0, Gold = 1, Platinum = 2, Titanium = 3 }

        public enum SubscriptionFees { Gold = 1499, Platinum = 4999, Titanium = 8999, Free = 0 }

        public enum SubscriptionUpgradeFees { GoldToPlatinum = 3500, GoldToTitanium = 7500, PlatinumToTitanium = 4000, FreeToGold = 1499, FreeToPlatinum = 4999, FreeToTitanium = 8999 }

        public enum TicketUpgrade { GoldToPlatinum = 2, GoldToTitanium = 8, PlatinumToTitanium = 6, FreeToGold = 2, FreeToPlatinum = 4, FreeToTitanium = 10 }

        public enum AllotedTicket { Free = 0, Gold = 2, Platinum = 4, Titanium = 10 }

        public enum DisplaySection { HomePage = 1, Showroom = 2, Categories = 3, Purchases = 4 }

        public enum IndexSection { Free = 0, Gold = 1, Platinum = 2, Titanium = 3, World = 4, All = 5 }

        public enum EventDisplaySection { HomePage = 1, PastEvents = 2 }

        public enum PaymentStatus { Approved = 1, Failed = 2 }

        public enum TransactionType { ItemPurchase = 1, RegistrationViaAdmin = 2, Registration = 3, SubscriptionUpgrade = 4, SubscriptionRenew = 5, StandoffSold = 6, EventTicketBooking = 7, RegistrationViaFB = 8, RegisterViaMobile = 9, ItemPurchaseViaAndroidDevice = 10, ItemPurchaseViaiOSDevice = 11, UpgradeViaAdmin = 12 }

        public enum UserSubscriptionOperationType { FirstTime = 0, Renewal = 1, Upgrade = 2, RegistrationViaFB = 3 }

        public enum Application { Vault = 1, Entertainment = 2, Emporium = 3 }

        public enum AuditLogOperation { RenewalNotice = 1, SubscriptionExpired = 2, UserDeactivationNotice = 3, UserDeactivation = 4, VaultItemNotice = 5 }

        public enum DeliveryStatus { TOBESCHEDULED, SCHEDULED, INTRANSIT, DELIVERED }

        //Web Service ENUM

        public enum RespnseMode { DATA = 1, VALIDATION = 2, ERROR = 3 }

    }
}
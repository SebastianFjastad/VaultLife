using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PaymentService
{
    [XmlRoot("request")]
    public class PurchaseTransactionRequest
    {
        /* Setcom implementation */
        public string CO_ID { get; set; } // CO_ID==TerminalID
        public string OUTLET { get; set; }
        public string Reference { get; set; }
        public string CC_Amount { get; set; }
        public string CCName { get; set; }
        public string CCNumber { get; set; }
        public string ExYear { get; set; } // expects CCYY
        public string ExMonth { get; set; }
        public string CCCVV { get; set; }
        public string PayPeriod { get; set; }

        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Consistent { get; set; } // consistent key
        public string buyer_id { get; set; }
        public string buyer_session_id { get; set; }
        // ship to details
        public string ship_title { get; set; }
        public string ship_first_name { get; set; }
        public string ship_last_nmae { get; set; }
        public string ship_street1 { get; set; }
        public string ship_street2 { get; set; }
        public string ship_city { get; set; }
        public string ship_state { get; set; }
        public string ship_zip { get; set; }
        public string ship_country { get; set; }
        public string ship_phone { get; set; }
        // bill to details
        public string bill_title { get; set; }
        public string bill_first_name { get; set; }
        public string bill_last_name { get; set; }
        public string bill_street1 { get; set; }
        public string bill_street2 { get; set; }
        public string bill_city { get; set; }
        public string bill_state { get; set; }
        public string bill_zip { get; set; }
        public string bill_country { get; set; }
        public string bill_phone { get; set; }
        // ip & 3ds redirect URL
        public string ip_address { get; set; }
        public string redirect_url { get; set; }


        // Transaction date and time.
        [XmlIgnore]
        public DateTime transactionDateTime { get; set; }
        [XmlIgnore]
        public int? MemberInGameID { get; set; }
        [XmlIgnore]
        public int paymentTransactionID { get; set; }

        /* No UDF's for setcom
         * public void setUdf1(string productIdentifier, string memberOwner, string productOwner)
        {
            this.udf1 = String.Format("{0}|{1}|{2}", productIdentifier, memberOwner, productOwner);
        }

        public void setUdf2(string purchasePrice, string costPrice, string gameID)
        {
            this.udf2 = String.Format("{0}|{1}|{2}", purchasePrice, costPrice, gameID);
        }
        public void setUdf3(string voucherNumber)
        {
            this.udf3 = String.Format("{0}", voucherNumber);
        }
        public void setUdf4(string serialNumber, string deliveryMethod)
        {
            this.udf4 = String.Format("{0}|{1}", serialNumber, deliveryMethod);
        }
        public void setUdf5(string productName)
        {
            this.udf5 = String.Format("{0}", productName);
        }
        */
    }
}
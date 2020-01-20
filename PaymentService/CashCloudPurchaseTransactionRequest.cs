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
    public class CashCloudPurchaseTransactionRequest
    {
        public string terminalid { get; set; }
        public string password { get; set; }
        public string action { get; set; }
        public string card { get; set; }
        public string cvv2 { get; set; }
        public string expYear { get; set; }
        public string expMonth { get; set; }
        public string member { get; set; }
        public string currencyCode { get; set; }
        public string address { get; set; }

        public string city { get; set; }
        public string stateCode { get; set; }
        public string zip { get; set; }
        public string countryCode { get; set; }
        public string Email { get; set; }
        public string amount { get; set; }
        public string trackid { get; set; }
        public string merchantip { get; set; }
        public string customerip { get; set; }
      

        public string udf1 { get; set; }//Product  Identifier : up to 10 digit product id, can be blank
                                        //Member owner : 3 alphanumeric characters code 
                                        //Product owner : 3  alphanumeric characters code, can be blank

        //00|999999999|9999999999|xxx|yyy
        public string udf2 { get; set; }//PurchasePrice(7)|CostPrice(7)|GameID(10)
        //9999999|9999999|9999999999
        public string udf3 { get; set; }//Voucher number 30 digits
        public string udf4 { get; set; }//SerialNumber(12 digits) | Delivery method (2 digits) meaning:
        //0 courier, 1 hand delivered, 3 Electronic 4 a VAULTLife representative will contact you for redemption
        public string udf5 { get; set; }//Product Name

        // Transaction date and time.
        [XmlIgnore]
        public DateTime transactionDateTime { get; set; }
        [XmlIgnore]
        public int? MemberInGameID { get; set; }
        [XmlIgnore]
        public int paymentTransactionID { get; set; }

        public void setUdf1(string productIdentifier, string memberOwner, string productOwner)
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

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PaymentService
{
    public class SetcomPurchase
    {
        public PurchaseTransactionResponse DoPayment(PurchaseTransactionRequest requestTx, string paymentGatewayURL)
        {
            // Build the data string
            StringBuilder sb_purchase_data = new StringBuilder();
            sb_purchase_data.Append(string.Format("CO_ID={0}", requestTx.CO_ID));
            sb_purchase_data.Append(string.Format("&OUTLET={0}", requestTx.OUTLET));
            sb_purchase_data.Append(string.Format("&Reference={0}", requestTx.Reference));
            sb_purchase_data.Append(string.Format("&CC_Amount={0}", requestTx.CC_Amount));
            sb_purchase_data.Append(string.Format("&CCName={0}", requestTx.CCName));
            sb_purchase_data.Append(string.Format("&CCNumber={0}", requestTx.CCNumber));
            sb_purchase_data.Append(string.Format("&ExYear={0}", requestTx.ExYear));
            sb_purchase_data.Append(string.Format("&ExMonth={0}", requestTx.ExMonth));
            sb_purchase_data.Append(string.Format("&CCCVV={0}", requestTx.CCCVV));

            if (requestTx.Consistent != "")
            {
                sb_purchase_data.Append(string.Format("&Consistent={0}", requestTx.Consistent));
            }

            // Optional SETCOM fields
            if (requestTx.MobileNumber != "")
            {
                sb_purchase_data.Append(string.Format("&MobileNumber={0}", requestTx.MobileNumber));
            }

            if (requestTx.MobileNumber != "")
            {
                sb_purchase_data.Append(string.Format("&buyer_id={0}", requestTx.buyer_id));
            }

            if (requestTx.bill_first_name != "")
            {
                sb_purchase_data.Append(string.Format("&bill_first_name={0}", requestTx.bill_first_name));
            }

            if (requestTx.bill_last_name != "")
            {
                sb_purchase_data.Append(string.Format("&bill_last_name={0}", requestTx.bill_last_name));
            }

            if (requestTx.bill_phone != "")
            {
                sb_purchase_data.Append(string.Format("&bill_phone={0}", requestTx.bill_phone));
            }

            if (requestTx.bill_street1 != "")
            {
                sb_purchase_data.Append(string.Format("&bill_street1={0}", requestTx.bill_street1));
            }

            if (requestTx.bill_street2 != "")
            {
                sb_purchase_data.Append(string.Format("&bill_street2={0}", requestTx.bill_street2));
            }

            if (requestTx.bill_state != "")
            {
                sb_purchase_data.Append(string.Format("&bill_state={0}", requestTx.bill_state));
            }

            if (requestTx.bill_zip != "")
            {
                sb_purchase_data.Append(string.Format("&bill_zip={0}", requestTx.bill_zip));
            }

            if (requestTx.bill_city != "")
            {
                sb_purchase_data.Append(string.Format("&bill_city={0}", requestTx.bill_city));
            }

            if (requestTx.bill_country != "")
            {
                sb_purchase_data.Append(string.Format("&bill_country={0}", requestTx.bill_country));
            }

            if (requestTx.bill_title != "")
            {
                sb_purchase_data.Append(string.Format("&bill_title={0}", requestTx.bill_title));
            }

            if (requestTx.EmailAddress != "")
            {
                sb_purchase_data.Append(string.Format("&EmailAddress={0}", requestTx.EmailAddress));
            }

            if (requestTx.ip_address != "")
            {
                sb_purchase_data.Append(string.Format("&ip_address={0}", requestTx.ip_address));
            }

            byte[] postData = Encoding.UTF8.GetBytes(sb_purchase_data.ToString());

            // Create a request for the URL. 		
            WebRequest request = WebRequest.Create(paymentGatewayURL);
            //request.Proxy = new WebProxy("127.0.0.1", 8888); // for debugging with fiddler
            // Set the request Method
            request.Method = "POST";
            // If required by the server, set the credentials.
            //request.Credentials = CredentialCache.DefaultCredentials;

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = postData.Length;

            // Get the request stream.            
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.

            dataStream.Write(postData, 0, postData.Length);
            // Close the Stream object.
            dataStream.Close();

            // Get the response.
            WebResponse response = request.GetResponse();
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            string serverResponse = reader.ReadToEnd();
            reader.Close();
            string[] TrimStrings = serverResponse.Split(',');
            string ReturnString = string.Empty;
            for (int i = 0; i < TrimStrings.Length; i++)
            {
                if (i != TrimStrings.Length - 1)
                    ReturnString = ReturnString + TrimStrings[i].Trim() + ",";
                else
                    ReturnString = ReturnString + TrimStrings[i].Trim();
            }

            /*------------Handling purchase response------------*/
            List<string> ReturnedList = new List<string>(ReturnString.ToString().Split(','));

            PurchaseTransactionResponse purchaseTransactionResponse = new PurchaseTransactionResponse() ;
            purchaseTransactionResponse.outcome = ReturnedList[0];
            purchaseTransactionResponse.responseIndicator = ReturnedList[1];
            purchaseTransactionResponse.transactionDate = ReturnedList[2];
            purchaseTransactionResponse.transactionTime = ReturnedList[3];
            purchaseTransactionResponse.transactionOrderID = ReturnedList[4];
            purchaseTransactionResponse.merchantReference = ReturnedList[5];
            purchaseTransactionResponse.transactionAmount = ReturnedList[6];

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
            return purchaseTransactionResponse;
        }
    }
}

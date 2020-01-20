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
    public class PaymentTransactionManager
    {
        public void MockPurchaseRequest()
        {
            PurchaseTransactionRequest purchaseTransactionRequest = new PurchaseTransactionRequest();
            // Action should be derived.
            purchaseTransactionRequest.action = "1";
            // user and pass should be from DB - should also be set in the PerformPaymentTransaction method
            purchaseTransactionRequest.terminalid = "CMVAUL1";
            purchaseTransactionRequest.password = "Password1!";
            
            purchaseTransactionRequest.card = "4444444444444444";
            purchaseTransactionRequest.cvv2 = "115";
            purchaseTransactionRequest.expYear = "2012";
            purchaseTransactionRequest.expMonth = "12";
            purchaseTransactionRequest.member = "Test User";
            purchaseTransactionRequest.currencyCode = "USD";
            purchaseTransactionRequest.address = "Ebene";
            purchaseTransactionRequest.city = "MOKA";
            purchaseTransactionRequest.stateCode = "MOKA";
            purchaseTransactionRequest.zip = "152458";
            purchaseTransactionRequest.countryCode = "Mauritius";
            purchaseTransactionRequest.Email = "test@test.com";
            purchaseTransactionRequest.amount = "0.00";
            purchaseTransactionRequest.trackid = "soft1256";

            PurchaseTransactionResponse ptRes = this.PerformPaymentTransaction(purchaseTransactionRequest);

            Console.WriteLine("authcode [" + ptRes.authcode + "]");
            Console.WriteLine("responsecode [" + ptRes.responsecode + "]");
            Console.WriteLine("result [" + ptRes.result + "]");
            Console.WriteLine("terminalid [" + ptRes.terminalid + "]");
            Console.WriteLine("trackid [" + ptRes.trackid + "]");
            Console.WriteLine("tranid [" + ptRes.tranid + "]");
            Console.WriteLine("udf1 [" + ptRes.udf1 + "]");
            Console.WriteLine("udf2 [" + ptRes.udf2 + "]");
            Console.WriteLine("udf3 [" + ptRes.udf3 + "]");
            Console.WriteLine("udf4 [" + ptRes.udf4 + "]");
            Console.WriteLine("udf5 [" + ptRes.udf5 + "]");
            string myKey = Console.ReadLine();

        }
    

        public PurchaseTransactionResponse PerformPaymentTransaction(PurchaseTransactionRequest requestTx)
        {
            // Get URL from Config
            // Do DB lookup here. Currently hard-coded
            string paymentGatewayURL = "http://test.soft-connect.biz/DirectTransaction.aspx"; //https://secure.soft-connect.biz/DirectTransaction.aspx;

            // Create a request for the URL. 		
            WebRequest request = WebRequest.Create(paymentGatewayURL);

            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;

            // Set the request Method
            request.Method = "POST";

            // Serialize request

            XmlSerializer xmlSerializer = new XmlSerializer(requestTx.GetType());
            StringWriter textWriter = new StringWriter();
            // For testing
            xmlSerializer.Serialize(Console.Out, requestTx);

            xmlSerializer.Serialize(textWriter, requestTx);
            string postData = textWriter.ToString();

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "text/xml";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();

            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            
            PurchaseTransactionResponse purchaseTransactionResponse;
            XmlSerializer deserializer = new XmlSerializer(typeof(PurchaseTransactionResponse));
            // Call the Deserialize method and cast to the object type.
            purchaseTransactionResponse = (PurchaseTransactionResponse)deserializer.Deserialize(reader);

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
            
            return purchaseTransactionResponse;
        
        }

    }
}

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
    public class CashCloudPurchase
    {
        public CashCloudPurchaseTransactionResponse DoPayment(CashCloudPurchaseTransactionRequest requestTx, string paymentGatewayURL)
        {
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
            // xmlSerializer.Serialize(Console.Out, requestTx);

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
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            CashCloudPurchaseTransactionResponse purchaseTransactionResponse;
            XmlSerializer deserializer = new XmlSerializer(typeof(CashCloudPurchaseTransactionResponse));
            // Call the Deserialize method and cast to the object type.
            purchaseTransactionResponse = (CashCloudPurchaseTransactionResponse)deserializer.Deserialize(reader);

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
            return purchaseTransactionResponse;
        }
    }
}
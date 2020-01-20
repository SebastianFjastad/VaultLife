/*using System;
using System.Text;
using System.Configuration;
using System.Net;
using System.IO;
using Vaultlife.Models;
using Vaultlife.Helpers;
namespace Vaultlife.PaymentService
{
    public class CashCloud
    {

        public string SendPaymentThroughGateway(PaymentHelper PaymentDetails)
        {
            string returnString = String.Empty;
            string[] TrimStrings;
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(string.Format("CO_ID={0}", ConfigurationSettings.AppSettings["CO_ID"]));
                builder.Append(string.Format("&Outlet={0}", ConfigurationSettings.AppSettings["Outlet"]));
                builder.Append(string.Format("&Reference={0}", PaymentDetails.Itemtitle));
                builder.Append(string.Format("&CC_Amount={0}", PaymentDetails.ItemPrice));
                builder.Append(string.Format("&CCname={0}", PaymentDetails.NameOnCard));
                builder.Append(string.Format("&CCnumber={0}", PaymentDetails.CreditCardNumber));
                builder.Append(string.Format("&CCCVV={0}", PaymentDetails.CCVNumber));
                //builder.Append(string.Format("&CCtype={0}", CreditCardType(Convert.ToInt32(userProfile.CreditCardType))));
                builder.Append(string.Format("&ExMonth={0}", PaymentDetails.ExpiryMonth));
                builder.Append(string.Format("&ExYear={0}", PaymentDetails.ExpiryYear));
                //builder.Append(string.Format("&PayPeriod={0}", userProfile.PayPeriod));
                builder.Append(string.Format("&EmailAddress={0}", PaymentDetails.BuyerEmailID));

                byte[] postData = Encoding.UTF8.GetBytes(builder.ToString());

                WebRequest request = WebRequest.Create("https://secure.setcom.co.za/server.cfm");
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postData.Length;

                Stream data = request.GetRequestStream();
                data.Write(postData, 0, postData.Length);
                data.Close();

                WebResponse response = request.GetResponse();
                data = response.GetResponseStream();
                StreamReader reader = new StreamReader(data);

                string serverResponse = reader.ReadToEnd();
                reader.Close();

                TrimStrings = serverResponse.Split(',');

                for (int i = 0; i < TrimStrings.Length; i++)
                {
                    if (i != TrimStrings.Length - 1)
                        returnString = returnString + TrimStrings[i].Trim() + ",";
                    else
                        returnString = returnString + TrimStrings[i].Trim();
                }
            }
            catch (Exception x)
            {
                returnString = x.Message;
            }

            return returnString;
        }

        public string GetPaymentResults(string Code)
        {
            string Results="";

            if (Code == "123456") Results = "Success 123456 -  Payment Approved";
            if (Code == "10000") Results = "Error 10000 -  Processing error";
            if (Code == "10101") Results = "Error 10101 -  One or more compulsory field(s) missing";
            if (Code == "10102") Results = "Error 10102 -  The merchant / outlet could not be found on the system";
            if (Code == "10103") Results = "Error 10103 -  Merchant requires consistent checking to be done";
            if (Code == "10104") Results = "Error 10104 -  Security failure occurred while perforing consistent checking";
            if (Code == "10105") Results = "Error 10105 -  Payment method not accepted by outlet";
            if (Code == "10106") Results = "Error 10106 -  Merchant inactive";
            if (Code == "10107") Results = "Error 10107 -  File missing on server";
            if (Code == "10108") Results = "Error 10108 -  Missing variable";
            if (Code == "10109") Results = "Error 10109 -  Unknown error occurred";
            if (Code == "10110") Results = "Error 10110 -  Card blacklisted";
            if (Code == "10201") Results = "Error 10201 -  Transaction amount invalid";
            if (Code == "10202") Results = "Error 10202 -  Expiry month invalid";
            if (Code == "10203") Results = "Error 10203 -  Expiry year invalid";
            if (Code == "10204") Results = "Error 10204 -  PayPeriod invalid";
            if (Code == "10205") Results = "Error 10205 -  Transaction amount too small";
            if (Code == "10206") Results = "Error 10206 -  Email address invalid.";
            if (Code == "10207") Results = "Error 10207 -  Original transaction date of out of range";
            if (Code == "10301") Results = "Error 10301 -  Unable to retrieve order information";
            if (Code == "10302") Results = "Error 10302 -  Base table not found";
            if (Code == "10303") Results = "Error 10303 -  Column not found";
            if (Code == "10304") Results = "Error 10304 -  Syntax error or access violation";
            if (Code == "10305") Results = "Error 10305 -  Error in assignment";
            if (Code == "10306") Results = "Error 10306 -  Serialization error occurred";
            if (Code == "10307") Results = "Error 10307 -  General exception error occurred";
            if (Code == "10308") Results = "Error 10308 -  Communication link failure";
            if (Code == "10309") Results = "Error 10309 -  Datasource not found or no default driver specified";
            if (Code == "10310") Results = "Error 10310 -  Numeric value out of range";
            if (Code == "10311") Results = "Error 10311 -  Authorisation failure";
            if (Code == "10312") Results = "Error 10312 -  Call to the bank failed.";
            if (Code == "10313") Results = "Error 10313 -  Fully-qualified address and the socket has not been marked to allow address reuse.";
            if (Code == "10401") Results = "Error 10401 -  Invalid Gateway call";
            if (Code == "10402") Results = "Error 10402 -  Verification unavailable";
            if (Code == "10403") Results = "Error 10403 -  Error occurred while attempting verification.";
            if (Code == "10404") Results = "Error 10404 -  Signature validation error occurred";
            if (Code == "10405") Results = "Error 10405 -  Transaction not authenticated.";
            if (Code == "10406") Results = "Error 10406 -  The transaction requires the verification data to be included in the message.";
            if (Code == "10407") Results = "Error 10407 -  Unable to verify cardholder.";
            if (Code == "16001") Results = "Error 16001 -  Invalid transaction type";
            if (Code == "16002") Results = "Error 16002 -  Invalid storename";
            if (Code == "16003") Results = "Error 16003 -  Card number or CVV blank/incorrect";
            if (Code == "16004") Results = "Error 16004 -  Transaction amount zero";
            if (Code == "16005") Results = "Error 16005 -  Card number in invalid format";
            if (Code == "16006") Results = "Error 16006 -  Card number in invalid format";
            if (Code == "16008") Results = "Error 16008 -  Invalid or missing config";
            if (Code == "16009") Results = "Error 16009 -  Invalid property assignment";
            if (Code == "16010") Results = "Error 16010 -  Unsupported transaction";
            if (Code == "16011") Results = "Error 16011 -  Terminal incorrectly loaded";
            if (Code == "16012") Results = "Error 16012 -  MerchantID incorrect";
            if (Code == "16013") Results = "Error 16013 -  Mandatory properties have not been set. Certain properties are mandatory for messages sent to the server. This error is raised when mandatory properties have not been set";
            if (Code == "16014") Results = "Error 16014 -  BIN table not found";
            if (Code == "16016") Results = "Error 16016 -  Refer to issuer";
            if (Code == "16017") Results = "Error 16017 -  Host down";
            if (Code == "16018") Results = "Error 16018 -  Invalid account";
            if (Code == "17001") Results = "Error 17001 -  Refer to card issuer";
            if (Code == "17002") Results = "Error 17002 -  Refer to card issuer";
            if (Code == "17003") Results = "Error 17003 -  Invalid merchant";
            if (Code == "17004") Results = "Error 17004 -  Pick-up card";
            if (Code == "17005") Results = "Error 17005 -  Do not honor";
            if (Code == "17006") Results = "Error 17006 -  Error";
            if (Code == "17007") Results = "Error 17007 -  Pick-up card - special condition";
            if (Code == "17008") Results = "Error 17008 -  Honor with identification";
            if (Code == "17009") Results = "Error 17009 -  Request in progress";
            if (Code == "17010") Results = "Error 17010 -  Approved - partial";
            if (Code == "17011") Results = "Error 17011 -  Approved - VIP";
            if (Code == "17012") Results = "Error 17012 -  Invalid transaction";
            if (Code == "17013") Results = "Error 17013 -  Invalid amount";
            if (Code == "17015") Results = "Error 17015 -  No such issuer";
            if (Code == "17016") Results = "Error 17016 -  Approved - update track 3";
            if (Code == "17017") Results = "Error 17017 -  Customer cancellation";
            if (Code == "17018") Results = "Error 17018 -  Customer dispute";
            if (Code == "17019") Results = "Error 17019 -  Re-enter transaction";
            if (Code == "17020") Results = "Error 17020 -  Invalid response";
            if (Code == "17021") Results = "Error 17021 -  No action taken";
            if (Code == "17022") Results = "Error 17022 -  Suspected malfunction";
            if (Code == "17023") Results = "Error 17023 -  Unacceptable transaction fee";
            if (Code == "17024") Results = "Error 17024 -  File update not supported";
            if (Code == "17025") Results = "Error 17025 -  Unable to locate record";
            if (Code == "17026") Results = "Error 17026 -  Duplicate record";
            if (Code == "17027") Results = "Error 17027 -  File update field edit error";
            if (Code == "17028") Results = "Error 17028 -  File update file locked";
            if (Code == "17029") Results = "Error 17029 -  File update failed";
            if (Code == "17030") Results = "Error 17030 -  Format error";
            if (Code == "17031") Results = "Error 17031 -  Bank not supported";
            if (Code == "17032") Results = "Error 17032 -  Completed partially";
            if (Code == "330Q8") Results = "Error 330Q8 -  Admin card not found";
            if (Code == "330Q9") Results = "Error 330Q9 -  Admin card not allowed";
            if (Code == "330R0") Results = "Error 330R0 -  Approved admin request / in window";
            if (Code == "330R1") Results = "Error 330R1 -  Approved admin request / out of window";
            if (Code == "330R2") Results = "Error 330R2 -  Approved admin request /any time";
            if (Code == "330R3") Results = "Error 330R3 -  Chargeback / customer file updated";
            if (Code == "330R4") Results = "Error 330R4 -  Chargeback / customer file updated / acquirer not";
            if (Code == "330R5") Results = "Error 330R5 -  Chargeback / incorrect prefix number";
            if (Code == "330R6") Results = "Error 330R6 -  Chargeback / incorrect response code";
            if (Code == "330R7") Results = "Error 330R7 -  Admin transaction not supported";
            if (Code == "330R8") Results = "Error 330R8 -  Card on national negative file";
            if (Code == "330S4") Results = "Error 330S4 -  Ptlf is full";
            if (Code == "330S7") Results = "Error 330S7 -  Accepted, incorrect destination";
            if (Code == "330S8") Results = "Error 330S8 -  Admin file problem";
            if (Code == "330S9") Results = "Error 330S9 -  Unable to validate PIN, security box is down";
            if (Code == "330T1") Results = "Error 330T1 -  Invalid credit card advance increment";
            if (Code == "330T2") Results = "Error 330T2 -  Invalid transaction date";
            if (Code == "330T3") Results = "Error 330T3 -  Card not supported";
            if (Code == "330T3") Results = "Error 330T3 -  Card not supported.";
            if (Code == "330T4") Results = "Error 330T4 -  Amount over maximum";
            if (Code == "330T5") Results = "Error 330T5 -  CAF status 0 or 9";
            if (Code == "330T6") Results = "Error 330T6 -  Bad UAF (usage accumulation file)";
            if (Code == "330T7") Results = "Error 330T7 -  Cash back > daily limit";
            if (Code == "330T8") Results = "Error 330T8 -  Invalid credit card.";
            if (Code == "330T8") Results = "Error 330T8 -  Invalid account or card number.";
            if (Code == "50841") Results = "Error 50841 -  Insufficient information was provided to approve this payment.";
            if (Code == "70016") Results = "Error 70016 -  DB file not found";
            if (Code == "91") Results = "Error 91 -  Issuer or switch inoperative";
            if (Code == "B001") Results = "Error B001 -  Unable to verify merchant";
            if (Code == "B002") Results = "Error B002 -  Required field not defined";
            if (Code == "B003") Results = "Error B003 -  Merchant not active";
            if (Code == "B004") Results = "Error B004 -  Action not valid";
            if (Code == "B005") Results = "Error B005 -  Duplicate username";
            if (Code == "B006") Results = "Error B006 -  Password and confirm password does not match";
            if (Code == "B007") Results = "Error B007 -  General exception error occurred";
            if (Code == "B008") Results = "Error B008 -  No profile found for buyer username";
            if (Code == "B009") Results = "Error B009 -  Duplicate profiles found for buyer username";
            if (Code == "B010") Results = "Error B010 -  Incorrect fields passed for update";
            if (Code == "B011") Results = "Error B011 -  30001 Timeout checked by Andre & Bronwyn";
            if (Code == "B012") Results = "Error B012 -  32011-05 updated by Bronwyn";
            if (Code == "B013") Results = "Error B013 -  32047-41 updated by Bronwyn";
            if (Code == "B014") Results = "Error B014 -  Dollar 30006 Timeout updated";
            if (Code == "B015") Results = "Error B015 -  Dollar Unknown error updated";
            if (Code == "Issuer") Results = "Error Issuer  -  Declined Your bank has declined your transaction.";
            if (Code == "32057") Results = "Error 32057 -  Invalid credit card.";

            return Results;
        }
    }


}
*/
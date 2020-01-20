using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Vaultlife.Models;
using PaymentService;

namespace Vaultlife.Managers
{
    public class SetcomPaymentTransactionManager
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
        private string MERCHANT_SECRET { get; set; }

        public void MockSetcomPurchaseRequest()
        {
            PurchaseTransactionRequest purchaseTransactionRequest = new PurchaseTransactionRequest();
            // Action based on service calls
            // CO_ID and OUTLET from DB - are set in the PerformPaymentTransaction method, trackID is derived during paymentTransaction process
            // Internal use fields
            purchaseTransactionRequest.MemberInGameID = 1;
            purchaseTransactionRequest.transactionDateTime = DateTime.Now;

            // Mandatory SETCOM fields
            purchaseTransactionRequest.CCNumber = "4444333322221111";
            purchaseTransactionRequest.CCCVV = "111";
            purchaseTransactionRequest.ExYear = "2020";
            purchaseTransactionRequest.ExMonth = "11";
            purchaseTransactionRequest.CCName = "Test Buyer VS1";
            purchaseTransactionRequest.CC_Amount = "270.00";
            purchaseTransactionRequest.CO_ID = ""; // Populated during PerformPaymentTransaction, data from db payment config
            purchaseTransactionRequest.OUTLET = ""; // Populated during PerformPaymentTransaction, data from db payment config
            purchaseTransactionRequest.Reference = ""; // Unique merchant tracking ID, set during PerformPaymentTransaction
            
            // Consistent key field - Optional SETCOM field - hashed transaction key for transactional safety
            purchaseTransactionRequest.Consistent = ""; // Populated during PerformPaymentTransaction, data from db payment config

            // Optional SETCOM fields
            purchaseTransactionRequest.MobileNumber = "0823456789";
            purchaseTransactionRequest.buyer_id = purchaseTransactionRequest.MemberInGameID.ToString();
            purchaseTransactionRequest.bill_first_name = "test";
            purchaseTransactionRequest.bill_last_name = "user";
            purchaseTransactionRequest.bill_phone = "08234567890";
            purchaseTransactionRequest.bill_street1 = "street1";
            purchaseTransactionRequest.bill_street2 = "street2";
            purchaseTransactionRequest.bill_state = "Gauteng";
            purchaseTransactionRequest.bill_zip = "1234";
            purchaseTransactionRequest.bill_city = "Johannesburg";
            purchaseTransactionRequest.bill_country = "South Africa";
            purchaseTransactionRequest.bill_title = "Mr";
            purchaseTransactionRequest.EmailAddress = "test@test.cpm";
            purchaseTransactionRequest.ip_address = "172.152.43.1";

            PurchaseTransactionResponse ptRes = this.PerformPaymentTransaction(purchaseTransactionRequest);

            Console.WriteLine("outcome [" + ptRes.outcome + "]");
            Console.WriteLine("response indicator [" + ptRes.responseIndicator + "]");
            Console.WriteLine("tx date [" + ptRes.transactionDate + "]");
            Console.WriteLine("tx time [" + ptRes.transactionTime + "]");
            Console.WriteLine("tx order ID [" + ptRes.transactionOrderID + "]");
            Console.WriteLine("merchant ref [" + ptRes.merchantReference + "]");
            Console.WriteLine("tx amount [" + ptRes.transactionAmount + "]");

        }
     
        private PaymentConfiguration GetPaymentConfiguration()
        {
            // Get Config
            PaymentConfiguration paymentConfiguration = new PaymentConfiguration();
            var paymentConfigs = from pc in db.PaymentConfigurations where pc.Status == "A" orderby pc.PaymentConfigurationID descending select pc;
            foreach (var paymentConfig in paymentConfigs)
            {
                // get the most recent active config.
                paymentConfiguration = (PaymentConfiguration)paymentConfig;
                break;
            }
            return paymentConfiguration;
        }

        private string BuildMerchantReference(PurchaseTransactionRequest purchaseTransactionRequest)
        {
            string merchantReference = purchaseTransactionRequest.bill_last_name.Trim() + ", " + purchaseTransactionRequest.bill_first_name.Trim() + purchaseTransactionRequest.CC_Amount + purchaseTransactionRequest.transactionDateTime;

            string hashedMerchantReference = Hashing.GetMd5Hash(merchantReference);
            if (hashedMerchantReference.Length > 250)
            {
                hashedMerchantReference = hashedMerchantReference.Substring(0, 249);
            }

            Console.WriteLine("The MD5 hash of " + merchantReference + " is: " + hashedMerchantReference + ".");

            return hashedMerchantReference ;
        }

        private string BuildConsistentKey(PurchaseTransactionRequest purchaseTransactionRequest)
        {
            // Consistent Key - NB!! NEVER store/persist this field !!!
            /*  How to generate the Consistent Field:
                The consistent field to be sent in the Request is generated by hashing (using the SHA-512 algorithm using UTF-8 encoding) the concatenated fields in the order specified below:
                1. CO_ID
                2. OUTLET
                3. Reference
                4. CC_Amount
                5. CCName
                6. CCNumber
                7. ExYear
                8. ExMonth
                9. CCCVV
                10. Merchant Secret Key
                Process to follow to generate the Consistent field:
                1. Concatenate the above fields in the order specified.
                2. Apply a SHA512 hashing algorithm to the newly generated string. Remember to use UTF-8 encoding.
                3. Please ensure that the generated hash is always in uppercase. */
            StringBuilder consistentKey = new StringBuilder();
            consistentKey.Append(purchaseTransactionRequest.CO_ID);
            consistentKey.Append(purchaseTransactionRequest.OUTLET);
            consistentKey.Append(purchaseTransactionRequest.Reference);
            consistentKey.Append(purchaseTransactionRequest.CC_Amount);
            consistentKey.Append(purchaseTransactionRequest.CCName);
            consistentKey.Append(purchaseTransactionRequest.CCNumber);
            consistentKey.Append(purchaseTransactionRequest.ExYear);
            consistentKey.Append(purchaseTransactionRequest.ExMonth);
            consistentKey.Append(purchaseTransactionRequest.CCCVV);
            consistentKey.Append(MERCHANT_SECRET);

            string sha512HashedConsistentKey = Hashing.GetSHA512Hashed(consistentKey.ToString()).ToUpper();

            Console.WriteLine("The MD5 hash of " + consistentKey + " is: " + sha512HashedConsistentKey + ".");

            return sha512HashedConsistentKey;
        }

        // Returns PaymentTransactionID for this transaction. 
        private PurchaseTransactionRequest PersistPaymentRequest(PurchaseTransactionRequest purchaseTransactionRequest)
        {
            PaymentTransaction paymentTransaction = new PaymentTransaction();
            paymentTransaction.Action = "1";
            paymentTransaction.Address = "";
            paymentTransaction.Amount = Convert.ToDecimal(purchaseTransactionRequest.CC_Amount);
            paymentTransaction.City = purchaseTransactionRequest.bill_city;
            paymentTransaction.ClientIP = purchaseTransactionRequest.ip_address;
            paymentTransaction.CountryCode = purchaseTransactionRequest.bill_country;
            paymentTransaction.CurrencyCode = "ZAR";
            paymentTransaction.email = purchaseTransactionRequest.EmailAddress;
            paymentTransaction.Member = purchaseTransactionRequest.bill_last_name + ", " + purchaseTransactionRequest.bill_first_name ;
            //paymentTransaction.MemberInGameID = purchaseTransactionRequest.MemberInGameID;
            paymentTransaction.MerchantIP = "";
            paymentTransaction.StateCode = purchaseTransactionRequest.bill_state;
            paymentTransaction.TrackID = purchaseTransactionRequest.Reference;
            paymentTransaction.TransactionRequestDateTime = purchaseTransactionRequest.transactionDateTime;
            paymentTransaction.Zip = purchaseTransactionRequest.bill_zip;
            paymentTransaction.PaymentStatus = "UNPAID";

            db.PaymentTransactions.Add(paymentTransaction);
            
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            purchaseTransactionRequest.paymentTransactionID = paymentTransaction.PaymentTransactionID;
            return purchaseTransactionRequest;
        }

        private bool VerifyAndPersistPaymentResponse(PurchaseTransactionRequest purchaseTransactionRequest, PurchaseTransactionResponse purchaseTransactionResponse)
        {
            PaymentTransaction paymentTransaction = db.PaymentTransactions.Where(x => x.TrackID == purchaseTransactionResponse.merchantReference ).First();
            // Do validation on merchant reference.
            if (purchaseTransactionResponse.merchantReference == purchaseTransactionRequest.Reference)
            {
                paymentTransaction.Result = purchaseTransactionResponse.outcome;
                paymentTransaction.ResponseCode = purchaseTransactionResponse.responseIndicator;
                paymentTransaction.AuthCode = (purchaseTransactionResponse.outcome.ToUpper() == "APPROVED") ? purchaseTransactionResponse.responseIndicator : "";
                paymentTransaction.TranID = purchaseTransactionResponse.transactionOrderID;
                paymentTransaction.TransactionResponseDateTime = DateTime.Now;
                if (purchaseTransactionResponse.outcome.ToUpper() == "APPROVED") 
                {
                    paymentTransaction.PaymentStatus = "PAID";
                }
            }

            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
            return true;
        }

        public PurchaseTransactionResponse PerformPaymentTransaction(PurchaseTransactionRequest requestTx)
        {
            // Get URL from Config
            PaymentConfiguration paymentConfiguration = this.GetPaymentConfiguration();

            requestTx.CO_ID = paymentConfiguration.CO_ID;
            requestTx.OUTLET = paymentConfiguration.OUTLET;
            MERCHANT_SECRET = paymentConfiguration.MERCHANT_SECRET_KEY;
            requestTx.Reference = BuildMerchantReference(requestTx);
            requestTx.Consistent = this.BuildConsistentKey(requestTx);

            // Persist request transaction
            requestTx = this.PersistPaymentRequest(requestTx);

            // Do Payment
            SetcomPurchase transactionProcessor = new SetcomPurchase();
            PurchaseTransactionResponse purchaseTransactionResponse;
            purchaseTransactionResponse = transactionProcessor.DoPayment(requestTx, paymentConfiguration.PaymentGatewayURL);

            // persist response
            bool updateSuccessful = this.VerifyAndPersistPaymentResponse(requestTx, purchaseTransactionResponse);

            return purchaseTransactionResponse;
        
        }

    }
}

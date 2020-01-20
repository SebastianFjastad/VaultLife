using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PaymentService
{
    [XmlRoot("response")]
    public class PurchaseTransactionResponse
    {
        public string outcome { get; set; }
        public string responseIndicator { get; set; }
        public string transactionDate { get; set; }
        public string transactionTime { get; set; }
        public string transactionOrderID { get; set; }
        public string merchantReference { get; set; }
        public string transactionAmount { get; set; }
        public string consistent { get; set; }
        public string timeRemaining { get; set; }
    }
}
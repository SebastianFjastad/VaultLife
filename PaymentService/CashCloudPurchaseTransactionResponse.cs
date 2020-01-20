using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PaymentService
{
    [XmlRoot("response")]
    public class CashCloudPurchaseTransactionResponse
    {
        public string result { get; set; }
        public string responsecode { get; set; }
        public string authcode { get; set; }
        public string tranid { get; set; }
        public string trackid { get; set; }
        public string terminalid { get; set; }
        public string udf1 { get; set; }
        public string udf2 { get; set; }
        public string udf3 { get; set; }
        public string udf4 { get; set; }
        public string udf5 { get; set; }
        public string timeRemaining { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService
{
    public class RequestMoneyRequest
    {
        public string email { get; set; } //mandatory
        public int amount { get; set; } //mandatory

        public int currency_id { get; set; } //mandatory

        public int reason_id { get; set; } //mandatory

        public string remark { get; set; } //optional

        //External Merchants Transaction References 
        public string externId { get; set; } //optional

        public string externReference { get; set; } //optional

        public string externDescription { get; set; } //optional

    }

    public class RequestMoneyResponse
    {
        public bool error { get; set; }
        public string hash { get; set; }
        public string amount { get; set; }
        public string fee { get; set; }
        public string validationFailed { get; set; }

        //See samples for validationFailed and test in sandbox to see how to parse
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService
{
    public class RefundRequest
    {
        public string hash { get; set; } //mandatory
        public string amount { get; set; }//mandatory
        public string remark { get; set; }//mandatory

        //External Merchants Transaction References
        public string externId { get; set; } //optional
        public string externReference { get; set; } //optional
        public string externDescription { get; set; } //optional
    }
    
    public class RefundResponse
    {
        public bool error { get; set; }
        public string hash { get; set; }

        public string amount { get; set; }

        public string fee { get; set; }

        public string fee_refund { get; set; }

        public string validationFailed { get; set; }

        //See samples for validationFailed and test in sandbox to see how to parse
    }
}

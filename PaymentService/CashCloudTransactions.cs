using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService
{
    public class TransactionsRequest
    {

        public string hash { get; set; }//mandatory Transaction Unique Hash
    }

    public class TransactionsResponse
    {
        public string error { get; set; }
        public List<Transaction> Transactions { get; set; }
    }

    public class Transaction
    {
        public string hash { get; set; }
        public string transaction_type_id { get; set; }
        public string transaction_state_id { get; set; }
        public string reason_id { get; set; }
        public string remark { get; set; }
        public string currency_id { get; set; }
        public string amount { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public Partner partner { get; set; }

        
    
    }

    public class Partner 
    {
        public string email { get; set; }
        public string eWalletID { get; set; }
    }

}

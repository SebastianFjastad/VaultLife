
namespace Vaultlife.Helpers
{
    public class PaymentHelper
    {

        #region Private Variables

        private string _creditCardNumber;
        private string _nameOnCard = string.Empty;
        private string _ccvNumber;
        private string _expiryMonth;
        private string _expiryYear;
        private string _transactionID = string.Empty;

        private string _itemtitle = string.Empty;
        private double _itemPrice;
        private string _buyerEmailID = string.Empty;


        #endregion


        #region Public Properties

        public string CreditCardNumber
        {
            get { return _creditCardNumber; }
            set { _creditCardNumber = value; }
        }

        public string NameOnCard
        {
            get { return _nameOnCard; }
            set { _nameOnCard = value; }
        }

        public string CCVNumber
        {
            get { return _ccvNumber; }
            set { _ccvNumber = value; }
        }

        public string ExpiryMonth
        {
            get { return _expiryMonth; }
            set { _expiryMonth = value; }
        }

        public string ExpiryYear
        {
            get { return _expiryYear; }
            set { _expiryYear = value; }
        }

        public string TransactionID
        {
            get { return _transactionID; }
            set { _transactionID = value; }
        }


        public string Itemtitle
        {
            get { return _itemtitle; }
            set { _itemtitle = value; }
        }

        public double ItemPrice
        {
            get { return _itemPrice; }
            set { _itemPrice = value; }
        }

        public string BuyerEmailID
        {
            get { return _buyerEmailID; }
            set { _buyerEmailID = value; }
        }

        #endregion      


     

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Vaultlife.Models
{
    public class ProductImageURL
    {
        public string ProductImageDisplayURL;
    }

	[DataContract]
	public class MemberInGameItem
	{
		private int _productID; // product
		private int _gameID;  // Game
		private int _ownerID;  //product
		private string _ownerImage; // unknown
		private string _productName; //product
		private string _productCategory;  //product->productInCategory ** manytoOneRelationship - one product - many categories
		private string _productDescription; //product
        private String  _productImage;
        //private IEnumerable<ProductImageURL> _productImageURLs;
        private decimal _productPrice; //product
		private string _productTerms; //N/A????
		private string _gameCode;  //Game
		private string _gameStatus;  // ?
		private string _gameStatusColor; // ?
		private string _gameTypeName; //GameType
		private string _gameName; // Game
		private string _gameDescription; // Game
		private decimal _gamePrice; // ?
		private DateTime _gameDateTime; //GameStart? GameRule table
		private string _territoryName; // territory - via long convoluted query path...
		private DateTime _dateTimeInserted; // for what?
		private DateTime _dateTimeUpdated; // for what? game/product/member?
        // query is from f in db.MemberInGames.include(Game).include(ProductInGame).include(GameRule).where(f.MemberID = {MemberID} && f.GameID = {derived_GameID})
        
		[DataMember]
		public int ProductID
		{
			get { return _productID; }
			set { _productID = value; }
		}

		[DataMember]
		public int GameID
		{
			get { return _gameID; }
			set { _gameID = value; }
		}

		[DataMember]
		public  int OwnerID
		{
			get { return _ownerID; }
			set { _ownerID = value; }
		}

		[DataMember]
		public string OwnerImage
		{
			get { return _ownerImage; }
			set { _ownerImage = value; }
		}

		[DataMember]
		public string ProductName
		{
			get { return _productName; }
			set { _productName = value; }
		}

		[DataMember]
		public string ProductCategory
		{
			get { return _productCategory; }
			set { _productCategory = value; }
		}

		[DataMember]
		public string ProductDescription
		{
			get { return _productDescription; }
			set { _productDescription = value; }
		}

		[DataMember]
		public decimal ProductPrice
		{
			get { return _productPrice; }
			set { _productPrice = value; }
		}

		[DataMember]
		public string ProductTerms
		{
			get { return _productTerms; }
			set { _productTerms = value; }
		}
        
        [DataMember]
        public string ProductImage
        {
            get { return _productImage; }
            set { _productImage = value; }
        }
		[DataMember]
		public string GameCode
		{
			get { return _gameCode; }
			set { _gameCode = value; }
		}

		[DataMember]
		public string GameStatus
		{
			get { return _gameStatus; }
			set { _gameStatus = value; }
		}

		[DataMember]
		public string GameStatusColor
		{
			get { return _gameStatusColor; }
			set { _gameStatusColor = value; }
		}

		[DataMember]
		public string GameTypeName
		{
			get { return _gameTypeName; }
			set { _gameTypeName = value; }
		}

		[DataMember]
		public string GameName
		{
			get { return _gameName; }
			set { _gameName = value; }
		}

		[DataMember]
		public string GameDescription
		{
			get { return _gameDescription; }
			set { _gameDescription = value; }
		}

		[DataMember]
		public decimal GamePrice
		{
			get { return _gamePrice; }
			set { _gamePrice = value; }
		}

		//[DataMember]
        public DateTime GameDateTime
		{
			get { return _gameDateTime; }
			set { _gameDateTime = value; }
		}

		[DataMember]
		public string TerritoryName
		{
			get { return _territoryName; }
			set { _territoryName = value; }
		}

		//[DataMember]
        public DateTime DateTimeInserted
		{
			get { return _dateTimeInserted; }
			set { _dateTimeInserted = value; }
		}

		//[DataMember]
		public DateTime DateTimeUpdated
		{
			get { return _dateTimeUpdated; }
		    set { _dateTimeUpdated = value; }
		}
    }
}
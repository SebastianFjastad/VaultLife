using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vaultlife.Models.Mobile
{
    public class PaginatedItems
    {
        public List<BannerAdvert> BannerAdverts { get; set; }
        public List<Item> Items { get; set; }

    }

    public class BannerAdvert
    {
        public List<string> BannerAdvertImageID { get; set; }
    }

    public class Item
    {
        public string ProductID { get; set; }
        public string GameID { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string ProductDescription { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public string ProductPrice { get; set; }
        public string ProductTerms { get; set; }
        public string ProductWebsite { get; set; }
        public string GameCode { get; set; }
        public string GameStatus { get; set; }
        public string GameStatusColor { get; set; }
        public string GameType { get; set; }
        public string GameName { get; set; }
        public string GameDescription { get; set; }
        public string GamePrice { get; set; }
        public string GameCountry { get; set; }
        public string GameCurrency { get; set; }
        public string GameUnlockDateTime { get; set; }
        public string GameExpiryDateTime { get; set; }
        public string GamePrepareDateTime { get; set; }
        public string GameResolveWinnersDateTime { get; set; }
        public string TerritoryName { get; set; }
        public string TerritoryFlag { get; set; }
        public string LastUpdated { get; set; }
        public string FiveMinDeal { get; set; }
        public string NextGameID { get; set; }
        public string NextGameType { get; set; }
        public string NextGameTypeID { get; set; }
    }

    public class ProductImage
    {
        public string ImageID { get; set; }
    }
}
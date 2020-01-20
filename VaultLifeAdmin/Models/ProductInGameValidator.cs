using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using FluentValidation;
using FluentValidation.Attributes;
using VaultLifeAdmin.Models;

namespace VaultLifeAdmin.Models
{
    public class ProductInGameValidator : AbstractValidator<ProductInGame>
    {

        public ProductInGameValidator()
        {
            //RuleFor(pg => pg.Quantity).Must(AvailableSOHCheck).WithMessage("Insufficent Stock Available for this Product");
            //RuleFor(product => product.Quantity).Must(NotBeNegative).WithMessage("Quantity cannot be less than 0");
            RuleFor(x => x.ProductInGameID).Must(HaveOneLocation).WithMessage("Must have at least one product location");
        }

        private bool AvailableSOHCheck(ProductInGame productInGame, int newQuantity)
        {


            VaultLifeApplicationEntities _db = new VaultLifeApplicationEntities();
            var dbProduct = _db.Products
                                .Where(x => x.ProductID == productInGame.ProductID)
                                .SingleOrDefault();

            if (dbProduct == null)
                return false;

            return ((dbProduct.AvailableSOH - newQuantity) >= 0);
        }
        private bool NotBeNegative(ProductInGame product, int Quantity)
        {


            return Quantity >= 0;
        }
        private bool HaveOneLocation(ProductInGame product, int ProductInGameID)
        {
            VaultLifeApplicationEntities _db = new VaultLifeApplicationEntities();
            var dbProductInGame = _db.ProductInGames.Include(a => a.ProductLocations).Where(x => x.ProductInGameID == ProductInGameID).SingleOrDefault();

            if (dbProductInGame == null)
                return false;

            if (dbProductInGame.ProductLocations == null)
                return false;

            if (dbProductInGame.ProductLocations.Count <= 0)
                return false;

            return true;
        }


    }


    [Validator(typeof(ProductInGameValidator))]
    public partial class ProductInGame
    {

    }
}
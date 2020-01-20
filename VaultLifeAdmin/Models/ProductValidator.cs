using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaultLifeAdmin.Models;
using FluentValidation;
using FluentValidation.Attributes;

namespace VaultLifeAdmin.Models
{

    [Validator(typeof(ProductValidator))]
    public partial class Product
    {

    }

    public class ProductValidator : AbstractValidator<Product>
    {


        public ProductValidator()
        {

            //RuleFor(product => product.ProductSKUCode).NotEmpty().WithMessage("SKU must not be empty.");
            //RuleFor(product => product.ProductSKUCode).Length(0, 100).WithMessage("SKU must not exceed 100 characters.");
            RuleFor(product => product.ProductSKUCode).Must(UniqueName).WithMessage("SKU must be unique");
            RuleFor(product => product.AvailableSOH).Must(NotExceedSOH).WithMessage("Available SOH must not exceed SOH");
            RuleFor(product => product.AvailableSOH).Must(NotBeNegative).WithMessage("Available SOH cannot be less than 0");
        }

        private bool UniqueName(Product product, string name)
        {
            VaultLifeApplicationEntities _db = new VaultLifeApplicationEntities();
            var dbProduct = _db.Products
                                .Where(x => x.ProductSKUCode.ToLower() == name.ToLower())
                                .FirstOrDefault();

            if (dbProduct == null)
                return true;

            return dbProduct.ProductID == product.ProductID;
        }

        private bool NotExceedSOH(Product product, int? AvailableSOH)
        {


            return product.SOH >= AvailableSOH;
        }

        private bool NotBeNegative(Product product, int? AvailableSOH)
        {


            return AvailableSOH >= 0;
        }
    }

}
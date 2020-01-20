using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Vaultlife.Models
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


            return AvailableSOH >=0;
        }
    }


    [Validator(typeof(MemberValidator))]
    public partial class Member
    {

    }


    public class MemberValidator : AbstractValidator<Member>
    {

        public MemberValidator()
        {

            //RuleFor(product => product.ProductSKUCode).NotEmpty().WithMessage("SKU must not be empty.");
            //RuleFor(product => product.ProductSKUCode).Length(0, 100).WithMessage("SKU must not exceed 100 characters.");
            RuleFor(product => product.EmailAddress).Must(UniqueName).WithMessage("Email must be unique");
       //     RuleFor(product => product.DateOfBirth).Must(AgeCheck).WithMessage("Must be over 18 years old");

        }

        private bool UniqueName(Member member, string name)
        {
            VaultLifeApplicationEntities _db = new VaultLifeApplicationEntities();
            var dbMember = _db.Members
                                .Where(x => x.EmailAddress.ToLower() == name.ToLower())
                                .SingleOrDefault();

            if (dbMember == null)
                return true;

            return dbMember.MemberID == member.MemberID;
        }

        private bool AgeCheck(Member member, DateTime name)
        {
            TimeSpan ts = DateTime.Now - name;

            //VaultLifeApplicationEntities _db = new VaultLifeApplicationEntities();
            //var dbMember = _db.Members
            //                    .Where(x => x.DateOfBirth == name)
            //                    .SingleOrDefault();

            //if (dbMember == null)
            //    return true;

            return ts.TotalDays > (365 * 18);
        }



    }



    [Validator(typeof(ProductInGameValidator))]
    public partial class ProductInGame
    {

    }
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
            var dbProductInGame = _db.ProductInGames.Include(a =>a.ProductLocations).Where(x => x.ProductInGameID == ProductInGameID).SingleOrDefault();

            if (dbProductInGame == null)
                return false;
            
            if (dbProductInGame.ProductLocations == null)
                return false;

            if (dbProductInGame.ProductLocations.Count  <= 0)
                return false;

            return true;
        }


    }

    [Validator(typeof(SerialNumberValidator))]
    public partial class SerialNumber
    { }


    public class SerialNumberValidator : AbstractValidator<SerialNumber>
    {
        public SerialNumberValidator()
        {

            RuleFor(s => s.Serial).Must(Unique).WithMessage("Serial number already used");
          
        }

        private bool Unique(SerialNumber sn, string snum)
        {
            VaultLifeApplicationEntities _db = new VaultLifeApplicationEntities();
            var dbSerial = _db.SerialNumbers
                                .Where(x => x.Serial.ToLower() == snum.ToLower())
                                .SingleOrDefault();

            if (dbSerial == null)
                return true;

            return dbSerial.SerialNumberID == sn.SerialNumberID;
        }
    }

    [Validator(typeof(VoucherValidator))]
    public partial class Voucher
    { }
    public class VoucherValidator : AbstractValidator<Voucher>
    {
        public VoucherValidator()
        {

            RuleFor(s => s.VoucherNumber).Must(Unique).WithMessage("Voucher number already used");
          
        }

        private bool Unique(Voucher v, string vnum)
        {
            VaultLifeApplicationEntities _db = new VaultLifeApplicationEntities();
            var dbVoucher = _db.Vouchers
                                .Where(x => x.VoucherNumber.ToLower() == vnum.ToLower())
                                .SingleOrDefault();

            if (dbVoucher == null)
                return true;

            return dbVoucher.VoucherID == v.VoucherID;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class MustBeTrueAttribute : ValidationAttribute, IClientValidatable
    {
        protected override ValidationResult IsValid(object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if ((bool)value)
                return ValidationResult.Success;
            return new ValidationResult(String.Format(ErrorMessageString, validationContext.DisplayName));
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "checkrequired"
            };

            yield return rule;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class AgeGreaterThan : ValidationAttribute, IClientValidatable
    {
        protected override ValidationResult IsValid(object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {

            TimeSpan ts = DateTime.Now - Convert.ToDateTime(value);

            if (ts.TotalDays > (365 * 18))
                return ValidationResult.Success;
            return new ValidationResult(String.Format(ErrorMessageString, validationContext.DisplayName));
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "checkage"
            };

            yield return rule;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ValidateAgeAttribute : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "Minimum age is 18, your {0} should fall before {1}";

      public DateTime MaximumDateProperty { get; private set; }

        public ValidateAgeAttribute(
            int minimumAgeProperty)
            : base(DefaultErrorMessage)
        {
            MaximumDateProperty = DateTime.Now.AddYears(minimumAgeProperty * -1);
            }

        protected override ValidationResult IsValid(object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime parsedValue = (DateTime)value;

                if ( parsedValue >= MaximumDateProperty)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return ValidationResult.Success;

        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ValidationType = "validateage",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
            };

           rule.ValidationParameters.Add("maximumdate", MaximumDateProperty.ToShortDateString());

            yield return rule;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, MaximumDateProperty.ToShortDateString());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaultLifeAdmin.Models;
using FluentValidation;
using FluentValidation.Attributes;

namespace VaultLifeAdmin.Models
{
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

}
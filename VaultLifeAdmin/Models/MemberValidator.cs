using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaultLifeAdmin.Models;
using FluentValidation;
using FluentValidation.Attributes;

namespace VaultLifeAdmin.Models
{
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

    [Validator(typeof(MemberValidator))]
    public partial class Member
    {

    }

}
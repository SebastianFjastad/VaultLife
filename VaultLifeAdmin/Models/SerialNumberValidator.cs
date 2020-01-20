using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaultLifeAdmin.Models;
using FluentValidation;
using FluentValidation.Attributes;

namespace VaultLifeAdmin.Models
{
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

}
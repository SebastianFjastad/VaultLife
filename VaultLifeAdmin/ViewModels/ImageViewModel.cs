using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaultLifeAdmin.Models;

namespace VaultLifeAdmin.ViewModels
{
    public class ImageViewModel
    {
        public Imagedetail Image {get; set;}
        public bool DeleteOnSave { get; set; }
    }
}
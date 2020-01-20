using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vaultlife.Models
{
    public class FileUploadModel
    {
        public FileUploadModel()
        {
            Files = new List<HttpPostedFileBase>();
        }

        public List<HttpPostedFileBase> Files { get; set; }
    }
}
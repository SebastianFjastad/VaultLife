using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vaultlife.Models
{
    public class CustomModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<System.Attribute> attributes, System.Type containerType, System.Func<object> modelAccessor, System.Type modelType, string propertyName)
        {
            var modelMetadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            if (string.IsNullOrEmpty(propertyName)) return modelMetadata;

            if (modelType == typeof(String))
                modelMetadata.ConvertEmptyStringToNull = false;
            
            return modelMetadata;
        }
    }
}
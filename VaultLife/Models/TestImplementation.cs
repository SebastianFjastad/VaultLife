using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vaultlife.Models
{
    public class TestImplementation : ITest
    {
        public string TestString
        {
            get { return "Dependency resolver is working"; }
        }
    }
}
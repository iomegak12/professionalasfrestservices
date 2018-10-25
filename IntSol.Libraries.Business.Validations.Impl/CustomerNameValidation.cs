using IntSol.Libraries.Business.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntSol.Libraries.Business.Validations.Impl
{
    public class CustomerNameValidation : ICustomerNameValidation
    {
        public bool Validate(string tObject)
        {
            var badKeywords = new string[] { "bad", "worse", "worst", "not good" };
            var isSearchStringBadWord = badKeywords.Contains(tObject);

            return isSearchStringBadWord;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace IntSol.Libraries.Business.Validations.Interfaces
{
    public interface IBusinessValidation<T>
    {
        bool Validate(T tObject);
    }
}

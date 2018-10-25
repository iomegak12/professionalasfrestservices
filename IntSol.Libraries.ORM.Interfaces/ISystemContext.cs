using System;
using System.Collections.Generic;

namespace IntSol.Libraries.ORM.Interfaces
{
    public interface ISystemContext : IDisposable
    {
        IEnumerable<T> ExecuteRoutine<T>(
            string routineName, IDictionary<string, object> routineParameters = default(IDictionary<string, object>));

        bool CommitChanges();
    }
}

using IntSol.Libraries.ORM.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace IntSol.Libraries.ORM.Impl
{
    public abstract class BaseSystemContext : DbContext, ISystemContext
    {
        private const int MIN_ROWS = 1;

        public BaseSystemContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public bool CommitChanges()
        {
            var rowsAffected = this.SaveChanges();

            return rowsAffected >= MIN_ROWS;
        }

        public IEnumerable<T> ExecuteRoutine<T>(string routineName, IDictionary<string, object> routineParameters = null)
        {
            var tList = default(IEnumerable<T>);
            var validation = !string.IsNullOrEmpty(routineName);

            if (validation)
            {
            }

            return tList;
        }
    }
}

using System.Collections.Generic;
using System.Data.SQLite;
using Illallangi.LiteOrm;

namespace Illallangi.ETagCache.Context
{
    using Common.Logging;

    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region Fields

        /// <summary>
        /// Holds the current value of the Log property.
        /// </summary>
        private readonly ILog currentLog;

        /// <summary>
        /// Holds the current value of the ConnectionSource property.
        /// </summary>
        private readonly IConnectionSource currentConnectionSource;

        #endregion

        #region Constructors

        protected RepositoryBase(ILog log, IConnectionSource connectionSource)
        {
            this.currentLog = log;
            this.currentConnectionSource = connectionSource;
        }

        #endregion

        #region Properties

        #region Protected Properties

        protected ILog Log
        {
            get { return this.currentLog; }
        }

        #endregion

        #region Private Properties

        private IConnectionSource ConnectionSource
        {
            get { return this.currentConnectionSource; }
        }

        #endregion

        #endregion

        #region Methods

        public abstract T Create(T obj);

        public abstract IEnumerable<T> Retrieve(T obj = null);

        public abstract T Update(T obj);

        public abstract void Delete(T obj);

        protected SQLiteConnection GetConnection()
        {
            return this.ConnectionSource.GetConnection();
        }

        #endregion
    }
}
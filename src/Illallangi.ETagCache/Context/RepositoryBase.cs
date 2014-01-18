using System.Collections.Generic;
using System.Data.SQLite;
using Ninject.Extensions.Logging;

namespace Illallangi.FlightLog.Context
{
    using Illallangi.ETagCache.Context;
    using Illallangi.LiteOrm;

    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region Fields

        /// <summary>
        /// Holds the current value of the Logger property.
        /// </summary>
        private readonly ILogger currentLogger;

        /// <summary>
        /// Holds the current value of the ConnectionSource property.
        /// </summary>
        private readonly IConnectionSource currentConnectionSource;

        #endregion

        #region Constructors

        protected RepositoryBase(ILogger logger, IConnectionSource connectionSource)
        {
            this.currentLogger = logger;
            this.currentConnectionSource = connectionSource;
        }

        #endregion

        #region Properties

        #region Protected Properties

        protected ILogger Logger
        {
            get { return this.currentLogger; }
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
using System.Collections.Generic;
using Illallangi.ETagCache.Model;
using Illallangi.FlightLog.Context;
using Illallangi.LiteOrm;
using Ninject.Extensions.Logging;

namespace Illallangi.ETagCache.Context
{
    using System.Linq;

    public sealed class CacheEntryRepository : RepositoryBase<CacheEntry>
    {
        public CacheEntryRepository(ILogger logger, IConnectionSource connectionSource)
            : base(logger, connectionSource)
        {
        }

        public override CacheEntry Create(CacheEntry obj)
        {
            this.Logger.Debug(@"CacheEntryRepository.Create(""{0}"")", obj);

            var id = this.GetConnection()
                .InsertInto("CacheEntries")
                .Values("ETag", obj.ETag)
                .Values("Resource", obj.Resource)
                .Values("Value", obj.Value)
                .Go();

            return this.Retrieve(new CacheEntry { Id = id }).Single();
        }

        public override IEnumerable<CacheEntry> Retrieve(CacheEntry obj = null)
        {
            this.Logger.Debug(@"CacheEntryRepository.Retrieve(""{0}"")", obj);

            return this.GetConnection()
                .Select<CacheEntry>("CacheEntries")
                .Column("CacheEntryId", (cacheEntry, value) => cacheEntry.Id = value, null == obj ? null : obj.Id)
                .Column("ETag", (cacheEntry, value) => cacheEntry.ETag = value)
                .Column("Resource", (cacheEntry, value) => cacheEntry.Resource = value, null == obj ? null : obj.Resource)
                .Column("Value", (cacheEntry, value) => cacheEntry.Value = value)
                .Go();
        }

        public override CacheEntry Update(CacheEntry obj)
        {
            throw new System.NotImplementedException();
        }

        public override void Delete(CacheEntry obj)
        {
            throw new System.NotImplementedException();
        }
    }
}

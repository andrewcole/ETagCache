using System.Linq;
using Common.Logging;
using Illallangi.ETagCache.Model;
using Illallangi.LiteOrm;

namespace Illallangi.ETagCache.Context
{
    public sealed class CacheEntryRepository : SQLiteContextBase, IRestCache
    {
        private const string DatabasePath = @"%localappdata%\Illallangi Enterprises\ETagCache.dat";

        private const string SqlSchema =
            @"CREATE TABLE CacheEntries (CacheEntryId INTEGER PRIMARY KEY AUTOINCREMENT, BaseUrl TEXT NOT NULL, Resource TEXT NOT NULL, ETag TEXT NOT NULL, Content TEXT NOT NULL, Unique (BaseUrl, Resource));";

        public CacheEntryRepository(ILog log = null)
            : base(CacheEntryRepository.DatabasePath, sqlSchemaLines: new[] { CacheEntryRepository.SqlSchema }, log: log)
        {
        }

        public void Create(string baseUrl, string resource, string eTag, string content)
        {
            this.Log.DebugFormat(
                @"CacheEntryRepository.Create(baseUrl=""{0}"", resource=""{1}"", eTag=""{2}"", content=""{3}"")",
                baseUrl,
                resource,
                eTag,
                content);

            this.GetConnection()
                .InsertInto("CacheEntries")
                .Values("BaseUrl", baseUrl)
                .Values("Resource", resource)
                .Values("ETag", eTag)
                .Values("Content", content)
                .Go();
        }

        public IRestCacheEntry Retrieve(string baseUrl, string resource)
        {
            this.Log.DebugFormat(
                @"CacheEntryRepository.Retrieve(baseUrl=""{0}"", resource=""{1}"")",
                baseUrl,
                resource);

            return this.GetConnection()
                .Select<CacheEntry>("CacheEntries")
                .Column("CacheEntryId", (cacheEntry, value) => cacheEntry.Id = value)
                .Column("BaseUrl", (cacheEntry, value) => cacheEntry.BaseUrl = value, baseUrl)
                .Column("Resource", (cacheEntry, value) => cacheEntry.Resource = value, resource)
                .Column("ETag", (cacheEntry, value) => cacheEntry.ETag = value)
                .Column("Content", (cacheEntry, value) => cacheEntry.Content = value)
                .Go()
                .SingleOrDefault();
        }

        public void Delete(IRestCacheEntry cache)
        {
            this.GetConnection()
                .DeleteFrom("CacheEntries")
                .Where("BaseUrl", cache.BaseUrl)
                .Where("Content", cache.Content)
                .Where("ETag", cache.ETag)
                .Where("Resource", cache.Resource)
                .Go();
        }
    }
}
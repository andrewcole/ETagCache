namespace Illallangi.ETagCache.Model
{
    public class CacheEntry : IRestCacheEntry
    {
        public int? Id { get; set; }

        public string BaseUrl { get; set; }
        
        public string Resource { get; set; }

        public string ETag { get; set; }

        public string Content { get; set; }
    }
}

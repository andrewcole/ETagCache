namespace Illallangi.ETagCache.Model
{
    public class CacheEntry
    {
        public int? Id { get; set; }

        public string Resource { get; set; }

        public string ETag { get; set; }

        public string Value { get; set; }
    }
}

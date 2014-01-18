using System.Configuration;

namespace Illallangi.ETagCache.Config
{
    public sealed class ValueElement : ConfigurationElement
    {
        [ConfigurationProperty("Value", IsRequired = true)]
        public string Value
        {
            get { return (string)this["Value"]; }
            set { this["Value"] = value; }
        }
    }
}
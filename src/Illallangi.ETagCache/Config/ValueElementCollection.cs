using System.Configuration;

namespace Illallangi.ETagCache.Config
{
    public sealed class ValueElementCollection : ConfigurationElementCollection
    {
        public ValueElement this[int index]
        {
            get
            {
                return this.BaseGet(index) as ValueElement;
            }

            set
            {
                if (this.BaseGet(index) != null)
                {
                    this.BaseRemoveAt(index);
                }

                this.BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ValueElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ValueElement)element).Value;
        }
    }
}
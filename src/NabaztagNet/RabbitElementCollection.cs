using System.Configuration;

namespace NabaztagNet
{
    [ConfigurationCollection(typeof(Rabbit))]
    public class RabbitElementCollection : ConfigurationElementCollection
    {
        public Rabbit this[int index]
        {
            get
            {
                return (Rabbit)BaseGet(index);
            }
        }

        public new Rabbit this[string key]
        {
            get
            {
                return (Rabbit)BaseGet(key);
            }
        }

        public void Add(Rabbit element)
        {
            BaseAdd(element);
        }

        public void Remove(string key)
        {
            BaseRemove(key);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new Rabbit();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Rabbit)element).Name;
        }
    }
}

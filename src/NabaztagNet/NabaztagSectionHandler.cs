using System.Configuration;

namespace NabaztagNet
{
    public class NabaztagSectionHandler : ConfigurationSection
    {
        [ConfigurationProperty("rabbits")]
        public RabbitElementCollection Rabbits
        {
            get
            {
                return (RabbitElementCollection)base["rabbits"];
            }
        }
    }
}

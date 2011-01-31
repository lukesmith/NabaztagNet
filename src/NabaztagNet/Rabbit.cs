using System.Configuration;

namespace NabaztagNet
{
    public class Rabbit : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)base["name"];
            }

            set
            {
                base["name"] = value;
            }
        }

        [ConfigurationProperty("serialNumber", IsKey = false, IsRequired = true)]
        public string SerialNumber
        {
            get
            {
                return (string)base["serialNumber"];
            }

            set
            {
                base["serialNumber"] = value;
            }
        }

        [ConfigurationProperty("token", IsKey = false, IsRequired = true)]
        public string Token
        {
            get
            {
                return (string)base["token"];
            }

            set
            {
                base["token"] = value;
            }
        }

        [ConfigurationProperty("key", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Key
        {
            get
            {
                return (string)base["key"];
            }

            set
            {
                base["key"] = value;
            }
        }

        [ConfigurationProperty("enabled", DefaultValue = true, IsRequired = false)]
        public bool Enabled
        {
            get
            {
                return (bool)base["enabled"];
            }

            set
            {
                base["enabled"] = value;
            }
        }
    }
}

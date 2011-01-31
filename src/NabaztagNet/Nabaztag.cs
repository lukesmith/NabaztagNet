using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Xml;
using NabaztagNet.Helpers;

namespace NabaztagNet
{
    public sealed class Nabaztag
    {
        private readonly string name;
        private readonly string key;
        private readonly string serialNumber;
        private readonly string token;

        private static NabaztagSectionHandler config;

        /// <summary>
        /// Creates an instance of Nabaztag class.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="serialNumber"></param>
        /// <param name="token"></param>
        public Nabaztag(string key, string serialNumber, string token)
        {
            if (string.IsNullOrEmpty("serialNumber"))
            {
                throw new ArgumentNullException("serialNumber");
            }

            if (string.IsNullOrEmpty("token"))
            {
                throw new ArgumentNullException("token");
            }

            this.key = key;
            this.serialNumber = serialNumber;
            this.token = token;
        }

        private Nabaztag(string name, string key, string serialNumber, string token)
        {
            if (string.IsNullOrEmpty("name"))
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrEmpty("serialNumber"))
            {
                throw new ArgumentNullException("serialNumber");
            }

            if (string.IsNullOrEmpty("token"))
            {
                throw new ArgumentNullException("token");
            }

            this.name = name;
            this.key = key;
            this.serialNumber = serialNumber;
            this.token = token;
        }

        public event EventHandler<NabaztagResponseEventArgs> Response
        {
            add
            {
                this.response += value;
            }

            remove
            {
                this.response -= value;
            }
        }
        
        private event EventHandler<NabaztagResponseEventArgs> response;

        public string Name
        {
            get { return this.name; }
        }

        public string Key
        {
            get { return this.key; }
        }

        public string SerialNumber
        {
            get { return this.serialNumber; }
        }

        public string Token
        {
            get { return this.token; }
        }

        public bool Enabled { get; set; }

        private static NabaztagSectionHandler Config
        {
            get
            {
                if (config == null)
                {
                    config = (NabaztagSectionHandler)ConfigurationManager.GetSection("nabaztagSection");
                }

                return config;
            }
        }

        /// <summary>
        /// Gets a <see cref="Nabaztag"/> object configured from the nabaztagSection in the config file.
        /// </summary>
        /// <param name="name">Name of the rabbit in the config file.</param>
        /// <returns></returns>
        /// <exception cref="ConfigurationErrorsException"></exception>
        public static Nabaztag GetRabbit(string name)
        {
            if (string.IsNullOrEmpty("name"))
            {
                throw new ArgumentNullException("name");
            }
            
            Rabbit rabbit = Config.Rabbits[name];
            
            if (rabbit == null)
            {
                throw new ConfigurationErrorsException("Configuration for rabbit " + name + " does not exist.");
            }

            Nabaztag nab = new Nabaztag(rabbit.Name, rabbit.Key, rabbit.SerialNumber, rabbit.Token);
            nab.Enabled = rabbit.Enabled;

            return nab;
        }

        /// <summary>
        /// Sends a message to a rabbit configured in the config file.
        /// </summary>
        /// <param name="name">Name of the rabbit in the config file.</param>
        /// <param name="text">Text for the rabbit to read out.</param>
        /// <param name="voice">Voice which the text should be read in.</param>
        public static void SendMessage(string name, string text, Voice voice)
        {
            if (string.IsNullOrEmpty("name"))
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrEmpty("text"))
            {
                throw new ArgumentNullException("text");
            }

            Nabaztag nab = GetRabbit(name);
            
            if (nab != null)
            {
                NabaztagMessage eve = new NabaztagMessage();
                eve.Speech = new TextToSpeech(text, voice, 1, 1);
                nab.SendEvent(eve);
            }
        }

        public void SendEvent(INabaztagMessage message)
        {
            if (string.IsNullOrEmpty("message"))
            {
                throw new ArgumentNullException("message");
            }

            if (this.Enabled)
            {
                Uri url = message.GenerateUrl(this);

                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                this.ParseResponse(responseStream);
            }
        }

        private void ParseResponse(Stream response)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(response);
            XmlNode node = doc.DocumentElement;
            XmlNodeList commentNode = node.SelectNodes("/rsp/comment");

            string comment = string.Empty;

            if (commentNode.Count == 1)
            {
                comment = commentNode[0].InnerText;
            }

            XmlNodeList messageNode = node.SelectNodes("/rsp/message");
            if (messageNode != null)
            {
                if (messageNode.Count == 1)
                {
                    string message = messageNode[0].InnerText;
                    if (message == "POSITIONEAR")
                    {
                        XmlNodeList leftEarNode = node.SelectNodes("/rsp/leftposition");
                        XmlNodeList rightEarNode = node.SelectNodes("/rsp/rightposition");
                        EarPosition leftPosition = (EarPosition)Enum.ToObject(typeof(EarPosition), Convert.ToInt32(leftEarNode[0].InnerText));
                        EarPosition rightPosition = (EarPosition)Enum.ToObject(typeof(EarPosition), Convert.ToInt32(rightEarNode[0].InnerText));

                        EventsHelper.DoEvent(this.response, this, new NabaztagResponseEventArgs(comment, leftPosition, rightPosition));
                    }
                    else if (message == "EARPOSITIONSEND")
                    {
                        EventsHelper.DoEvent(this.response, this, new NabaztagResponseEventArgs(comment));
                    }
                    else if (message == "TTSSEND")
                    {
                        EventsHelper.DoEvent(this.response, this, new NabaztagResponseEventArgs(comment));
                    }
                    else if (message == "NOTAVAILABLE")
                    {
                        EventsHelper.DoEvent(this.response, this, new NabaztagResponseEventArgs(comment, true));
                    }
                    else if (message == "NOGOODTOKENORSERIAL")
                    {
                        EventsHelper.DoEvent(this.response, this, new NabaztagResponseEventArgs(comment, true));
                    }
                }
            }
        }
    }
}

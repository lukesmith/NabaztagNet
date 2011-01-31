using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NabaztagNet
{
    public sealed class PodcastMessage : INabaztagMessage
    {
        private const string NabaztagPodcastApiUrl = "http://api.nabaztag.com/vl/FR/api_stream.jsp";
        private List<Uri> podcasts;

        public PodcastMessage()
        {
        }

        public PodcastMessage(IEnumerable<Uri> podcasts)
        {
            this.podcasts = new List<Uri>(podcasts);
        }
        
        public Collection<Uri> Podcasts
        {
            get
            {
                if (this.podcasts == null)
                {
                    this.podcasts = new List<Uri>();
                }

                return new Collection<Uri>(this.podcasts);
            }
        }
        
        Uri INabaztagMessage.GenerateUrl(Nabaztag nab)
        {
            string url = NabaztagPodcastApiUrl;

            if (this.Podcasts.Count > 0)
            {
                string queryString = "sn=" + nab.SerialNumber;
                queryString += "&token=" + nab.Token;
                queryString += "&urlList=";

                foreach (Uri uri in this.Podcasts)
                {
                    queryString += uri + "|";
                }

                url += "?" + queryString.TrimEnd('|');
            }

            return new Uri(url);
        }
    }
}

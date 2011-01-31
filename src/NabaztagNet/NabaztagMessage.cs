using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NabaztagNet
{
    public sealed class NabaztagMessage : INabaztagMessage
    {
        private const string NabaztagApiUrl = "http://api.nabaztag.com/vl/FR/api.jsp";
        private List<Choreography> choreography;

        public EarPosition? RightEar { get; set; }

        public EarPosition? LeftEar { get; set; }

        public TextToSpeech Speech { get; set; }

        public bool RequestEarPosition { get; set; }

        public Collection<Choreography> Choreography
        {
            get
            {
                if (this.choreography == null)
                {
                    this.choreography = new List<Choreography>();
                }

                return new Collection<Choreography>(this.choreography);
            }
        }

        Uri INabaztagMessage.GenerateUrl(Nabaztag nab)
        {
            string url = NabaztagApiUrl;
            string queryString = this.GenerateQueryString(nab);

            if (!string.IsNullOrEmpty(queryString))
            {
                url += "?" + queryString;
            }

            return new Uri(url);
        }

        private string GenerateQueryString(Nabaztag nab)
        {
            string queryString = string.Empty;

            queryString += "sn=" + nab.SerialNumber;
            queryString += "&token=" + nab.Token;

            if (this.Speech != null)
            {
                queryString += "&voice=" + this.Speech.Voice;
                queryString += "&tts=" + this.Speech.Text;
            }

            if (this.RightEar.HasValue)
            {
                queryString += "&rightear=" + (int)this.RightEar.Value;
            }

            if (this.LeftEar.HasValue)
            {
                queryString += "&leftear=" + (int)this.LeftEar.Value;
            }

            if (this.RequestEarPosition)
            {
                queryString += "&ears=ok";
            }

            string chor = string.Empty;
            foreach (Choreography choreography in this.Choreography)
            {
                chor += choreography + ",";
            }

            chor = chor.TrimEnd(',');

            if (!string.IsNullOrEmpty(chor))
            {
                queryString += "&chor=" + chor;
            }

            return queryString;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Windows.Data.Xml.Dom;

namespace Wwear
{
    public class Weather
    {
        public string query { get; set; } // cityname, countryname
        public string observation_time { get; set; }
        public string date { get; set; }
        public string temp_C { get; set; }
        public string tempMaxC { get; set; }
        public string tempMinC { get; set; }
        public string weatherIconUrl { get; set; }
        public string windspeedKmph { get; set; }
        public string humidity { get; set; }
    }
}

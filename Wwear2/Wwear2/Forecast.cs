using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Wwear2
{
    public class Forecast
    {
        public string query { get; set; } // cityname, countryname
        public string observation_time { get; set; }
        public string date { get; set; }
        public string temp_C { get; set; }
        public string tempMaxC { get; set; }
        public string tempMinC { get; set; }
        public double tempMaxF { get; set; }
        public double tempMinF { get; set; }
        public string weatherIconUrl { get; set; }
        public string windspeedMph { get; set; }
        public string humidity { get; set; }
        public string precip { get; set; }
        public string city { get; set; }

        public void GetForecast(string lat, string lon)
        {
            UriBuilder simpleUri = new UriBuilder("api.openweathermap.org/data/2.5/forecast?");
            simpleUri.Query = "lat="+lat+"&lon="+lon+"&mode=xml";

            HttpWebRequest forecast = (HttpWebRequest)WebRequest.Create(simpleUri.Uri);

            //Forecast update state made these
            ForeCastUpdateState updateState = new ForeCastUpdateState();
            updateState.webRequest = forecast;

            forecast.BeginGetResponse(new AsyncCallback(HandleForecast), updateState);
        }

        private void HandleForecast(IAsyncResult result)
        {
            //Gets results
            ForeCastUpdateState updateState = (ForeCastUpdateState)result.AsyncState;
            HttpWebRequest forecast = (HttpWebRequest)updateState.webRequest;

            updateState.webResponse = (HttpWebResponse)forecast.EndGetResponse(result);

            Stream retrievedData;

            try
            {
                retrievedData = updateState.webResponse.GetResponseStream();

                //Finally xml
                XElement xmlWeather = XElement.Load(retrievedData);
                XElement parentTag;

                //Begin parsing

                //Gather City
                parentTag = xmlWeather.Descendants("location").First();
                city = (string)(parentTag.Element("name"));

                //Gather Temp
                parentTag = xmlWeather.Descendants("forecast").First();
                tempMinC = (string)(parentTag.Element("time").Element("temperature").Attribute("min"));
                tempMaxC = (string)(parentTag.Element("time").Element("temperature").Attribute("max"));

                //Convert to farenheight
                tempMinF = double.Parse(tempMinC) * (double)(9 / 5) + 32;
                tempMaxF = double.Parse(tempMaxC) * (double)(9 / 5) + 32;

                //Gather Wind Speed
                windspeedMph = (string)(parentTag.Element("time").Element("windSpeed").Attribute("mps"));

                //Gather humidity
                humidity = (string)(parentTag.Element("time").Element("humidity").Attribute("value"));

                //Gather precipitation
                precip = (string)(parentTag.Element("time").Element("precipitation").Attribute("value"));
            }
            catch (FormatException)
            {
                return;
            }
        }

        public class ForeCastUpdateState
        {
            public HttpWebRequest webRequest { get; set; }
            public HttpWebResponse webResponse { get; set; }
        }
    }

    
}

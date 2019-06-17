// -----------------------------------------------------------------------
// <copyright file="WeatherModel.cs">
//
// </copyright>
// <summary>Contains Weather widget model</summary>
// -----------------------------------------------------------------------

namespace WeatherWidget.Model
{
    using Newtonsoft.Json;
    using WeatherWidget.Properties;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Timers;
    using System.Xml;
    using WingetContract.ViewModel;

    /// <summary>
    /// Contains weather widget model
    /// </summary>
    internal class WeatherModel : ObservableObject
    {
        #region Properties

        /// <summary>
        /// Gets weather forecast
        /// </summary>
        public List<WeatherElement> WeatherForcast { get => _weatherForcast; private set => Set(ref _weatherForcast, value); }

        #endregion

        #region Private members

        /// <summary>
        /// Refresh timer
        /// </summary>
        private Timer _timer;

        /// <summary>
        /// Api link
        /// </summary>
        private string _link;

        /// <summary>
        /// Associates wheater name to weather enum value
        /// </summary>
        private Dictionary<string, WeathersEnum> _weatherConverter = new Dictionary<string, WeathersEnum>
        {
            {"Thunderstorm",  WeathersEnum.Thunderstorm},
            {"Drizzle",  WeathersEnum.Rain},
            {"Rain",  WeathersEnum.Rain},
            {"Snow",  WeathersEnum.Snow},
            {"Atmosphere",  WeathersEnum.Mist},
            {"Clouds", WeathersEnum.Clouds},
            {"Clear",  WeathersEnum.Sunny}
        };


        /// <summary>
        /// Weather forecast
        /// </summary>
        private List<WeatherElement> _weatherForcast;

        #endregion

        #region Contructor

        /// <summary>
        /// Default constructeur
        /// </summary>
        public WeatherModel()
        {
            _link = string.Format(Resources.LinkAddress, Resources.CityId, Resources.ApiId);

            _timer = new Timer(1800*1000) // Every 30 min
            {
                AutoReset = false
            };
            _timer.Elapsed += Refresh;
        }

        #endregion

        #region Public functions

        /// <summary>
        /// Performs a first update
        /// </summary>
        public void Initialize()
        {
            Refresh(null, null);
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Updates tram timers
        /// </summary>
        /// <param name="s">Sender</param>
        /// <param name="e">Arguments</param>
        private void Refresh(object s, ElapsedEventArgs e)
        {
            new Task(() =>
            {
                try
                {
                    XmlNode dataNode = GetData();

                    if (dataNode != null)
                    {
                        StringBuilder rssContent = new StringBuilder();

                        XmlNodeList nodes = dataNode.SelectNodes("list/list");
                        List<WeatherElement> forcast = new List<WeatherElement>();
                        double CurrentDate = (DateTime.Today.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;

                        bool odd = false;

                        foreach (XmlNode node in nodes)
                        {
                            // Only take every 6h
                            odd = !odd;
                            if (!odd)
                            {
                                continue;
                            }

                            WeatherElement weather = new WeatherElement();

                            if (nodes[0].SelectSingleNode("main/temp") != null)
                            {
                                // Get day
                                string[] time = node.SelectSingleNode("dt_txt").InnerXml.Split(' ');
                                string[] date = time[0].Split('-');
                                weather.Day = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2])).DayOfWeek.ToString().Substring(0,3) + ".";

                                // Get time
                                weather.Hour = time[1].Split(':')[0] + "h";
                                weather.Hour = weather.Hour.StartsWith("0") ? weather.Hour.Substring(1) : weather.Hour;

                                // Get temperature
                                double kelvin = XmlConvert.ToDouble(node.SelectSingleNode("main/temp").InnerXml);
                                weather.Temperature = KelToCel(kelvin).ToString("0.#") + "°C";

                                // Get weather
                                string currentWeather = node.SelectSingleNode("weather/main").InnerText;
                                weather.Weather = _weatherConverter[currentWeather];

                            }
                            else
                            {
                                weather.Weather = WeathersEnum.Unknown;
                                weather.Temperature = Resources.DefautValue;
                                weather.Hour = Resources.DefautValue;
                                weather.Day = Resources.DefautValue;
                            }

                            forcast.Add(weather);
                        }

                        WeatherForcast = forcast;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                _timer.Start();
            }
            ).Start();
        }

        /// <summary>
        /// Gets data on web
        /// </summary>
        /// <returns>XML data read</returns>
        private XmlNode GetData()
        {
            XmlNode xmlDoc = new XmlDocument();
            try
            {
                WebClient wc = new WebClient();
                var json = wc.DownloadString(_link);
                wc.Dispose();
                xmlDoc = JsonConvert.DeserializeXmlNode(json, "list");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                xmlDoc = null;
            }

            return xmlDoc;
        }

        /// <summary>
        /// Convert Kelvin to celcius
        /// </summary>
        /// <param name="kel">Kelvin value</param>
        /// <returns>Celcius value</returns>
        private double KelToCel(double kel)
        {
            return (kel - 273.15);
        }

        #endregion
    }
}
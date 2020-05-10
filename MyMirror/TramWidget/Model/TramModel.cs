// -----------------------------------------------------------------------
// <copyright file="TramModel.cs">
//
// </copyright>
// <summary>Contains Tram widget model</summary>
// -----------------------------------------------------------------------

namespace TramWidget.Model
{
    using TramWidget.Properties;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Timers;
    using System.Xml;
    using Common.ViewModel;
    using Common.Log;
    using Common.Settings;

    /// <summary>
    /// Contains Tram widget model
    /// </summary>
    internal class TramModel : ObservableObject
    {
        #region Properties
        /// <summary>

        /// <summary>
        /// Gets windget settings
        /// </summary>
        public SettingsManager<TramSettings> SettingsManager { get; internal set; }

        /// <summary>
        /// Gets Tram C direction
        /// </summary>
        public List<string> DirectionC { get => _directionC; private set => Set(ref _directionC, value); }

        /// <summary>
        /// Gets Tram E direction
        /// </summary>
        public List<string> DirectionE { get => _directionE; private set => Set(ref _directionE, value); }

        /// <summary>
        /// Gets Tram timers C - 1
        /// </summary>
        public List<string> NextTramC1 { get => _nextTramC1; private set => Set(ref _nextTramC1, value); }

        /// <summary>
        /// Gets Tram timers C - 2
        /// </summary>
        public List<string> NextTramC2 { get => _nextTramC2; private set => Set(ref _nextTramC2, value); }

        /// <summary>
        /// Gets Tram timers E - 1
        /// </summary>
        public List<string> NextTramE1 { get => _nextTramE1; private set => Set(ref _nextTramE1, value); }

        /// <summary>
        /// Gets Tram timers E - 2
        /// </summary>
        public List<string> NextTramE2 { get => _nextTramE2; private set => Set(ref _nextTramE2, value); }

        #endregion

        #region Private members

        /// <summary>
        /// Refresh timer
        /// </summary>
        private Timer _timer;

        /// <summary>
        /// Api link C - 1
        /// </summary>
        private string _linkC1;

        /// <summary>
        /// Api link C - 2
        /// </summary>
        private string _linkC2;

        /// <summary>
        /// Api link E - 1
        /// </summary>
        private string _linkE1;

        /// <summary>
        /// Api link E - 2
        /// </summary>
        private string _linkE2;

        /// <summary>
        /// Tram C direction
        /// </summary>
        private List<string> _directionC;

        /// <summary>
        /// Tram E direction
        /// </summary>
        private List<string> _directionE;

        /// <summary>
        /// Tram timers C - 1
        /// </summary>
        private List<string> _nextTramC1;

        /// <summary>
        /// Tram timers C - 2
        /// </summary>
        private List<string> _nextTramC2;

        /// <summary>
        /// Tram timers E - 1
        /// </summary>
        private List<string> _nextTramE1;

        /// <summary>
        /// Tram timers E - 2
        /// </summary>
        private List<string> _nextTramE2;

        #endregion

        #region Contructor

        /// <summary>
        /// Default constructeur
        /// </summary>
        public TramModel()
        {
            SettingsManager = new SettingsManager<TramSettings>();
            SettingsManager.Initialize(Resources.SettingsFileName);

            string address = SettingsManager.Settings.LinkAddress.Value;
            string tram1 = SettingsManager.Settings.Tram1.Value;
            string tram2 = SettingsManager.Settings.Tram2.Value;
            string tram3 = SettingsManager.Settings.Tram3.Value;
            string tram4 = SettingsManager.Settings.Tram4.Value;

            _linkC1 = string.Format(address, tram1);
            _linkC2 = string.Format(address, tram2);
            _linkE1 = string.Format(address, tram3);
            _linkE2 = string.Format(address, tram4);

            DirectionC = DirectionE = new List<string>() { string.Empty, string.Empty };

            NextTramC1 = NextTramC2 = NextTramE1 = NextTramE2 = new List <string> { Resources.DefaultTramText, Resources.DefaultTramText };

            _timer = new Timer(SettingsManager.Settings.TramPullFrequency.Value)
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
                    XmlNode dataNode;

                    // Get tram C 1
                    string direction1 = null;
                    List<string> nextTram = null;
                    dataNode = GetData(_linkC1);
                    ParseData(dataNode, ref direction1, ref nextTram);
                    NextTramC1 = nextTram;

                    // Get tram C 2
                    string direction2 = null;
                    dataNode = GetData(_linkC2);
                    ParseData(dataNode, ref direction2, ref nextTram);
                    NextTramC2 = nextTram;
                    DirectionC = new List<string> { direction1, direction2 };

                    // Get tram E 1
                    dataNode = GetData(_linkE1);
                    ParseData(dataNode, ref direction1, ref nextTram);
                    NextTramE1 = nextTram;

                    // Get tram E 2
                    dataNode = GetData(_linkE2);
                    ParseData(dataNode, ref direction2, ref nextTram);
                    NextTramE2 = nextTram;
                    DirectionE = new List<string> { direction1, direction2 };
                }
                catch (Exception ex)
                {
                    LogManager.LogLine(ex.Message);
                }
                _timer.Start();
            }
            ).Start();
        }

        /// <summary>
        /// Extract data from XML
        /// </summary>
        /// <param name="dataNode">XML node</param>
        /// <param name="direction">Read direction</param>
        /// <param name="nextTram">Read tram times</param>
        private void ParseData(XmlNode dataNode, ref string direction, ref List<string> nextTram)
        {
            direction = Resources.DefaultTramText;
            nextTram = new List<string> { Resources.DefaultTramText, Resources.DefaultTramText };

            if (dataNode != null)
            {
                Encoding iso = Encoding.GetEncoding("ISO-8859-1");
                Encoding utf8 = Encoding.UTF8;

                // Next 
                XmlNodeList nodes = dataNode.SelectNodes("root/Row");

                bool firstNode = true;


                List<int> firsts = new List<int> { -1, -1 };
                foreach (XmlNode node in nodes)
                {
                    XmlNodeList nds = node.SelectNodes("times");

                    if (nds.Count > 1)
                    {
                        if (firstNode)
                        {
                            byte[] utfBytes = utf8.GetBytes(node.SelectSingleNode("pattern/desc").InnerXml);
                            byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
                            direction = utf8.GetString(isoBytes);
                            firsts = new List<int> { XmlConvert.ToInt32(nds[0].SelectSingleNode("realtimeArrival").InnerText),
                                                            XmlConvert.ToInt32(nds[1].SelectSingleNode("realtimeArrival").InnerText)};

                            firstNode = false;
                        }
                        else if (XmlConvert.ToInt32(nds[0].SelectSingleNode("realtimeArrival").InnerText) < firsts[0])
                        {
                            byte[] utfBytes = utf8.GetBytes(node.SelectSingleNode("pattern/desc").InnerXml);
                            byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
                            direction = utf8.GetString(isoBytes);

                            if (XmlConvert.ToInt32(nds[1].SelectSingleNode("realtimeArrival").InnerText) < firsts[0])
                            {
                                firsts[1] = XmlConvert.ToInt32(nds[1].SelectSingleNode("realtimeArrival").InnerText);
                            }
                            else
                            {
                                firsts[1] = firsts[0];
                            }
                            firsts[0] = XmlConvert.ToInt32(nds[0].SelectSingleNode("realtimeArrival").InnerText);
                        }
                        else if (XmlConvert.ToInt32(nds[0].SelectSingleNode("realtimeArrival").InnerText) < firsts[1])
                        {
                            firsts[1] = XmlConvert.ToInt32(nds[0].SelectSingleNode("realtimeArrival").InnerText);
                        }
                    }
                }

                nextTram = new List<string>
                        {
                            (firsts[0] == -1) ? Resources.DefaultTramText : SecToString(firsts[0]),
                            (firsts[1] == -1) ? Resources.DefaultTramText : SecToString(firsts[1])
                        };
            }
        }

        /// <summary>
        /// Gets XML data on web
        /// </summary>
        /// <param name="link">Data adress</param>
        /// <returns>XML data</returns>
        private XmlNode GetData(string link)
        {
            XmlNode xmlDoc = new XmlDocument();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                WebClient wc = new WebClient();
                string json = wc.DownloadString(link);

                wc.Dispose();
                xmlDoc = JsonConvert.DeserializeXmlNode("{\"Row\":" + json + "}", "root");
            }
            catch (Exception ex)
            {
                LogManager.LogLine(ex.Message);
                xmlDoc = null;
            }

            return xmlDoc;
        }

        /// <summary>
        /// Converts integer sec in time string
        /// </summary>
        /// <param name="sec">Seconds</param>
        /// <returns>Returned string</returns>
        private string SecToString(int sec)
        {
            int delay = (sec - (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second)) / 60;

            delay = delay == 0 ? 1 : delay;

            string delayString = delay.ToString() + " min";

            return delayString;
        }

        #endregion
    }
}

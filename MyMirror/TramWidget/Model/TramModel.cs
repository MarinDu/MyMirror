
namespace TramWidget.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Timers;
    using System.Xml;
    using WingetContract.ViewModel;

    public class TramModel : ObservableObject
    {

        private Timer _timer;

        public TramModel()
        {
            _timer = new Timer(59000)
            {
                AutoReset = false
            };
            _timer.Elapsed += Refresh;      
        }

        public void Initialize()
        {
            UptadeTime(null, null);
        }

        private void UptadeTime(object sender, ElapsedEventArgs e)
        {

            _timer.Start();
        }

        /// <summary>
        /// Tram sens 1
        /// </summary>
        private List<string> _nextTram1;
        public List<string> NextTram1 { get => _nextTram1; set => Set(ref _nextTram1, value); }

        /// <summary>
        /// Tram sens 1
        /// </summary>
        private string _direction1;
        public string Direction1 { get => _direction1; set => Set(ref _direction1, value); }

        /// <summary>
        /// Tram Sens 2
        /// </summary>
        private List<string> _nextTram2;
        public List<string> NextTram2 { get => _nextTram2; set => Set(ref _nextTram2, value); }

        /// <summary>
        /// Tram sens 2
        /// </summary>
        private string _direction2;
        public string Direction2 { get => _direction2; set => Set(ref _direction2, value); }

        /// <summary>
        /// Lien Api sens 1
        /// </summary>
        private string _link1;

        /// <summary>
        /// Lien Api sens 2
        /// </summary>
        private string _link2;


        /// <summary>
        /// Constructeur
        /// </summary>
        public TramModel(string tram1ID, string tram2ID)
        {
            _link1 = "https://data.metromobilite.fr/api/routers/default/index/stops/" + tram1ID + "/stoptimes";
            _link2 = "https://data.metromobilite.fr/api/routers/default/index/stops/" + tram2ID + "/stoptimes";

            NextTram1 = new List<string> { "-", "-" };
            NextTram2 = new List<string> { "-", "-" };
        }

        /// <summary>
        /// Thread de mise a jour des données affichées dans le new feed
        /// </summary>
        private void Refresh(object s, ElapsedEventArgs e)
        {
            new Task(() =>
            {
                try
                {
                    Encoding iso = Encoding.GetEncoding("ISO-8859-1");
                    Encoding utf8 = Encoding.UTF8;

                    // Récupération tram 1
                    XmlNode dataNode = GetData(_link1);
                    if (dataNode != null)
                    {
                        StringBuilder rssContent = new StringBuilder();

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
                                    Direction1 = utf8.GetString(isoBytes);
                                    firsts = new List<int> { XmlConvert.ToInt32(nds[0].SelectSingleNode("realtimeArrival").InnerText),
                                                            XmlConvert.ToInt32(nds[1].SelectSingleNode("realtimeArrival").InnerText)};

                                    firstNode = false;
                                }
                                else if (XmlConvert.ToInt32(nds[0].SelectSingleNode("realtimeArrival").InnerText) < firsts[0])
                                {
                                    byte[] utfBytes = utf8.GetBytes(node.SelectSingleNode("pattern/desc").InnerXml);
                                    byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
                                    Direction1 = utf8.GetString(isoBytes);

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
                        NextTram1[0] = (firsts[0] == -1) ? "-" : SecToString(firsts[0]);
                        NextTram1[1] = (firsts[1] == -1) ? "-" : SecToString(firsts[1]);

                        NotifyPropertyChanged(nameof(NextTram1));
                    }


                    // Récupération tram 2
                    dataNode = GetData(_link2);
                    if (dataNode != null)
                    {
                        StringBuilder rssContent = new StringBuilder();

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
                                    Direction2 = utf8.GetString(isoBytes);
                                    firsts = new List<int> { XmlConvert.ToInt32(nds[0].SelectSingleNode("realtimeArrival").InnerText),
                                                            XmlConvert.ToInt32(nds[1].SelectSingleNode("realtimeArrival").InnerText)};

                                    firstNode = false;
                                }
                                else if (XmlConvert.ToInt32(nds[0].SelectSingleNode("realtimeArrival").InnerText) < firsts[0])
                                {
                                    byte[] utfBytes = utf8.GetBytes(node.SelectSingleNode("pattern/desc").InnerXml);
                                    byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
                                    Direction2 = utf8.GetString(isoBytes);

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
                        NextTram2[0] = (firsts[0] == -1) ? "-" : SecToString(firsts[0]);
                        NextTram2[1] = (firsts[1] == -1) ? "-" : SecToString(firsts[1]);

                        NotifyPropertyChanged(nameof(NextTram2));
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
        /// récupère le sdonnées RSS sous format XML
        /// </summary>
        /// <returns></returns>
        private XmlNode GetData(string link)
        {
            XmlNode xmlDoc = new XmlDocument();
            try
            {
                ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                WebClient wc = new WebClient();
                string json = wc.DownloadString(link);

                wc.Dispose();
                xmlDoc = JsonConvert.DeserializeXmlNode("{\"Row\":" + json + "}", "root");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                xmlDoc = null;
            }

            return xmlDoc;
        }

        private string SecToString(int sec)
        {
            int delay = (sec - (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second)) / 60;

            string delayString = delay.ToString() + "min";

            return delayString;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelApp.Models
{
    class Ip
    {
        #region ip
        static IPAddress getInternetIPAddress()
        {
            try
            {
                IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());
                IPAddress gateway = IPAddress.Parse(getInternetGateway());
                return findMatch(addresses, gateway);
            }
            catch (FormatException) { return null; }
        }

        static string getInternetGateway()
        {
            using (Process tracert = new Process())
            {
                ProcessStartInfo startInfo = tracert.StartInfo;
                startInfo.FileName = "tracert.exe";
                startInfo.Arguments = "-h 1 208.77.188.166"; // www.example.com
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                tracert.Start();

                using (StreamReader reader = tracert.StandardOutput)
                {
                    string line = "";
                    for (int i = 0; i < 9; ++i)
                        line = reader.ReadLine();
                    line = line.Trim();
                    return line.Substring(line.LastIndexOf(' ') + 1);
                }
            }
        }

        static IPAddress findMatch(IPAddress[] addresses, IPAddress gateway)
        {
            byte[] gatewayBytes = gateway.GetAddressBytes();
            foreach (IPAddress ip in addresses)
            {
                byte[] ipBytes = ip.GetAddressBytes();
                if (ipBytes[0] == gatewayBytes[0]
                    && ipBytes[1] == gatewayBytes[1]
                    && ipBytes[2] == gatewayBytes[2])
                {
                    return ip;
                }
            }
            return null;
        }
        #endregion

        public static string GetIP()
        {
            WebClient wc = new WebClient();
            string strIP = wc.DownloadString("http://checkip.dyndns.org");
            strIP = (new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b")).Match(strIP).Value;
            wc.Dispose();
            return strIP;
        }

        public static string LocaIp()
        {
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            return localIP;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public static string GetUserCity()
        {
            //WebBrowser web = new WebBrowser();
            //web.Navigate(@"https://www.whatismyip.com/my-ip-information");
            //string strIP = web.DocumentText;
            //System.Windows.Forms.HtmlDocument document = web.Document;

            WebClient wc = new WebClient();
            string strIP = wc.DownloadString("https://www.whatismyip.com/my-ip-information/");
            string find = "City: ";
            for (int i = 0; i < strIP.Length - find.Length - 1; i++)
            {
                string b = strIP.Substring(i, find.Length);
                if (b == find)
                {
                    string ostatok = strIP.Substring(i + find.Length, 100);
                    return strIP.Substring(i, ostatok.Length);
                }
            }
            wc.Dispose();
            return strIP;
        }

        public static IpInfo GetUserCountryByIp(string ip = "")
        {
            IpInfo ipInfo = new IpInfo();
            try
            {
                string info;
                //if (ip.Length != 0)
                //    info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
                //else
                    info = new WebClient().DownloadString("https://api.ipdata.co/?api-key=0b5fa0d7f2d1dd9346371d717dda7eec172039fdb5445ad860253faf");
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                //RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                //ipInfo.Country = myRI1.EnglishName;
            }
            catch (Exception)
            {
                //ipInfo.Country = null;
            }
            //return ipInfo.Country;
            return ipInfo;
        }

        public static bool CheckConnection(String URL)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Timeout = 5000;
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }

    public class IpInfo
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("loc")]
        public string Loc { get; set; }

        [JsonProperty("org")]
        public string Org { get; set; }

        [JsonProperty("postal")]
        public string Postal { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("calling_code")]
        public string Calling_code { get; set; }
    }
}

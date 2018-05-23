using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace NewsService01
{
    public class ProcessRss
    {
        IDictionary<string, string> UrlMap;
        public ProcessRss()
        {
            UrlMap = new Dictionary<string, string>();
            UrlMap["nt"] = "http://www.nt.se/nyheter/norrkoping/rss/";
            UrlMap["expressen"] = "http://www.expressen.se/Pages/OutboundFeedsPage.aspx?id=3642159&viewstyle=rss";
            UrlMap["svd"] = "https://www.svd.se/?service=rss";
           // User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.181 Safari/537.36

        }
        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public List<Feeds> Process(string SiteName)
        {

            List<Feeds> feeds = new List<Feeds>();
            string url = UrlMap[SiteName];
            ServicePointManager.ServerCertificateValidationCallback += AcceptAllCertifications;
            ServicePointManager.SecurityProtocol =SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            var webClient = new WebClient();

            string result = webClient.DownloadString(url);


            XDocument xDoc = XDocument.Parse(result);
            //XDocument xDoc = XDocument.Load(url);
           //XDocument xDoc = XDocument.Load(RSSData);
            
            
            var items = (from x in xDoc.Descendants("item")
                         select new
                         {
                             title = x.Element("title").Value,
                             link = x.Element("link").Value,
                             pubDate = x.Element("pubDate").Value,
                             description = x.Element("description").Value
                         });
            if (items != null)
            {
                feeds.AddRange(items.Select(i => new Feeds
                {
                    Title = i.title,
                    Link = i.link,
                    PublishDate = i.pubDate,
                    Description = i.description
                }));
            }
            return feeds;

        }
    }
}
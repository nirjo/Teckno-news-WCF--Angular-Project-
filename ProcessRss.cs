using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Text;

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
        

        }
        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public string myFormatter(string StringToCnage) {
            byte[] bytes = Encoding.Default.GetBytes(StringToCnage);
            StringToCnage = Encoding.UTF8.GetString(bytes);
            return StringToCnage;
        
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
          
            if (SiteName == "nt" )
            {

                var items = (from x in xDoc.Descendants("item")
                             select new
                             {
                                 
                                 title = (x.Element("title").Value),
                                 link = x.Element("link").Value,
                                 pubDate = x.Element("pubDate").Value,
                                 description = (x.Element("description").Value),
                                 category = x.Element("category").Value
                             });
                if (items != null)
                {
                    feeds.AddRange(items.Select(i => new Feeds
                    {
                        Title = i.title,
                        Link = i.link,
                        PublishDate = i.pubDate,
                        Description = i.description,
                        Category = i.category
                    }));
                }
            } else if ( SiteName == "svd"){

                var items = (from x in xDoc.Descendants("item")
                             select new
                             {
                                
                                 title = myFormatter(x.Element("title").Value),
                                 link = x.Element("link").Value,
                                 pubDate = x.Element("pubDate").Value,
                                 description = myFormatter(x.Element("description").Value),
                                 category = myFormatter(x.Element("category").Value)
                             });
                if (items != null)
                {
                    feeds.AddRange(items.Select(i => new Feeds
                    {
                        Title = i.title,
                        Link = i.link,
                        PublishDate = i.pubDate,
                        Description = i.description,
                        Category = i.category
                    }));
                }
            } else
            {
                var items = (from x in xDoc.Descendants("item")
                             select new
                             {
                                 title = myFormatter(x.Element("title").Value),
                                 link = x.Element("link").Value,
                                 pubDate = x.Element("pubDate").Value,
                                 description = myFormatter(x.Element("description").Value)
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
            }
            return feeds;

        }
    }
}

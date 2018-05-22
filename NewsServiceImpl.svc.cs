using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

//using System.Web;
//using System.Web.Mvc; 

namespace NewsService01
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "NewsServiceImpl" in code, svc and config file together.
    public class NewsServiceImpl : INewsServiceImpl
    {
        #region IRestServiceImpl Members

        public string XMLData(string id)
        {
            return "XML";
        }

        public string JSONData(string id)
        {

            ProcessRss processRss = new ProcessRss();
            List<Feeds> feeds = processRss.Process(id);

            return JsonConvert.SerializeObject(feeds);
         
        }

        #endregion
    }
}

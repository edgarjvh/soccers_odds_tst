using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using ws_feeds_reader.models.soccer_odds;

namespace ws_feeds_reader
{    
    [WebService(Namespace = "http://feedsreader.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]

    public class soccersodds : WebService
    {
        [WebMethod]
        public object ReadSoccersOddsFeed(string xmlUrl)
        {
            try
            {               
                // validar que sea el deporte correcto leído por este método
                XDocument xDoc = XDocument.Load(xmlUrl);
                string sport = xDoc.Root.Attribute("sport").Value;

                if (sport.Equals("soccer"))
                {
                    var stream = File.Open(xmlUrl, FileMode.Open);
                    XmlSerializer ser = new XmlSerializer(typeof(Scores));
                    var result = ser.Deserialize(stream) as Scores;
                    return result;
                }
                else
                {
                    return "incorrect sport";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

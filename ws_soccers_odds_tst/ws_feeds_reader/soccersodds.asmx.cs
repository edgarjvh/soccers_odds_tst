using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        [WebMethod][XmlInclude(typeof(Scores))]
        public object ReadSoccersOddsFeed(string url)
        {
            try
            {               
                var request = WebRequest.Create(url);
                var response = request.GetResponse();

                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                {                    
                    Stream dataStream = response.GetResponseStream();                    
                    StreamReader reader = new StreamReader(dataStream);
                    XmlSerializer serializer = new XmlSerializer(typeof(Scores));

                    Scores Scores = (Scores)serializer.Deserialize(reader);                    

                    /* 
                     * Colocar un punto de interrupción para analizar el objeto 'Scores'
                     * De aquí en adelante comenzaría el procesado de inserción en la BD
                     */

                    return Scores;
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

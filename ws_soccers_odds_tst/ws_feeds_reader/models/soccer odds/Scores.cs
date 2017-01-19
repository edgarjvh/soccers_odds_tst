using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace ws_feeds_reader.models.soccer_odds
{
    [XmlRoot("scores")]
    public class Scores
    {
        [XmlAttribute("sport")]
        public string Sport { get; set; }

        [XmlAttribute("ts")]
        public string TimeStamp { get; set; }

        [XmlElement("category")]
        public List<Category> Categories { get; set; }

        public Scores()
        {
            Categories = new List<Category>();
        }
    }
}
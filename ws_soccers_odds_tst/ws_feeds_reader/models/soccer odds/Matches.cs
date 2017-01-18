using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using ws_feeds_reader.models.socccer_odds;

namespace ws_feeds_reader.models.soccer_odds
{
    public class Matches
    {
        [XmlAttribute("date")]
        public string Date { get; set; }

        [XmlAttribute("formatted_date")]
        public string FormattedDate { get; set; }

        [XmlElement("match")]
        public List<Match> List { get; set; }

        public Matches()
        {
            List = new List<Match>();
        }
    }
}
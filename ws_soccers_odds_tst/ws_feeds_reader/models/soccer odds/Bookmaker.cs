using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace ws_feeds_reader.models.socccer_odds
{
    public class Bookmaker
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("extra")]
        public string Extra { get; set; }

        [XmlElement("odd")]
        public List<Odd> Odds { get; set; }

        [XmlElement("total")]
        public List<OddTotal> Totals { get; set; }

        [XmlElement("handicap")]
        public List<OddHandicap> Handicaps { get; set; }
    }
}
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace ws_feeds_reader.models.socccer_odds
{
    public class TvStations
    {
        [XmlElement("tv")]
        public List<TvStation> List { get; set; }
    }
}
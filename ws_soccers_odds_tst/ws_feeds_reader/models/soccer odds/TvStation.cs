using System.Xml;
using System.Xml.Serialization;

namespace ws_feeds_reader.models.socccer_odds
{
    public class TvStation
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
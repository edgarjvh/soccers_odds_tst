﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace ws_feeds_reader.models.socccer_odds
{
    public class OddTotal
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
       
        [XmlAttribute("main")]
        public string Main { get; set; }

        [XmlElement("odd")]
        public List<Odd> Odds { get; set; }

        public OddTotal()
        {
            Odds = new List<Odd>();
        }
    }
}
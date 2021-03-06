﻿using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace ws_feeds_reader.models.socccer_odds
{
    public class Match
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("fix_id")]
        public string FixId { get; set; }

        [XmlAttribute("static_id")]
        public string StaticId { get; set; }

        [XmlAttribute("venue")]
        public string Stadium { get; set; }

        [XmlAttribute("date")]
        public string Date { get; set; }

        [XmlAttribute("formatted_date")]
        public string FormattedDate { get; set; }

        [XmlAttribute("time")]
        public string Time { get; set; }

        [XmlAttribute("status")]
        public string Status { get; set; }

        [XmlElement("localteam")]
        public Team LocalTeam { get; set; }

        [XmlElement("visitorteam")]
        public Team VisitorTeam { get; set; }

        [XmlElement("events")]
        public Events Events { get; set; }

        [XmlElement("ht")]
        public HalfTime HalfTime { get; set; }

        [XmlElement("ft")]
        public FinishTime FinishTime { get; set; }

        [XmlElement("odds")]
        public Odds Odds { get; set; }

        [XmlElement("tv_stations")]
        public TvStations TvStations { get; set; }

        public Match()
        {
            Events = new Events();
            LocalTeam = new Team();
            VisitorTeam = new Team();
            HalfTime = new HalfTime();
            FinishTime = new FinishTime();
            Odds = new Odds();
            TvStations = new TvStations();
        }
    }
}
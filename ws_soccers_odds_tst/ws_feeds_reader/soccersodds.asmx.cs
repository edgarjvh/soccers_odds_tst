using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web.Services;
using System.Xml.Serialization;
using ws_feeds_reader.data;
using ws_feeds_reader.models.socccer_odds;
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
                    
                    // ===============================================================

                    Procedure proc = new Procedure();                    

                    foreach (Category CATEGORY in Scores.Categories)
                    {
                        if (CATEGORY.Id != null)
                        {
                            long category_id = long.Parse(CATEGORY.Id);
                            long category_gid = CATEGORY.GID == null ? 0 : long.Parse(CATEGORY.GID);
                            string category_name = CATEGORY.Name == null ? "" : CATEGORY.Name;
                            string category_file_group = CATEGORY.FileGroup == null ? "" : CATEGORY.FileGroup;
                            int category_is_cup = CATEGORY.IsCup == null ? 0 : CATEGORY.IsCup.ToLower().Equals("false") ? 0 : 1;

                            // ============= saving categories into database ==============
                            proc.GetData(
                                            "Categories_save",
                                            category_id,
                                            category_gid,
                                            category_name,
                                            category_file_group,
                                            category_is_cup);
                            // ============================================================

                            foreach (Match MATCH in CATEGORY.Matches.List)
                            {
                                if (MATCH.Id != null)
                                {
                                    long match_id = 0;
                                    long.TryParse(MATCH.Id, out match_id);
                                    string match_status = MATCH.Status == null ? "" : MATCH.Status;
                                    string date = MATCH.FormattedDate;
                                    string time = MATCH.Time;
                                    DateTime date_time;
                                    DateTime.TryParseExact(date + " " + time, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out date_time);
                                    string match_stadium = MATCH.Stadium == null ? "" : MATCH.Stadium;
                                    long match_static_id = 0;
                                    long.TryParse(MATCH.StaticId, out match_static_id);
                                    long match_fix_id = 0;
                                    long.TryParse(MATCH.FixId, out match_fix_id);                                                                        
                                    long match_localteam_id = 0;
                                    long.TryParse(MATCH.LocalTeam.Id, out match_localteam_id);
                                    long match_visitorteam_id = 0;
                                    long.TryParse(MATCH.VisitorTeam.Id, out match_visitorteam_id);
                                    string match_localteam_name = MATCH.LocalTeam.Name == null ? "" : MATCH.LocalTeam.Name;
                                    string match_visitorteam_name = MATCH.VisitorTeam.Name == null ? "" : MATCH.VisitorTeam.Name;
                                    string match_ht_score = MATCH.HalfTime.Score == null ? "" : MATCH.HalfTime.Score;
                                    string match_ft_score = MATCH.FinishTime.Score == null ? "" : MATCH.FinishTime.Score;
                                    int match_localTeam_goals = 0;
                                    int.TryParse(MATCH.LocalTeam.Goals, out match_localTeam_goals);
                                    int match_visitorteam_goals = 0;
                                    int.TryParse(MATCH.VisitorTeam.Goals, out match_visitorteam_goals);

                                    // ====== saving local and visitor team into database ======
                                    proc.GetData("Teams_save", match_localteam_id, match_localteam_name);
                                    proc.GetData("Teams_save", match_visitorteam_id, match_visitorteam_name);
                                    // =========================================================
                                                                        
                                    proc.GetData(
                                                    "Matches_save",
                                                    match_id,
                                                    match_status,
                                                    date_time,
                                                    match_stadium,
                                                    match_static_id,
                                                    match_fix_id,
                                                    category_id,
                                                    match_localteam_id,
                                                    match_visitorteam_id,
                                                    match_ht_score,
                                                    match_ft_score,
                                                    match_localTeam_goals,
                                                    match_visitorteam_goals);

                                    if (MATCH.Events.Count > 0)
                                    {
                                        foreach (Event _event in MATCH.Events)
                                        {
                                            if (_event.EventId != null)
                                            {
                                                long event_id = 0;
                                                long.TryParse(_event.EventId, out event_id);
                                                string event_type = _event.Type == null ? "" : _event.Type;
                                                int event_minute = 0;
                                                int.TryParse(_event.Minute, out event_minute);
                                                string event_team = _event.Team == null ? "" : _event.Team;
                                                long event_player_id = 0;
                                                long.TryParse(_event.PlayerId, out event_player_id);
                                                string event_player_name = _event.Player == null ? "" : _event.Player;
                                                long event_assist_id = 0;
                                                long.TryParse(_event.AssistId, out event_assist_id);
                                                string event_assist_name = _event.Assist == null ? "" : _event.Assist;
                                                string event_result = _event.Result == null ? "" : _event.Result;

                                                // ================= saving players into database ===================
                                                proc.GetData("Player_save", event_player_id, event_player_name);
                                                proc.GetData("Player_save", event_assist_id, event_assist_name);
                                                // ==================================================================

                                                // ================== saving events into database ===================
                                                proc.GetData(
                                                                "Events_save",
                                                                event_id,
                                                                event_type,
                                                                event_minute,
                                                                event_team,
                                                                event_player_id,
                                                                event_assist_id,
                                                                event_result,
                                                                match_id);
                                                // ==================================================================
                                            }
                                        }
                                    }

                                    if (MATCH.Odds.Types.Count > 0)
                                    {
                                        foreach (OddType oddType in MATCH.Odds.Types)
                                        {
                                            if (oddType.Id != null)
                                            {
                                                int odd_type_id = 0;
                                                int.TryParse(oddType.Id, out odd_type_id);
                                                string odd_type_name = oddType.Value == null ? "" : oddType.Value;

                                                // ================ saving odd types into database =================
                                                proc.GetData("OddTypes_save", odd_type_id, odd_type_name);
                                                // =================================================================

                                                foreach (Bookmaker bookmaker in oddType.Bookmakers)
                                                {
                                                    long bookmaker_id = 0;
                                                    long.TryParse(bookmaker.Id, out bookmaker_id);
                                                    string bookmaker_name = bookmaker.Name == null ? "" : bookmaker.Name;
                                                    string bookmaker_extra = bookmaker.Extra == null ? "" : bookmaker.Extra;

                                                    // =============== saving bookmarks into database ==============
                                                    proc.GetData("Bookmakers_save", bookmaker_id, bookmaker_name, bookmaker_extra);
                                                    // =============================================================

                                                    long odd_id = 0;
                                                    string odd_name = "";
                                                    float odd_value = 0;
                                                    float odd_total = 0;
                                                    float odd_main = 0;
                                                    float odd_handicap = 0;

                                                    if (bookmaker.Totals.Count > 0)
                                                    {
                                                        foreach (OddTotal _total in bookmaker.Totals)
                                                        {                                                            
                                                            float.TryParse(_total.Name, NumberStyles.Float, CultureInfo.InvariantCulture, out odd_total);
                                                            float.TryParse(_total.Main, NumberStyles.Float, CultureInfo.InvariantCulture, out odd_main);

                                                            foreach (Odd odd in _total.Odds)
                                                            {                                                                
                                                                long.TryParse(odd.Id, out odd_id);
                                                                odd_name = odd.Name == null ? "" : odd.Name;                                                                
                                                                float.TryParse(odd.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out odd_value);

                                                                // ========= saving odds into database ============
                                                                proc.GetData(
                                                                                "Odds_save",
                                                                                odd_id,
                                                                                odd_name,
                                                                                odd_value,
                                                                                bookmaker_id,
                                                                                odd_type_id,
                                                                                odd_handicap,
                                                                                odd_total,
                                                                                odd_main,
                                                                                match_id);
                                                                // ================================================
                                                            }
                                                        }
                                                    }

                                                    if (bookmaker.Handicaps.Count > 0)
                                                    {
                                                        foreach (OddHandicap _handicap in bookmaker.Handicaps)
                                                        {
                                                            float.TryParse(_handicap.Name, NumberStyles.Float, CultureInfo.InvariantCulture, out odd_handicap);
                                                            float.TryParse(_handicap.Main, NumberStyles.Float, CultureInfo.InvariantCulture, out odd_main);

                                                            foreach (Odd odd in _handicap.Odds)
                                                            {
                                                                long.TryParse(odd.Id, out odd_id);
                                                                odd_name = odd.Name == null ? "" : odd.Name;
                                                                float.TryParse(odd.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out odd_value);

                                                                // ========= saving odds into database ============
                                                                proc.GetData(
                                                                                "Odds_save",
                                                                                odd_id,
                                                                                odd.Name,
                                                                                odd.Value,
                                                                                bookmaker_id,
                                                                                odd_type_id,
                                                                                odd_handicap,
                                                                                odd_total,
                                                                                odd_main,
                                                                                match_id);
                                                                // ================================================
                                                            }
                                                        }
                                                    }

                                                    if (bookmaker.Odds.Count > 0)
                                                    {
                                                        foreach (Odd odd in bookmaker.Odds)
                                                        {
                                                            long.TryParse(odd.Id, out odd_id);
                                                            odd_name = odd.Name == null ? "" : odd.Name;
                                                            float.TryParse(odd.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out odd_value);

                                                            // ========= saving odds into database ============
                                                            proc.GetData(
                                                                            "Odds_save",
                                                                            odd_id,
                                                                            odd.Name,
                                                                            odd.Value,
                                                                            bookmaker_id,
                                                                            odd_type_id,
                                                                            odd_handicap,
                                                                            odd_total,
                                                                            odd_main,
                                                                            match_id);
                                                            // ================================================
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    } 
                                }
                            }  
                        }                       
                    }
                    return "soccer odd feed successfully saved!!!";
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
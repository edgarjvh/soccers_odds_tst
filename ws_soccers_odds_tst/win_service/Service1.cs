using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Win32;

namespace win_service
{
    public partial class Service1 : ServiceBase
    {        
        wService.soccersoddsSoapClient ws;
        static string regDir = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\SportFeeds";        
        string strConn = "";
        List<string> errors;
        
        public Service1()
        {
            errors = new List<string>();

            try
            {
                InitializeComponent();
                ws = new wService.soccersoddsSoapClient();
                
            }catch(Exception ex)
            {
                errors.Add(ex.Message);
            }
            
        }
        
        protected override void OnStart(string[] args)
        {
            timer1.Start();
        }

        protected override void OnStop()
        {
            timer1.Stop();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                strConn = Registry.GetValue(regDir, "strConn", "").ToString();

                if (!strConn.Equals(""))
                {
                    using (SqlConnection conn = new SqlConnection(strConn))
                    {
                        conn.Open();

                        string query1 = "select * from countries";
                        SqlCommand cmd1 = new SqlCommand(query1, conn);
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            // loop all countries looking for corresponding urls of odds, tvstations and livescores
                            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                            {
                                string query2 = "select * from feedurls where country_id = @1";
                                SqlCommand cmd2 = new SqlCommand(query2, conn);
                                cmd2.Parameters.AddWithValue("@1", (int)ds1.Tables[0].Rows[i]["country_id"]);
                                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                                DataSet ds2 = new DataSet();
                                da2.Fill(ds2);

                                if (ds2.Tables[0].Rows.Count > 0)
                                {
                                    bool existsOdds = false;

                                    // loop the dataset of feed urls looking for a match with type "odds"
                                    for (int x = 0; x < ds2.Tables[0].Rows.Count; x++)
                                    {
                                        // if there are odds, call the web service to retrieve the data
                                        if (ds2.Tables[0].Rows[x]["feed_type"].ToString().Equals("odds"))
                                        {
                                            ws.SaveSoccerOddsLivescore(ds2.Tables[0].Rows[x]["url"].ToString());
                                            existsOdds = true;
                                            break;
                                        }
                                    }

                                    if (!existsOdds) return;

                                    // loop the dataset of feed urls looking for a match with type "tvStations"
                                    for (int x = 0; x < ds2.Tables[0].Rows.Count; x++)
                                    {
                                        // if there are tvstations, call the web service to retrieve the data
                                        if (ds2.Tables[0].Rows[x]["feed_type"].ToString().Equals("tvStations"))
                                        {
                                            ws.saveSoccerTvStations(ds2.Tables[0].Rows[x]["url"].ToString());
                                            break;
                                        }
                                    }

                                    // loop the dataset of feed urls looking for a match with type "livescore"
                                    for (int x = 0; x < ds2.Tables[0].Rows.Count; x++)
                                    {
                                        // if there are livescores, call the web service to retrieve the data
                                        if (ds2.Tables[0].Rows[x]["feed_type"].ToString().Equals("livescore"))
                                        {
                                            ws.SaveSoccerOddsLivescore(ds2.Tables[0].Rows[x]["url"].ToString());
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }
    }
}

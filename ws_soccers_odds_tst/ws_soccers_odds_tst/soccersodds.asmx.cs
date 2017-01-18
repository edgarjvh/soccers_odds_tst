using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ws_soccers_odds_tst
{
    /// <summary>
    /// Descripción breve de soccersodds
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class soccersodds : System.Web.Services.WebService
    {

        [WebMethod]
        public object ReadData(string xmlUrl)
        {
            return null;
        }



    }
}

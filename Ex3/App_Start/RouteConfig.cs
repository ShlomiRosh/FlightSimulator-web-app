using Ex3.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ex3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
       

            routes.MapRoute("display", "display/{ip}/{port}/{interval}",
               defaults: new { controller = "Simulator", action = "Display", interval = "0" });


            routes.MapRoute("displayLoc", "display/{ip}/{port}/{interval}/GetDataPosition",
              defaults: new { controller = "Simulator", action = "GetDataPosition", interval = "0" });


            routes.MapRoute("save", "save/{ip}/{port}/{interval}/{time}/{name}",
               defaults: new { controller = "Simulator", action = "SaveData" });


            routes.MapRoute("saveFlie", "save/{ip}/{port}/{interval}/{time}/{name}/FileSave",
               defaults: new { controller = "Simulator", action = "FileDataSave" });


            routes.MapRoute("displayFlie", "display/{ip}/{port}/{interval}/ShowFileData",
               defaults: new { controller = "Simulator", action = "ShowFileData", interval = "0" });


            routes.MapRoute(name: "Default", url: "{controller}/{id}",
                defaults: new { controller = "Simulator", action = "Home", id = UrlParameter.Optional });
        }
    }
}

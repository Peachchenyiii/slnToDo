using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace prjToDo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // routes.MapRoute方法定義預設的路由(Routing)
            routes.MapRoute(
                // 設定路由名稱
                name: "Default",
                // 設定網址對應到控制器(Controller)、動作(Action方法)以及路由值(id，即URL參數)規則
                url: "{controller}/{action}/{id}",
                // 設定控制器(Controller))、動作(Action方法)以及路由值(id，即URL參數)的預設值
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages.Razor;

namespace Zhihe.SmartPlatform.Core
{
    public class WebPageEngine : RazorViewEngine
    {

        private string[] GetViewFormats = new[]
        {
              "~/Views/Parts/{0}.cshtml",
              "~/Plugins/{2}/View/{1}/{0}.cshtml",
              "~/Plugins/{2}/View/Shared/{0}.cshtml",
              "~/{2}/Views/{1}/{0}.cshtml",
              "~/{2}/Views/Shared/{0}.cshtml",
              "~/Areas/{2}/Views/{1}/{0}.cshtml",
              "~/Areas/{2}/Views/Shared/{0}.cshtml"
        };



        public WebPageEngine()
        {
            // ViewLocationFormats = GetViewFormats;
            AreaViewLocationFormats = GetViewFormats;
            AreaMasterLocationFormats = GetViewFormats;
            AreaPartialViewLocationFormats = GetViewFormats;
            PartialViewLocationFormats = GetViewFormats;
        }


        private void AddAssembly(ControllerContext controllerContext)
        {
            var nameSpace = controllerContext.RouteData.DataTokens["namespace"];
            if (nameSpace == null) return;
            RazorBuildProvider.CodeGenerationStarted += (object sender, EventArgs e) =>
            {
                var provider = (RazorBuildProvider)sender;
                //PluginDescription pluginDescription;
                //PluginLoader.AllPluginDescription.TryGetValue(nameSpace.ToString(), out pluginDescription);
               // provider.AssemblyBuilder.AddAssemblyReference(pluginDescription.assembly);
            };
        }


        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            AddAssembly(controllerContext);
            return base.FindPartialView(controllerContext, partialViewName, useCache);
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            AddAssembly(controllerContext);

            if (controllerContext.RouteData.DataTokens.Keys.Contains("pluginName"))
            {
                var nameSpace = controllerContext.RouteData.DataTokens["namespace"];
                string pluginName = controllerContext.RouteData.DataTokens["pluginName"].ToString();  //namespace 和 pluginName 这两个值都是在注册路由的时候保存在DataToken中的，在这里用作条件

                string value_controller = controllerContext.RouteData.Values["controller"].ToString();
                string value_actionname = controllerContext.RouteData.Values["action"].ToString();
                if (nameSpace != null && pluginName != null)
                {
                    var viewName1 = string.Format("~/Plugins/{0}/View/{1}/{2}.cshtml", pluginName, value_controller, value_actionname);
                    return base.FindView(controllerContext, viewName1, masterName, useCache);
                }
                else
                {
                    return base.FindView(controllerContext, viewName, masterName, useCache);
                }
            }
            else
            {
                return base.FindView(controllerContext, viewName, masterName, useCache);
            }
        }
    }
}

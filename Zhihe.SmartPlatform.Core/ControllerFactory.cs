using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Zhihe.SmartPlatform.Core
{
    public class ControllerFactory : DefaultControllerFactory
    {
        protected override Type GetControllerType(System.Web.Routing.RequestContext requestContext, string controllerName)
        {

            bool contPluginName = requestContext.RouteData.DataTokens.Keys.Contains("pluginName");
            bool contNamespace = requestContext.RouteData.DataTokens.Keys.Contains("namespace");

            if (contPluginName && contNamespace)
            {
                string dataToken_namespace = requestContext.RouteData.DataTokens["namespace"].ToString();
                string pluginName = requestContext.RouteData.DataTokens["pluginName"].ToString();
                string dateValue_controller = requestContext.RouteData.Values["controller"].ToString();

                string key = dataToken_namespace.Remove(dataToken_namespace.LastIndexOf("."));
                PluginEntity pluginEntity;
                Function function;
                PluginLoader.AllPluginEntity.TryGetValue(key, out pluginEntity);
                pluginEntity.Functions.TryGetValue(dateValue_controller + "Controller", out function);
                return function.ControllerType;
            }
            else 
            {
                return base.GetControllerType(requestContext, controllerName);
            }
        }
    }
}

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

            if (requestContext.RouteData.DataTokens.Count == 0)
            {
                return base.GetControllerType(requestContext, controllerName);
            }

            string dataToken_namespace = requestContext.RouteData.DataTokens["namespace"].ToString();
            string pluginName = requestContext.RouteData.DataTokens["pluginName"].ToString();
            string dateValue_controller = requestContext.RouteData.Values["controller"].ToString();


            if (dataToken_namespace == null && pluginName==null)
            {
                return base.GetControllerType(requestContext, controllerName);
            }
            else
            {
                string key = dataToken_namespace.Remove(dataToken_namespace.LastIndexOf("."));
                PluginEntity pluginEntity;
                Function function;
                PluginLoader.AllPluginEntity.TryGetValue(key, out pluginEntity);
                pluginEntity.Functions.TryGetValue(dateValue_controller+"Controller", out function);
                return function.ControllerType;
            }
        }

    }
}

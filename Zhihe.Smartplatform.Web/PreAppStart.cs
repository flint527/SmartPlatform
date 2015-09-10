using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Zhihe.SmartPlatform.Core;



[assembly: System.Web.PreApplicationStartMethod(typeof(Zhihe.Smartplatform.Web.PreAppStart), "PreApplicationStart")]
namespace Zhihe.Smartplatform.Web
{
    public class PreAppStart
    {
        public static void PreApplicationStart()
        {
            AppDomain.CurrentDomain.SetupInformation.ShadowCopyFiles = "true";
            AppDomain.CurrentDomain.SetShadowCopyPath(HostingEnvironment.MapPath("~/App_Data/Dependence"));
            ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());
            PluginLoader.LoadPlugin();
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new WebPageEngine());
        }
    }
}
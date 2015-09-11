using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Zhihe.Smartplatform.Web.Service;

namespace Zhihe.Smartplatform.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private HomeService homeService;

        public HomeController() {
            homeService = new HomeService();
        }


        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 更加菜单中传来的OnlyCode跳转到菜单对应的插件路由中
        /// </summary>
        /// <param name="onlyCode"></param>
        /// <returns></returns>
        public ActionResult GetFunction(string  id)  
        {
            string controllerName = string.Empty;   // XXXController
            string actionName = string.Empty;
            bool result = homeService.ResolveOnlyCode(id, out controllerName, out actionName);
            if (!result)
            {
                return View("NotExit");  // OnlyCode 在数据库菜单信息中不存在
            }
            else 
            {
                string ctrName = controllerName.Remove(controllerName.Length - "Controller".Length);
                return RedirectToRoute(controllerName, new { controller = ctrName, action = actionName });  // 跳转到对应的插件路由中去
            }
        }

    }
}

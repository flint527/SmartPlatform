using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zhihe.Smartplatform.Web.Service
{
    public class HomeService
    {
        /// <summary>
        /// 检查onlyCode在数据库中的菜单信息中是否存在 如果存在就通过onlyCode 解析出controller 和 Action 名字  
        /// </summary>
        /// <param name="OnlyCode"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public bool ResolveOnlyCode(string OnlyCode, out string controllerName, out string actionName)
        {
            controllerName = "AddUserController";
            actionName = "Index";
            return true;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhihe.Smartplatform.SysTools;

namespace Zhihe.SmartPlatform.Core
{
    public static class PlunginManager
    {
        //BusinessDataAccess businessDataAccess = new BusinessDataAccess();
        /// <summary>
        /// 初始化插件信息到数据库
        /// </summary>
        /// <param name="pluginDescription"></param>
        /// <returns></returns>
        public static void InitPluginFunctionToDB(Dictionary<string, PluginEntity> pluginEntitys)
        {

           // DB_AddDataHelper.AddDataBySql();

           
        }

        public static void DeletePluginFunction(string onlyCode)
        {
            Dictionary<String, String> Parameters = new Dictionary<string, string>();
            Parameters.Add("OnlyCode", onlyCode);
           // BaseDB.DeleteData<PluginFunction>(Parameters);
        }

    }
}

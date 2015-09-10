using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static void InitPluginFunctionToDB(PluginDescription pluginDescription)
        {
            //foreach (PluginFunction pluginFunction in pluginDescription.plugin.pluginFunctions)
            //{
            //    IList<SqlParameter> parameterList = BaseDBSupport.GetParameters<PluginFunction>(pluginFunction);
            //    foreach (var parameter in parameterList)
            //    {
            //        if (parameter.ParameterName == "OnlyCode")
            //        {
            //            //string controller = pluginFunction.Controller.Substring(0, pluginFunction.Controller.Length - "Controller".Length);
            //            var str = pluginFunction.FunctionName + "#" + pluginFunction.Controller + "#" + pluginFunction.Action;
            //            var onlyCode = Security.EncryptDES(str, "88888888");
            //            parameter.Value = onlyCode;
            //            DeletePluginFunction(onlyCode);// 删除已经存在的相同的插件
            //        }
            //    }
            //    BaseDB.AddData<PluginFunction>(pluginFunction, parameterList);
            //}
        }

        public static void DeletePluginFunction(string onlyCode)
        {
            Dictionary<String, String> Parameters = new Dictionary<string, string>();
            Parameters.Add("OnlyCode", onlyCode);
           // BaseDB.DeleteData<PluginFunction>(Parameters);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zhihe.SmartPlatform.Core
{
    public  class PluginEntity
    {

        public  string Name { set; get; }

        public  string Author { set; get; }

        public  string Description { set; get; }

        public  Dictionary<string, Function> Functions { set; get; }

        public  Assembly Assembly { set; get; }

        /// <summary>
        /// 根据命名空间的名称和控制器的名称来找到控制器的类型
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        //public  Type GetTypeByNamespaceAndControllerName(string nameSpace, string controllerName)
        //{
        //    Function function;
        //    //PluginEntity.Functions.TryGetValue(nameSpace, out function);
        //    return function.ControllerType;
        //}
    }
}

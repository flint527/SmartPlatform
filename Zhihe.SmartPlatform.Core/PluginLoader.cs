using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;

namespace Zhihe.SmartPlatform.Core
{
    public static class PluginLoader
    {
        // private static AppDomain _appdomain;

        public static Dictionary<string, PluginEntity> AllPluginEntity = new Dictionary<string, PluginEntity>(); // 所有插件的描述信息

        private static readonly string DllFilesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
        private static readonly string pluginsFilesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
        private static readonly string pluginsFilesTempPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data\\Dependence");

        /// <summary>
        /// Plugin 文件夹中所有dll文件的路径
        /// </summary>
        private static List<string> DllAssembly = new List<string>();

        /// <summary>
        /// 加载项目下的Plugins文件夹中的所有的插件信息
        /// </summary>
        static PluginLoader()
        {
            DirectoryInfo DllFilesdirectory = new DirectoryInfo(DllFilesPath);
            var DllfileInfos = DllFilesdirectory.GetFiles("*.dll", SearchOption.AllDirectories);
            foreach (var DllfileInfo in DllfileInfos)
            {
                DllAssembly.Add(DllfileInfo.Name);
            }
        }

        /// <summary>
        /// 加载插件
        /// </summary>
        public static void LoadPlugin()
        {
            List<Assembly> pluginAssemlyList = LoadPluginsFiles();  // 拷贝dll文件
            AllPluginEntity = GetPluginEntitys(pluginAssemlyList);
            InitPlugins(AllPluginEntity);
        }


        /// <summary>
        /// 拷贝加载插件相关的信息并加载插件程序集
        /// </summary>
        /// <returns></returns>
        public static List<Assembly> LoadPluginsFiles()
        {
            DirectoryInfo pluginsFilesdirectory = new DirectoryInfo(pluginsFilesPath);
            var fileInfos = pluginsFilesdirectory.GetFiles("*.dll", SearchOption.AllDirectories); // 获得所有的dll 文件
            List<Assembly> assemblyList = new List<Assembly>();
            foreach (var fileInfo in fileInfos)  // 遍历Plugin 文件夹下所有的dll文件
            {
                var fullName = fileInfo.FullName;
                var index = fullName.LastIndexOf("\\bin\\");
                if (index <= -1) continue;
                if (DllAssembly.Contains(fullName.Substring(index + 5))) continue; // 防止重复
                string tagFilePath = pluginsFilesTempPath + "\\" + fullName.Substring(index + 5);
                string strPath = Path.GetDirectoryName(tagFilePath);
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                fileInfo.CopyTo(tagFilePath, true);
                assemblyList.Add(Assembly.LoadFrom(tagFilePath));  // 加载插件程序集
            }
            return assemblyList;
        }

        /// <summary>
        /// 获取一个插件描述信息
        /// </summary>
        /// <param name="pluginAssemblys"></param>
        /// <returns></returns>
        public static Dictionary<string, PluginEntity> GetPluginEntitys(IEnumerable<Assembly> pluginAssemblys)
        {
            Dictionary<string, PluginEntity> PluginEntitys = new Dictionary<string, PluginEntity>();
            foreach (var pluginAssembly in pluginAssemblys) //循环每个插件dll文件
            {
                // 找出每个dll 文件中的所有的 controller 类型
                PluginDescription pluginDescription = new PluginDescription();
                Type[] allTypes = pluginAssembly.GetTypes();
                List<Type> controllerTypes = allTypes.Where(x => x.Name.Contains("Controller") && x.GetInterface("IController") != null).ToList();

                List<Function> functions;
                PluginEntity pluginEntity = ReadPluginXml(pluginAssembly, out functions);

                if (pluginEntity == null && functions == null) continue;

                foreach (var function in functions)
	            {
		            List<Type>  type = controllerTypes.Where( x=>x.Name==function.Name ).ToList();
                    if (type.Count==1)
	                {
		                pluginEntity.Functions[function.Name].ControllerType = type[0];
	                }
                    else //  配值文件中写的和世界的匹配不上 检查配置文件的属性是否符合要求
                    {
                        continue;
                    }
                    pluginEntity.Assembly = pluginAssembly;
	            }
                string key = pluginAssembly.FullName.Remove(pluginAssembly.FullName.IndexOf(","));
                PluginEntitys.Add(key, pluginEntity);
            }
            return PluginEntitys;
        }


        /// <summary>
        /// 获得插件中的ControllerTypes信息
        /// </summary>
        /// <param name="pluginAssembly"></param>
        ///// <returns></returns>
        //private static Dictionary<string, Type> GetControllerTypesFromAssembly(Assembly pluginAssembly)
        //{
        //    Dictionary<string, Type> controllerTypes = new Dictionary<string, Type>();
        //    var allTypes = pluginAssembly.GetTypes();
        //    foreach (var oneType in allTypes)
        //    {
        //        if (oneType.Name != null && oneType.Name.Contains("Controller") && oneType.GetInterface("IController") != null)
        //        {
        //            controllerTypes.Add(oneType.Name, oneType);
        //        }
        //    }
        //    return controllerTypes;
        //}

        /// <summary>
        /// 初始化插件信息
        /// </summary>
        /// <param name="pluginDescriptionList"></param>
        public static void InitPlugins(Dictionary<string, PluginEntity> pluginEntitys)
        {
            foreach (var pluginEntity in pluginEntitys)
            {
                //初始化插件信息到数据库
                //PlunginManager.InitPluginFunctionToDB(pluginDescription.Value);

                //初始化路由信息
                //pluginEntitys.Values.ToList()

                InitRoutes(pluginEntitys.Values.ToList());
            }
        }

        /// <summary>
        /// 初始化路由
        /// </summary>
        /// <param name="routeEntity"></param>
        //public static void InitRoutes(List<PluginEntity> pluginEntitys)
        //{
        //    foreach (PluginEntity pluginEntity in pluginEntitys)
        //    {
        //        pluginEntity.Functions.Values.ToList().ForEach(x =>{


        //            var route = RouteTable.Routes.MapRoute(
        //                name: x.Name,
        //                url: string.IsNullOrEmpty(x.Url) ? "{controller}/{action}/{id}" : x.Url,
        //                defaults: new
        //                {
        //                    controller = x.Controller,
        //                    action = x.Action,
        //                    id = UrlParameter.Optional,
        //                    PluginName = x.NameSpace
        //                }
        //              );
        //            route.DataTokens["namespace"] = x.NameSpace;
        //            route.DataTokens["pluginName"] = pluginEntity.Name;
        //        });
        //      // RouteTable.Routes.Reverse();
        //    }
        //}



        public static void InitRoutes(List<PluginEntity> pluginEntitys)
        {
            foreach (PluginEntity pluginEntity in pluginEntitys)
            {

                foreach (Function x in pluginEntity.Functions.Values)
                {
                    //RouteTable.Routes


                    var route = RouteTable.Routes.MapRoute(
                       name: x.Name,
                       url: string.IsNullOrEmpty(x.Url) ? "{controller}/{action}/{id}" : x.Url,
                       defaults: new
                       {
                           controller = x.Controller,
                           action = x.Action,
                           id = UrlParameter.Optional,
                           PluginName = x.NameSpace
                       }
                     );
                    route.DataTokens["namespace"] = x.NameSpace;
                    route.DataTokens["pluginName"] = pluginEntity.Name;
                }

               
                RouteTable.Routes.Reverse();
            }
        }




        public static PluginEntity ReadPluginXml(Assembly pluginAssembly,out List<Function> Functions) 
        {
            // 找到文件的路径名称
            string assFullName = pluginAssembly.FullName;
            string areaName = assFullName.Substring(0, assFullName.IndexOf(","));
            string namexml = areaName.Substring(areaName.LastIndexOf('.') + 1) + "Plugin.xml";
            string filePath = Path.Combine(pluginsFilesPath, areaName.Substring(areaName.LastIndexOf('.') + 1), namexml);

            if (!File.Exists(filePath)) 
            {
                Functions = null;
                return null; 
            }

            Functions = new List<Function>();
            PluginEntity pluginEntity = new PluginEntity();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlNode root = xmlDoc.SelectSingleNode("Plugin");
            XmlNodeList nodeList = root.ChildNodes;
            foreach (XmlNode xn in nodeList)
            {
                if ("Name".Equals(xn.Name))
                {
                    pluginEntity.Name = xn.InnerText;
                }
                if ("Author".Equals(xn.Name))
                {
                    pluginEntity.Author = xn.InnerText;
                }
                if ("Description".Equals(xn.Name))
                {
                    pluginEntity.Description = xn.InnerText;
                }
                if ("Functions".Equals(xn.Name))
                {
                    XmlElement xe = xn as XmlElement;
                    XmlNodeList funNode = xe.ChildNodes;
                    Dictionary<string, Function> functions = new Dictionary<string, Function>();
                    foreach (XmlNode funcs in funNode)
                    {
                         string tempUrl = string.Empty;
                         if ("Url".Equals(funcs.Name))
                         {
                            tempUrl = funcs.InnerText;
                         }

                         if ("Function".Equals(funcs.Name))
                         {
                            Function function = new Function();
                            XmlElement functs = funcs as XmlElement;
                            XmlNodeList functNode = functs.ChildNodes;
                            foreach (XmlNode func in functNode)
                            {
                                if ("Name".Equals(func.Name))
                                {
                                    function.Name = func.InnerText;
                                }
                                if ("NameSpace".Equals(func.Name))
                                {
                                    function.NameSpace = func.InnerText;
                                }
                                if ("Controller".Equals(func.Name))
                                {
                                    function.Controller = func.InnerText;
                                }
                                if ("Action".Equals(func.Name))
                                {
                                    function.Action = func.InnerText;
                                }
                                function.Url = tempUrl;
                            }
                            if (function.Name == null) continue;
                            Functions.Add(function);
                            functions.Add(function.Name, function);  
                            function.Assembly = pluginAssembly;
                         }
                    }
                    pluginEntity.Functions = functions;
                }
            }
            return pluginEntity;
        
        }

    }
}

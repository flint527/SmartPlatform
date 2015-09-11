using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zhihe.SmartPlatform.Core
{
    public abstract class BasePlugin
    {
        public abstract string Name { get; }//名称必须和项目名称的最后一个点号后面的单词相同 且不能重复

        public abstract string Author { get; }

        public abstract string Description { get; }

        public abstract string Url { get; }//内容为空的时候区默认值：Controller/{action}/{id}

        public abstract List<Function> Functions { get; }

        public PluginEntity GetPluginEntity(Assembly Assembly,string Url, out List<Function> Functions) 
        {
            this.Functions.ForEach(x => { x.Url = Url; x.Assembly = Assembly; });
            Functions = this.Functions;
            return  new PluginEntity(
                        Name:this.Name,
                        Author:this.Author,
                        Description:this.Description,
                        Functions: this.Functions.ToDictionary(x => x.Name), // 通过Function对象的Name属性作为key
                        Assembly:Assembly
                       );
        }
    }
}

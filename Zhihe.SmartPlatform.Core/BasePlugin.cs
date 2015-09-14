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

        public abstract string Url { get; }//内容为空的时候取默认值：Controller/{action}/{id}

        public abstract List<Function> Functions { get; }


        public PluginEntity GetPluginEntity(Assembly _Assembly, string _Url, out List<Function> _Functions)
        {
            //this.Functions.ForEach(x => { x.Url = _Url; x.Assembly = _Assembly; });  // x 是临时的~~ 修改不了

            //for (int i = 0; i < this.Functions.Count; i++)   // 这样写赋值不上
            //{

            //    //Functions[i].Url = _Url;
            //    this.Functions[i].Url = "asdfasdf";
            //    this.Functions[i].Assembly = _Assembly;
            //}

            _Functions = Functions.Select(x => new Function()
                {
                    Action = x.Action,
                    Assembly = _Assembly,
                    Controller = x.Controller,
                    NameSpace = x.NameSpace,
                    ControllerType = x.ControllerType,
                    Name = x.Name,
                    Url = _Url
                }).ToList();

            return new PluginEntity(
                        Name: this.Name,
                        Author: this.Author,
                        Description: this.Description,
                        Functions: _Functions.ToDictionary(x => x.Name), // 通过Function对象的Name属性作为key
                        Assembly: _Assembly
                       );
        }
    }
}

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
        public PluginEntity()
        {
        }

        public PluginEntity(string Name, string Author, string Description, Dictionary<string, Function> Functions, Assembly Assembly)
        {
            this.Name = Name;
            this.Author = Author;
            this.Description = Description;
            this.Functions = Functions;
            this.Assembly = Assembly;
        }
        public  string Name { set; get; }

        public  string Author { set; get; }

        public  string Description { set; get; }

        public  Dictionary<string, Function> Functions { set; get; }

        public  Assembly Assembly { set; get; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zhihe.SmartPlatform.Core
{
    public class Function
    {
        public Function()
        {    
        }

        public Function(string Name, string NameSpace, string Controller, string Action) 
        {
            this.Name = Name;
            this.NameSpace = NameSpace;
            this.Controller = Controller;
            this.Action = Action;
        }
        public string Name { set; get; }

        public string NameSpace{set;get;}

        public string Controller {set;get;}

        public string Action {set;get;}

        public Assembly Assembly { set; get; }

        public Type ControllerType { set;get;}

        public string Url { set; get; }

    }
}

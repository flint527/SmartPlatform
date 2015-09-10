using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zhihe.SmartPlatform.Core
{
    public class PluginDescription
    {

        public Dictionary<string, Type> allPluginDescription = new Dictionary<string, Type>();

      //  public BasePlugin plugin { set; get; }

        public Assembly assembly { set; get; }

        public List<Type> type { set; get; }

        public Dictionary<string, Type> controllerTypes { set; get; }
    }
}

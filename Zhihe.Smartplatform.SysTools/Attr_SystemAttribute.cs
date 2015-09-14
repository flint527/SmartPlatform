using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhihe.Smartplatform.SysTools
{
    public class Attr_SystemAttribute
    {

        [AttributeUsage(AttributeTargets.Class)]
        public class DBTableNameAttribute : System.Attribute
        {
            public string DBFormName { get; set; }
        }


        [AttributeUsage(AttributeTargets.Property)]
        public class IsPrimaryKeyAttribute : System.Attribute
        {
            public bool isPrimaryKey { get; set; }
        }


    }
}

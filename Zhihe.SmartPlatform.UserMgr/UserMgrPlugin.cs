﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zhihe.SmartPlatform.Core;

namespace Zhihe.SmartPlatform.UserMgr
{
    public class UserMgrPlugin : BasePlugin
    {
        public override string Name { get { return "UserMgr"; } } //名称必须和项目名称的最后一个点号后面的单词相同 且不能重复

        public override string Author { get { return "fanjinlong"; } }

        public override string Description { get { return "用户管理功能模块"; } }

        public override string Url { get { return ""; } } //内容为空的时候区默认值：Controller/{action}/{id}


        public override List<Function> Functions
        {
            get
            {
                return new List<Function>{
                          new Function(
                              Name:"AddUserController",  //必须与Controllers 文件夹中的Controller类名字相同
                              NameSpace:"Zhihe.SmartPlatform.UserMgr.Controllers",
                              Controller:"AddUser",
                              Action:"Index"
                          ),
                          new Function(
                              Name:"DeleteController",
                              NameSpace:"Zhihe.SmartPlatform.UserMgr.Controllers",
                              Controller:"Delete",
                              Action:"Index"
                          )
                      };
            }
        }
    }

}


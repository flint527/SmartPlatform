using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zhihe.Smartplatform.SysTools
{
    public class DB_AddDataHelper
    {


        public bool AddDataByObject<T>(T t)
        {
            string properties = GetPropertiesStr<T>();
            string propertiesValueStr = GetPropertiesStr<T>();
            string tableName = GetDbTableNameByObj<T>();
            string insertSql = "insert into dbo.[" + tableName + "] (" + properties + ")values(" + propertiesValueStr + ")";
            IList<SqlParameter> parameters = GetParameters<T>(t);
            var insertRetult = DB_SqlHelper.ExecSql(insertSql, parameters);
            return insertRetult == 1 ? true : false;
        }


        #region

        //----------------------------------------common part---------------------------------------------------------------

        /// <summary>
        ///  获取一个实体的所有字段 并且组装成 "aaa,bbb,ccc" 这样的字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public string GetPropertiesStr<T>()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < properties.Length; i++)
            {
                Attr_SystemAttribute.IsPrimaryKeyAttribute att = (Attr_SystemAttribute.IsPrimaryKeyAttribute)Attribute.GetCustomAttribute(properties[i], typeof(Attr_SystemAttribute.IsPrimaryKeyAttribute));
                if (att == null)
                {
                    sb.Append(properties[i].Name + ",");
                }
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        /// <summary>
        /// 获取对象在数据库中对应的表名称 或者类名称
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <returns></returns>
        public static String GetDbTableNameByObj<T>()
        {
            string tableName = typeof(T).Name;
            Attr_SystemAttribute.DBTableNameAttribute att = (Attr_SystemAttribute.DBTableNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(Attr_SystemAttribute.DBTableNameAttribute));
            if (att != null)
            {
                tableName = att.DBFormName;
            }
            return tableName;
        }

        /// <summary>
        /// 获取一个对象中的数据 并且将其转换成为SqlParameter数组返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static SqlParameter[] GetParameters<T>(T t)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            IList<SqlParameter> parameterList = new List<SqlParameter>();
            foreach (var propertyInfo in properties)
            {
               // var propertyValue = GetPropertyValue<T>(t, propertyInfo.Name);
                Type type = t.GetType(); //获取类型
                PropertyInfo Info = type.GetProperty(propertyInfo.Name);//获取指定名称的属性
                var propertyValue = Info.GetValue(t, null); //获取属性值;

                var propertyType = properties[1].PropertyType.Name;
                if (propertyInfo.Name != "Id")
                {
                    switch (propertyType)
                    {
                        case "Int32":
                            parameterList.Add(new SqlParameter(propertyInfo.Name, propertyInfo.GetValue(t, null)));
                            break;
                        case "String":
                            parameterList.Add(new SqlParameter(propertyInfo.Name, propertyInfo.GetValue(t, null)));
                            break;
                        default:
                            parameterList.Add(new SqlParameter(propertyInfo.Name, null));
                            break;
                    }
                }
            }
            return parameterList.ToArray<SqlParameter>();
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;
using System.Data;

namespace Zhihe.Smartplatform.SysTools
{
    public static class DB_SqlHelper
    {

        public static string connStr = string.Empty;


        // System.Configuration.ConfigurationManager.ConnectionStrings["name"];

        public static void ConnectionStr()
        {
            connStr = ConfigurationManager.ConnectionStrings["PlantformDBConnStr"].ToString();
        }

        /// <summary>
        /// 执行增删改语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecSql(string sql, IList<SqlParameter> sqlParameter)
        {
            ConnectionStr();
            SqlParameter[] parameters = sqlParameter.ToArray<SqlParameter>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 查询第一行第一列数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecQueryInObject(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// 执行查询操作
        /// </summary>
        public static List<T> ExecQueryInList<T>(string query, params SqlParameter[] parameters)
        {
            var list = new List<T>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                var reader = cmd.ExecuteReader();

                var table = reader.GetSchemaTable();
                PropertyInfo[] properties = typeof(T).GetProperties();
                var dic = new Dictionary<string, PropertyInfo>();
                foreach (DataRow row in table.Rows)
                {
                    var columnname = (string)row[0];
                    foreach (var propertyInfo in properties)
                    {
                        if (propertyInfo.Name == columnname)
                        {
                            dic.Add(columnname, propertyInfo);
                            break;
                        }
                    }
                }
                while (reader.Read())
                {
                    // T player = new T();
                    T player = System.Activator.CreateInstance<T>();
                    foreach (var keyval in dic)
                    {
                        PropertyInfo info = keyval.Value;
                        info.SetValue(player, reader[keyval.Key], null);
                    }
                    list.Add(player);
                }
                cmd.Parameters.Clear();
            }
            return list;
        }

        /// <summary>
        /// 执行查询的方法，将T-SQL命令语句发送到执行Command中去，得到一个记录对象
        /// </summary>
        public static SqlDataReader GetDataReader(string cmdText)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            return cmd.ExecuteReader();
        } 

    }
}

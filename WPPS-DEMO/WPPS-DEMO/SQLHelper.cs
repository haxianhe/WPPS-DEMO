/**
 * name:haxianhe
 * time:2018-5-13 21:38
 * description:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WPPS_DEMO
{
    /// <summary>
    /// 数据库助手类
    /// </summary>
    public class SQLHelper
    {
        #region 字段
        /// <summary>
        /// 表示一个到SQL Server数据库的打开连接
        /// </summary>
        private SqlConnection conn = null;
        /// <summary>
        /// 表示要对SQL Server数据库执行一个Transcat-SQL语句或存储过程
        /// </summary>
        private SqlCommand cmd = null;
        /// <summary>
        /// 提供一个从SQL Server数据库读取行的只进流方式
        /// </summary>
        private SqlDataReader sdr = null;
        #endregion

        #region 构造函数
        public SQLHelper()
        {
            string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            conn = new SqlConnection(connStr);
        }
        #endregion

        #region 数据库连接函数
        /// <summary>
        /// 数据库连接函数
        /// </summary>
        /// <returns></returns>
        private SqlConnection GetConn()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }
        #endregion

        #region 执行带参数的SQL增删改语句
        /// <summary>
        /// 执行带参数的SQL增删改语句
        /// </summary>
        /// <param name="sql">增删改SQL语句</param>
        /// <param name="paras">参数集合</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, SqlParameter[] paras, CommandType commandType)
        {
            int res;
            using (cmd = new SqlCommand(sql, GetConn()))
            {
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(paras);
                res = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            return res;
        }
        #endregion

        #region 执行带参数的SQL查询
        /// <summary>
        /// 执行带参数的SQL查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="paras">参数集合</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public DataTable ExecuteQuery(string sql, SqlParameter[] paras, CommandType commandType)
        {
            DataTable dt = new DataTable();
            using (cmd = new SqlCommand(sql, GetConn()))
            {
                //cmd.CommandTimeout = 200;
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(paras);
                sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(sdr);
                cmd.Parameters.Clear();
            }
            return dt;
        }
        #endregion
    }
}

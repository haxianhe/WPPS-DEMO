/**
 * 创建人：haxianhe
 * 创建时间：2018/6/7 21:43:02
 * 说明：<FUNCTION>
 **/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPPS_DEMO
{
    class QueryPower
    {
        private SQLHelper sqlHelper;

        public QueryPower()
        {
            sqlHelper = new SQLHelper();
        }
        /// <summary>
        /// 实时功率查询函数
        /// </summary>
        /// <param name="time">待查询时间</param>
        /// <returns></returns>
        public string QueryRealTimePower(string time)
        {
            string power = null;
            string sqlStr = @"select Power from TB_RealTime where RecTime=@time";
            SqlParameter[] paras = new SqlParameter[] {
                 new SqlParameter("@time",time)
            };
            DataTable dt = sqlHelper.ExecuteQuery(sqlStr, paras, CommandType.Text);
            if (dt.Rows.Count > 0)//如果查询结果大于0，证明有数据
            {
                power = Convert.ToString(dt.Rows[0]["Power"]);
            }
            return power;
        }
        /// <summary>
        /// 超短期功率查询函数
        /// </summary>
        /// <param name="time">待查询时间</param>
        /// <returns></returns>
        public string QueryShortTimePower(string time)
        {
            string power = null;
            string sqlStr = @"select Power from TB_Prediction_ShortTerm where RecTime=@time";
            SqlParameter[] paras = new SqlParameter[] {
                 new SqlParameter("@time",time)
            };
            DataTable dt = sqlHelper.ExecuteQuery(sqlStr, paras, CommandType.Text);
            if (dt.Rows.Count > 0)//如果查询结果大于0，证明有数据
            {
                power = Convert.ToString(dt.Rows[0]["Power"]);
            }
            return power;
        }
        /// <summary>
        /// 短期功率查询函数
        /// </summary>
        /// <param name="time">待查询时间</param>
        /// <returns></returns>
        public string QueryPeriodTimePower(string time)
        {
            string power = null;
            string sqlStr = @"select Power from TB_Prediction_PeriodTime where RecTime=@time";
            SqlParameter[] paras = new SqlParameter[] {
                 new SqlParameter("@time",time)
            };
            DataTable dt = sqlHelper.ExecuteQuery(sqlStr, paras, CommandType.Text);
            if (dt.Rows.Count > 0)//如果查询结果大于0，证明有数据
            {
                power = Convert.ToString(dt.Rows[0]["Power"]);
            }
            return power;
        }
    }
}

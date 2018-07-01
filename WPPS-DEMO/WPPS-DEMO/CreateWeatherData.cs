/**
 * 创建人：haxianhe
 * 创建时间：2018/6/6 20:06:33
 * 说明：模拟气象数据
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
    class CreateWeatherData
    {
        private SQLHelper sqlHelper;                                        //数据库帮助类句柄
        private System.Threading.Thread threadCreateWeatherData = null;     //气象数据生产线程
        private Boolean isStop = false;                                     //线程关闭标识
        private static CreateWeatherData create = new CreateWeatherData();  //单例对象

        /// <summary>
        /// 获取单例对象
        /// </summary>
        /// <returns></returns>
        public static CreateWeatherData getCreateWeatherData()
        {
            return create;
        }
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CreateWeatherData()
        {
            sqlHelper = new SQLHelper();
        }

        /// <summary>
        /// 启动气象数据模拟线程
        /// </summary>
        public void Start()
        {
            isStop = false;
            threadCreateWeatherData = new System.Threading.Thread(MainThread);
            threadCreateWeatherData.Start();
        }

        /// <summary>
        /// 停止气象数据模拟线程
        /// </summary>
        public void Stop()
        {
            if (threadCreateWeatherData != null)
            {
                isStop = true;
                System.Threading.Thread.Sleep(100);
                threadCreateWeatherData.Abort();
            }
        }

        public Action<string> refreshtxt;

        /// <summary>
        /// 气象数据模拟线程主函数
        /// </summary>
        private void MainThread()
        {
            while (!isStop)
            {
                DateTime now = DateTime.Now;
                DateTime _now = Convert.ToDateTime(now.Year + "-" + now.Month + "-" + now.Day + " T" + now.Hour + ":00" + ":00");
                for (int i = 0; i < 10800; i++)
                {
                    Random ran = new Random();
                    int windSpeed = ran.Next(0, 10);
                    string time = _now.ToString();
                    _now = _now.AddSeconds(1);
                    if (refreshtxt != null)
                    {
                        Insert(time, windSpeed);
                    }
                }
                string log = now.AddHours(-1).Year + ":" + now.AddHours(-1).Month + ":" +
                    now.AddHours(-1).Day + " " + now.AddHours(-1).Hour + "——" + now.Year + ":" + now.Month + ":" +
                    now.Day + " " + now.Hour + " ：气象数据模拟成功";
                refreshtxt(log);
                while (DateTime.Now.Minute != 0)
                {
                    System.Threading.Thread.Sleep(30000);
                }
            }

        }

        /// <summary>
        /// 将模拟数据插入数据库
        /// </summary>
        /// <param name="time"></param>
        /// <param name="windSpeed"></param>
        private void Insert(string time, int windSpeed)
        {
            string sql = @"insert into TB_DataSource_Weather(RecTime,WindSpeed) values(@time,@windSpeed)";
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@time",time),
                new SqlParameter("@windSpeed",windSpeed)
                 };
            int res = sqlHelper.ExecuteNonQuery(sql, paras, CommandType.Text);
        }
    }
}

/**
 * 创建人：haxianhe
 * 创建时间：2018/6/6 22:00:15
 * 说明：气象数据采集类
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
    class ScadaWeatherData
    {
        private SQLHelper sqlHelper;                                        //数据库帮助类句柄
        private System.Threading.Thread threadScadaWeatherData = null;     //实时数据采集线程
        private Boolean isStop = false;                                     //线程关闭标识
        public Action<string> refreshtxt;
        public Action<string> errorLog;
        private static ScadaWeatherData scada = new ScadaWeatherData();   //实例对象

        public static ScadaWeatherData getScadaRealTimeData()//获取单例对象
        {
            return scada;
        }

        public ScadaWeatherData()// 默认构造函数
        {
            sqlHelper = new SQLHelper();
        }

        public void Start()// 启动实时数据模拟线程
        {
            isStop = false;
            if (ControlModel.getControlModel().ScadaWeatherData == 0)
                threadScadaWeatherData = new System.Threading.Thread(MainThread);
            else
                threadScadaWeatherData = new System.Threading.Thread(MainThreadOpt);
            threadScadaWeatherData.Start();
        }

        public void Stop()// 停止实时数据模拟线程
        {
            if (threadScadaWeatherData != null)
            {
                isStop = true;
                System.Threading.Thread.Sleep(100);
                threadScadaWeatherData.Abort();
            }

        }

        private void MainThread()// 气象数据采集线程主函数
        {
            while (!isStop)
            {
                DateTime now = DateTime.Now;
                DateTime _now = Convert.ToDateTime(now.Year + "-" + now.Month + "-" + now.Day + " " + now.Hour + ":00" + ":00");
                int windSpeed = QueryWindSpeed(_now.ToString());
                if (windSpeed >= 0)
                {
                    for (int i = 0; i < 10800; i++)
                    {
                        if (refreshtxt != null)
                        {
                            refreshtxt(Insert(_now.ToString(), windSpeed));
                        }
                        _now = _now.AddSeconds(1);
                        System.Threading.Thread.Sleep(100);
                    }
                    System.Threading.Thread.Sleep(10800000);
                }
                else
                {
                    if (refreshtxt != null)
                    {
                        refreshtxt("采集失败 ： " + _now.ToString());
                    }
                }
                System.Threading.Thread.Sleep(10);
            }
        }

        private void MainThreadOpt()// 优化版主函数
        {
            int error = 0;
            int flag = 0;
            while (!isStop)
            {
                DateTime now = DateTime.Now;
                DateTime _now = Convert.ToDateTime(now.Year + "-" + now.Month + "-" + now.Day + " T" + now.Hour + ":00" + ":00");
                int windSpeed = QueryWindSpeed(_now.ToString());
                if (windSpeed >= 0)
                {
                    for (int i = 0; i < 10800; i++)
                    {
                        if (refreshtxt != null)
                        {
                            refreshtxt(Insert(_now.ToString(), windSpeed));
                        }
                        _now = _now.AddSeconds(1);
                        System.Threading.Thread.Sleep(100);
                    }
                    System.Threading.Thread.Sleep(10800000);
                    error = 0;
                }
                else
                {
                    if (refreshtxt != null)
                    {
                        refreshtxt("采集失败 ： " + _now.ToString());
                    }
                    if (errorLog != null && error == 0)
                    {
                        errorLog("气象数据采集失败 : " + now.ToString());
                        error = 1;
                    }
                    flag++;
                }
                if (flag < 5 && flag != 0)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                else if (flag > 5)
                {
                    System.Threading.Thread.Sleep(5000);
                }
            }
        }

        private int QueryWindSpeed(string time)// 从气象数据模拟表中查出模拟数据
        {
            int windSpeed = -1;
            string sql = @"select * from TB_DataSource_Weather where RecTime=@time";
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@time",time)
            };
            DataTable dt = sqlHelper.ExecuteQuery(sql, paras, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                windSpeed = Convert.ToInt32(dt.Rows[0]["WindSpeed"]);
            }
            return windSpeed;
        }

        private string Insert(string time, int windSpeed)//将模拟数据插入数据库
        {
            string log = "";
            string sql = @"insert into TB_Collection_PeriodTime(RecTime,WindSpeed) values(@time,@windSpeed)";
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@time",time),
                new SqlParameter("@windSpeed",windSpeed)
                 };
            int res = sqlHelper.ExecuteNonQuery(sql, paras, CommandType.Text);
            if (res > 0)
            {
                log = "气象数据采集成功 :  " + time;
            }
            return log;
        }
    }
}

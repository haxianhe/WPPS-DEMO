/**
 * 创建人：haxianhe
 * 创建时间：2018/6/6 21:02:51
 * 说明：实时数据采集类
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
    class ScadaRealTimeData
    {
        private SQLHelper sqlHelper;                                        //数据库帮助类句柄
        private System.Threading.Thread threadScadaRealTimeData = null;     //实时数据采集线程
        private Boolean isStop = false;                                     //线程关闭标识
        private static ScadaRealTimeData scada = new ScadaRealTimeData();   //实例对象
        public Action<string> refreshtxt;
        public Action<string> errorLog;

        public ScadaRealTimeData()// 默认构造函数
        {
            sqlHelper = new SQLHelper();
        }

        public static ScadaRealTimeData getScadaRealTimeData()// 获取实例对象
        {
            return scada;
        }

        public void Start()// 启动实时数据采集线程
        {
            isStop = false;
            if (ControlModel.getControlModel().ScadaRealTimeData == 0)
                threadScadaRealTimeData = new System.Threading.Thread(MainThread);
            else
                threadScadaRealTimeData = new System.Threading.Thread(MainThreadOpt);
            threadScadaRealTimeData.Start();
        }

        public void Stop()// 停止实时数据模拟线程
        {
            if (threadScadaRealTimeData != null)
            {
                isStop = true;
                System.Threading.Thread.Sleep(100);
                threadScadaRealTimeData.Abort();
            }

        }

        private void MainThread()//实时数据采集线程主函数
        {
            while (!isStop)
            {
                DateTime now = DateTime.Now.AddSeconds(-5);
                int windSpeed = QueryWindSpeed(now.ToString());
                if (windSpeed >= 0)
                {
                    if (refreshtxt != null)
                    {
                        refreshtxt(Insert(now.ToString(), windSpeed));
                    }
                }
                else
                {
                    if (refreshtxt != null)
                    {
                        refreshtxt("采集失败 ： " + now.ToString());
                    }
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void MainThreadOpt()//优化版主函数
        {
            int flag = 0;
            int error = 0;
            while (!isStop)
            {
                DateTime now = DateTime.Now.AddSeconds(-5);
                int windSpeed = QueryWindSpeed(now.ToString());
                if (windSpeed >= 0)
                {
                    if (refreshtxt != null)
                    {
                        refreshtxt(Insert(now.ToString(), windSpeed));
                    }
                    flag = 0;
                    error = 0;
                }
                else
                {
                    if (refreshtxt != null)
                    {
                        refreshtxt("采集失败 ： " + now.ToString());
                    }
                    if (errorLog != null && error == 0)
                    {
                        errorLog("实时数据采集失败 : " + now.ToString());
                        error = 1;
                    }
                    flag++;
                }
                if (flag < 5)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    System.Threading.Thread.Sleep(5000);
                }
            }
        }

        private int QueryWindSpeed(string time)//从实时数据模拟表中查出模拟数据
        {
            int windSpeed = -1;
            string sql = @"select * from TB_DataSource_RealTime where RecTime=@time";
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
            string sql = @"insert into TB_Collection_ShortTerm(RecTime,WindSpeed) values(@time,@windSpeed)";
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@time",time),
                new SqlParameter("@windSpeed",windSpeed)
                 };
            int res = sqlHelper.ExecuteNonQuery(sql, paras, CommandType.Text);
            if (res > 0)
            {
                log = "实时数据采集成功 : " + time;
            }
            return log;
        }
    }
}

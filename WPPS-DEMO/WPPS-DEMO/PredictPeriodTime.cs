/**
 * 创建人：haxianhe
 * 创建时间：2018/6/7 17:17:59
 * 说明：短期功率预测类
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
    class PredictPeriodTime
    {
        private SQLHelper sqlHelper;                                        //数据库帮助类句柄
        private System.Threading.Thread threadPredictPeriodTime = null;     //短期功率预测线程
        private Boolean isStop = false;                                     //线程关闭标识
        private static PredictPeriodTime predict = new PredictPeriodTime(); //实例对象
        public Action<string> refreshtxt;
        public Action<string> errorLog;

        public static PredictPeriodTime getPredictPeriodTime()// 获取实例对象
        {
            return predict;
        }

        public PredictPeriodTime()// 构造函数
        {
            sqlHelper = new SQLHelper();
        }

        public void Start()// 开启短期功率预测线程
        {
            isStop = false;
            if (ControlModel.getControlModel().PredictWeatherData == 0)
                threadPredictPeriodTime = new System.Threading.Thread(MainThread);
            else
                threadPredictPeriodTime = new System.Threading.Thread(MainThreadOpt);
            threadPredictPeriodTime.Start();
        }

        public void Stop()// 停止短期功率预测线程
        {
            if (threadPredictPeriodTime != null)
            {
                isStop = true;
                System.Threading.Thread.Sleep(2000);
                threadPredictPeriodTime.Abort();
            }

        }

        private void MainThread()// 短期功率预测线程主函数
        {
            while (!isStop)
            {
                DateTime _now = DateTime.Now;
                DateTime now = Convert.ToDateTime(_now.Year + "-" + _now.Month + "-" + _now.Day + " T" + _now.Hour + ":00" + ":00");
                int windSpeed = QueryWindSpeed(now.ToString());
                if (windSpeed >= 0)
                {
                    for (int i = 0; i < 10800; i++)
                    {
                        int power = QueryPower(windSpeed);
                        if (refreshtxt != null)
                        {
                            refreshtxt(InsertShortTime(now.ToString(), windSpeed));
                        }
                        _now = _now.AddSeconds(1);
                        System.Threading.Thread.Sleep(100);
                    }
                    System.Threading.Thread.Sleep(1080000);
                }
                else
                {
                    if (refreshtxt != null)
                    {
                        refreshtxt("短期功率预测失败 ：" + now.ToString());
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
                DateTime _now = DateTime.Now;
                DateTime now = Convert.ToDateTime(_now.Year + "-" + _now.Month + "-" + _now.Day + " T" + _now.Hour + ":00" + ":00");
                int windSpeed = QueryWindSpeed(now.ToString());
                if (windSpeed >= 0)
                {
                    for (int i = 0; i < 10800; i++)
                    {
                        int power = QueryPower(windSpeed);
                        if (refreshtxt != null)
                        {
                            refreshtxt(InsertShortTime(now.ToString(), windSpeed));
                        }
                        _now = _now.AddSeconds(1);
                        System.Threading.Thread.Sleep(100);
                    }
                    System.Threading.Thread.Sleep(10800);
                    flag = 0;
                    error = 0;
                }
                else
                {
                    if (refreshtxt != null)
                    {
                        refreshtxt("短期功率预测失败 ：" + now.ToString());
                    }
                    if (errorLog != null && error == 0)
                    {
                        errorLog("短期功率预测失败 : " + now.ToString());
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

        private int QueryWindSpeed(string time)// 从实时数据采集表中查出模拟数据
        {
            int windSpeed = -1;
            string sql = @"select * from TB_Collection_PeriodTime where RecTime=@time";
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

        private int QueryPower(int windSpeed)// 根据风速查出功率
        {
            int power = 0;
            string sql = @"select * from TB_Mapping where WindSpeed=@windSpeed";
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@windSpeed",windSpeed)
            };
            DataTable dt = sqlHelper.ExecuteQuery(sql, paras, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                power = Convert.ToInt32(dt.Rows[0]["Power"]);
            }
            return power;
        }

        private string InsertShortTime(string time, int power)// 插入超短期预测功率
        {
            string log = "";
            string sql = @"insert into TB_Prediction_PeriodTime(RecTime,Power) values(@time,@power)";
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@time",time),
                new SqlParameter("@power",power)
                 };
            int res = sqlHelper.ExecuteNonQuery(sql, paras, CommandType.Text);
            if (res > 0)
            {
                log = "短期功率预测成功 : " + time;
            }
            return log;
        }
    }
}

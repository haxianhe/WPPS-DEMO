/**
 * 创建人：haxianhe
 * 创建时间：2018/6/7 14:18:10
 * 说明：超短期功率预测类
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
    class PredictShortTime
    {
        private SQLHelper sqlHelper;                                        //数据库帮助类句柄
        private System.Threading.Thread threadPredictShortTime = null;     //实时数据采集线程
        private Boolean isStop = false;                                     //线程关闭标识
        private static PredictShortTime predict = new PredictShortTime();   //实例对象
        public Action<string> refreshtxt;
        public Action<string> errorLog;

        public static PredictShortTime getPredictShortTime()// 获取实例对象
        {
            return predict;
        }

        public PredictShortTime()// 构造函数
        {
            sqlHelper = new SQLHelper();
        }

        public void Start()//启动超短期功率预测线程
        {
            isStop = false;
            if (ControlModel.getControlModel().PredictRealTimeData == 0)
                threadPredictShortTime = new System.Threading.Thread(MainThread);
            else
                threadPredictShortTime = new System.Threading.Thread(MainThreadOpt);
            threadPredictShortTime.Start();
        }

        public void Stop()//停止超短期功率预测线程
        {
            if (threadPredictShortTime != null)
            {
                isStop = true;
                System.Threading.Thread.Sleep(2000);
                threadPredictShortTime.Abort();
            }

        }

        private void MainThread()// 超短期功率预测线程主函数
        {
            while (!isStop)
            {
                DateTime now = DateTime.Now.AddSeconds(-20);
                int windSpeed = QueryWindSpeed(now.ToString());

                if (windSpeed >= 0)
                {
                    int power = QueryPower(windSpeed);
                    InsertRealTime(now.ToString(), windSpeed);
                    if (refreshtxt != null)
                    {
                        refreshtxt(InsertShortTime(now.AddSeconds(600).ToString(), windSpeed));
                    }
                    InsertRealTime(now.ToString(), windSpeed);
                }
                else
                {
                    if (refreshtxt != null)
                    {
                        refreshtxt("超短期功率预测失败 ：" + now.ToString());
                    }
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void MainThreadOpt()// 优化版主函数
        {
            int error = 0;
            int flag = 0;
            while (!isStop)
            {
                DateTime now = DateTime.Now.AddSeconds(-20);
                int windSpeed = QueryWindSpeed(now.ToString());

                if (windSpeed >= 0)
                {
                    int power = QueryPower(windSpeed);
                    InsertRealTime(now.ToString(), windSpeed);
                    if (refreshtxt != null)
                    {
                        refreshtxt(InsertShortTime(now.AddSeconds(600).ToString(), windSpeed));
                    }
                    InsertRealTime(now.ToString(), windSpeed);
                    flag = 0;
                    error = 0;
                }
                else
                {
                    if (refreshtxt != null)
                    {
                        refreshtxt("超短期功率预测失败 ：" + now.ToString());
                    }
                    if (errorLog != null && error == 0)
                    {
                        errorLog("超短期功率预测失败 : " + now.ToString());
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
            string sql = @"select * from TB_Collection_ShortTerm where RecTime=@time";
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
            //根据风速从风速-功率对照表中查出功率
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
            string sql = @"insert into TB_Prediction_ShortTerm(RecTime,Power) values(@time,@power)";
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@time",time),
                new SqlParameter("@power",power)
                 };
            int res = sqlHelper.ExecuteNonQuery(sql, paras, CommandType.Text);
            if (res > 0)
            {
                log = "超短期功率预测成功 : " + time;
            }
            return log;
        }

        private string InsertRealTime(string time, int power)// 插入实时功率
        {
            string log = "";
            string sql = @"insert into TB_RealTime(RecTime,Power) values(@time,@power)";
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@time",time),
                new SqlParameter("@power",power)
                 };
            int res = sqlHelper.ExecuteNonQuery(sql, paras, CommandType.Text);
            if (res > 0)
            {
                log = "实时功率计算成功 : " + time;
            }
            return log;
        }
    }
}

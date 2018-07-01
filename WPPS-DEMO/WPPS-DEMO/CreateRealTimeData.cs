/**
 * 创建人：haxianhe
 * 创建时间：2018/6/6 14:05:17
 * 说明：模拟实时数据
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
    class CreateRealTimeData
    {
        private SQLHelper sqlHelper;                                        //数据库帮助类句柄
        private System.Threading.Thread productRealTimeData = null;         //实时数据生产线程
        private Boolean isStop = false;                                     //线程关闭标识
        private static CreateRealTimeData create = new CreateRealTimeData();//单例对象    

        /// <summary>
        /// 获取单例对象
        /// </summary>
        /// <returns></returns>
        public static CreateRealTimeData getCreateRealTimeData()
        {
            return create;
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CreateRealTimeData()
        {
            sqlHelper = new SQLHelper();
        }
        /// <summary>
        /// 启动实时数据模拟线程
        /// </summary>
        public void Start()
        {
            isStop = false;
            productRealTimeData = new System.Threading.Thread(MainThread);
            productRealTimeData.Start();
            //productRealTimeData.Join();
        }

        /// <summary>
        /// 停止实时数据模拟线程
        /// </summary>
        public void Stop()
        {
            if (productRealTimeData != null)
            {
                isStop = true;
                System.Threading.Thread.Sleep(100);
                productRealTimeData.Abort();
            }

        }
        public Action<string> refreshtxt;
        /// <summary>
        /// 实时数据模拟线程主函数
        /// </summary>
        private void MainThread()
        {
            while (!isStop)
            {
                Random ran = new Random();
                int windSpeed = ran.Next(0, 10);
                if (refreshtxt != null)
                {
                    refreshtxt(Insert(DateTime.Now.ToString(), windSpeed));
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// 将模拟数据插入数据库
        /// </summary>
        /// <param name="windSpeed"></param>
        /// <returns></returns>
        private string Insert(string time, int windSpeed)
        {
            string log = "";
            string sql = @"insert into TB_DataSource_RealTime(RecTime,WindSpeed) values(@time,@windSpeed)";
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@windSpeed",windSpeed),
                new SqlParameter("@time",time)
                 };
            int res = sqlHelper.ExecuteNonQuery(sql, paras, CommandType.Text);
            if (res > 0)
            {
                log = "实时数据模拟" + DateTime.Now.ToString() + " : 风速:" + windSpeed;
            }
            return log;
        }
    }
}

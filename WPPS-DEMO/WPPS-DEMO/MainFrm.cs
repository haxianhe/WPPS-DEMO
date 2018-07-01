using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WPPS_DEMO
{
    public partial class MainFrm : Form
    {
        private ProRealTimeData proRealData;    //实时数据模拟窗体
        private ProWeatherData proWeatherData;  //气象数据模拟窗体
        private SCADARealTime scadaRealTime;    //实时数据采集窗体
        private SCADAWeather scadaWeather;      //气象数据采集窗体
        private PreShortTime preShortTime;      //超短期功率预测窗体
        private PrePeriodTime prePeriodTime;    //短期功率预测窗体
        private Query query;                    //数据查询窗体
        private QueryOpt queryOpt;              //数据查询窗体
        private Control control;                //模块调度窗体
        public MainFrm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 主界面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrm_Load(object sender, EventArgs e)
        {
            Login login = new Login();//初始化登录窗体
            login.ShowDialog();       //显示登录窗体
            /**
             * 用户名、密码校验，如果登录成功则弹出
             * 提示框，否则关闭主界面，退出登录状态
             * */
            if (login.DialogResult != DialogResult.OK)
            {
                this.Close();
            }
        }

        /// <summary>
        /// 实时数据模拟菜单项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 实时数据模拟ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //确保每个内部窗体只会出现一次
            if (proRealData == null || proRealData.IsDisposed)
            {
                proRealData = new ProRealTimeData();
                proRealData.Show();
                proRealData.MdiParent = this;
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            if (proRealData == null || proRealData.IsDisposed)
            {
                proRealData = new ProRealTimeData();
                proRealData.Show();
                proRealData.MdiParent = this;
            }
        }

        private void 气象数据模拟ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (proWeatherData == null || proWeatherData.IsDisposed)
            {
                proWeatherData = new ProWeatherData();
                proWeatherData.Show();
                proWeatherData.MdiParent = this;
            }
        }

        /// <summary>
        /// 实时数据模拟工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 实时数据采集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //确保每个内部窗体只会出现一次
            if (scadaRealTime == null || scadaRealTime.IsDisposed)
            {
                scadaRealTime = new SCADARealTime();
                scadaRealTime.Show();
                scadaRealTime.MdiParent = this;
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            if (scadaRealTime == null || scadaRealTime.IsDisposed)
            {
                scadaRealTime = new SCADARealTime();
                scadaRealTime.Show();
                scadaRealTime.MdiParent = this;
            }
        }

        private void 气象数据采集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scadaWeather == null || scadaWeather.IsDisposed)
            {
                scadaWeather = new SCADAWeather();
                scadaWeather.Show();
                scadaWeather.MdiParent = this;
            }
        }

        private void 超短期功率预测ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (preShortTime == null || preShortTime.IsDisposed)
            {
                preShortTime = new PreShortTime();
                preShortTime.Show();
                preShortTime.MdiParent = this;
            }
        }

        private void 短期功率预测ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (prePeriodTime == null || prePeriodTime.IsDisposed)
            {
                prePeriodTime = new PrePeriodTime();
                prePeriodTime.Show();
                prePeriodTime.MdiParent = this;
            }
        }

        private void 数据查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (query == null || query.IsDisposed)
            {
                if (ControlModel.getControlModel().QueryData == 0)
                {
                    query = new Query();
                    query.Show();
                    query.MdiParent = this;
                }
                else
                {
                    queryOpt = new QueryOpt();
                    queryOpt.Show();
                    queryOpt.MdiParent = this;
                }
            }
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            if (query == null || query.IsDisposed)
            {
                query = new Query();
                query.Show();
                query.MdiParent = this;
            }
        }

        private void 模块调度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (control == null || control.IsDisposed)
            {
                control = new Control();
                control.Show();
                control.MdiParent = this;
            }
        }

        private void 层叠排列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void 水平排列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void 垂直排列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }
    }
}

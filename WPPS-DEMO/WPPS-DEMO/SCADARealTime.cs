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
    public partial class SCADARealTime : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SCADARealTime()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 开始采集按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ScadaRealTimeData scada = ScadaRealTimeData.getScadaRealTimeData();
            scada.refreshtxt = (arg) =>
            {
                this.Invoke(new Action(() =>
                {
                    textBox1.Text += arg + "\r\n";
                }));
            };
            scada.Start();
            textBox1.Text = "start\r\n";
        }
        /// <summary>
        /// 停止采集按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            ScadaRealTimeData scada = ScadaRealTimeData.getScadaRealTimeData();
            scada.Stop();
            textBox1.Text = "stop\r\n";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.textBox1.SelectionStart = this.textBox1.Text.Length;
            this.textBox1.SelectionLength = 0;
            this.textBox1.ScrollToCaret();
        }
    }
}

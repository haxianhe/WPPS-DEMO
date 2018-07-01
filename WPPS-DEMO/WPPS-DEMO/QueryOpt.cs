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
    public partial class QueryOpt : Form
    {
        private BindingSource bindingSource1 = new BindingSource();
        public QueryOpt()
        {
            InitializeComponent();
            ScadaRealTimeData scada = ScadaRealTimeData.getScadaRealTimeData();
            scada.errorLog = (arg) =>
            {
                this.Invoke(new Action(() =>
                {
                    textBox3.Text += arg + "\r\n";
                }));
            };
        }
        /// <summary>
        /// 绑定查询区间内的数据
        /// </summary>
        /// <param name="_startTime"></param>
        /// <param name="_stopTime"></param>
        private void BindPowerInfo(string _startTime, string _stopTime)
        {
            bindingSource1.Clear();
            QueryPower queryPower = new QueryPower();//实例化查询类对象
            DateTime startTime = Convert.ToDateTime(_startTime);
            DateTime stopTime = Convert.ToDateTime(_stopTime);
            //如果起止时间-终止时间>=15秒 进行查询，否则绑定数据，结束查询
            while (stopTime.Subtract(startTime).TotalSeconds >= 15)
            {
                string realTimePower = queryPower.QueryRealTimePower(startTime.ToString());
                string shortTimePower = queryPower.QueryShortTimePower(startTime.ToString());
                string periodTimePower = queryPower.QueryPeriodTimePower(startTime.ToString());
                bindingSource1.Add(new PowerModel(startTime.ToString(), realTimePower != null ? realTimePower : "", shortTimePower != null ? shortTimePower : "", periodTimePower != null ? periodTimePower : ""));
                startTime = startTime.AddSeconds(15);
            }
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSize = true;
            dataGridView1.DataSource = bindingSource1;//绑定数据源
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {           
            this.textBox1.SelectionStart = this.textBox1.Text.Length;
            this.textBox1.SelectionLength = 0;
            this.textBox1.ScrollToCaret();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string startTime = textBox1.Text.ToString();
            string stopTime = textBox2.Text.ToString();
            BindPowerInfo(startTime, stopTime);
        }
    }
}

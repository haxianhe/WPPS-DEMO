/**
 * 创建人：haxianhe
 * 创建时间：2018/6/7 20:53:19
 * 说明：<FUNCTION>
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPPS_DEMO
{
    class PowerModel
    {
        private string time;
        private string realTimePower;
        private string shortTimePower;
        private string periodTimePower;

        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        public string RealTimePower
        {
            get { return realTimePower; }
            set { realTimePower = value; }
        }

        public string ShortTimePower
        {
            get { return shortTimePower; }
            set { shortTimePower = value; }
        }
        public string PeriodTimePower
        {
            get { return periodTimePower; }
            set { periodTimePower = value; }
        }

        private PowerModel() { }

        public PowerModel(string time, string realTimePower, string shortTimePower, string periodTimePower)
        {
            this.time = time;
            this.realTimePower = realTimePower;
            this.shortTimePower = shortTimePower;
            this.periodTimePower = periodTimePower;
        }


    }
}

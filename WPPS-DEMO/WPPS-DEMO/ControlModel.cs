/**
 * 创建人：haxianhe
 * 创建时间：2018/6/13 20:07:16
 * 说明：<FUNCTION>
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPPS_DEMO
{
    class ControlModel
    {
        //单例
        private static ControlModel controlModel = new ControlModel();
        public static ControlModel getControlModel()//获取单例对象
        {
            return controlModel;
        }

        //字段
        private int scadaRealTimeData = 0;

        private int scadaWeatherData = 0;

        private int predictRealTimeData = 0;

        private int predictWeatherData = 0;

        private int queryData = 0;

        //属性
        public int ScadaRealTimeData
        {
            get { return scadaRealTimeData; }
            set { scadaRealTimeData = value; }
        }

        public int ScadaWeatherData
        {
            get { return scadaWeatherData; }
            set { scadaWeatherData = value; }
        }

        public int PredictRealTimeData
        {
            get { return predictRealTimeData; }
            set { predictRealTimeData = value; }
        }

        public int PredictWeatherData
        {
            get { return predictWeatherData; }
            set { predictWeatherData = value; }
        }

        public int QueryData
        {
            get { return queryData; }
            set { queryData = value; }
        }
    }
}

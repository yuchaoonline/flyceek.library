using agsXMPP.Xml.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgsXMPP_ConsoleTest001
{
    public class Weather : Element
    {
        public Weather()
        {
            this.TagName = "weather";
            this.Namespace = "ParanoraSoftWare:weather";
        }
        public Weather(int humidity, int temperature)
            : this()
        {
            this.Humidity = humidity;
            this.Temperature = temperature;
        }
        public int Humidity
        {
            get { return GetTagInt("humidity"); }
            set { SetTag("humidity", value.ToString()); }
        }
        public int Temperature
        {
            get { return GetTagInt("temperature"); }
            set { SetTag("temperature", value.ToString()); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoorLib
{

    public class GeoPro//geographic process地理数据处理
    {
        /// <summary>
        /// rad格式转dms的string
        /// </summary>
        /// <param name="rad">弧度</param>
        /// <returns>str</returns>
        public static string Rad2Str(double rad)
        {
            string str = "";
            double d = rad / Math.PI * 180;
            string sign = "";
            if (d < 0)
            {
                sign = "-";
            }
            d = Math.Abs(d);
            double dd, mm, ss;//开始输出度分秒
            dd = Math.Floor(d);//floor：求双精度的地板数，例如5.1取5；-5.1取-6
            mm = Math.Floor((d - dd) * 60.0);
            ss = (d - dd - mm / 60.0) * 3600.0;
            //string.Format("{0:00}", mm);//将对象的值转换为基于指定格式的字符串，并将其插入到另一个字符串。
            str = sign.ToString() + dd.ToString() + "°" + mm.ToString()+ "′" + ss.ToString("f4") + "″";
            return str;
        }

        /// <summary>
        /// Dms2Rad
        /// </summary>
        /// <param name="dms">角度</param>
        /// <returns>rad</returns>
        public static double Dms2Rad(double dms)//角度化弧度
        {
            int sign = 1;
            double rad = 0, sec = 0;
            int deg = 0, minu = 0;
            if (dms < 0)
            {
                sign = -1;
                dms = -dms;
            }
            //deg = (int)(dms + 0.0001);
            //minu = (int)((dms - deg) * 100 + 0.0001);            
            deg = Convert.ToInt32(Math.Floor(dms));
            minu = Convert.ToInt32(Math.Floor((dms - deg) * 100.0));      
            sec = (dms - deg - minu / 100.0) * 10000;//为啥是100不是60

            rad = deg + minu / 60.0 + sec / 3600.0;
            rad = rad / 180.0 * Math.PI;
            rad = rad * sign;
            
            return rad;
        }

        /// <summary>
        /// Rad2Dms
        /// </summary>
        /// <param name="rad">Rad</param>
        /// <returns>dms</returns>
        public static double Rad2Dms(double rad)
        {
            double dms = 0, sec = 0;
            int deg = 0, minu = 0;
            int sign = 1;
            if (rad < 0)
            {
                sign = -1;
                rad = -rad;
            }
            dms = rad / Math.PI * 180;

            //deg = (int)(dms + 0.0001);
            //minu = (int)((dms - deg) * 60 + 0.0001);
            deg = Convert.ToInt32(Math.Floor(dms));
            minu = Convert.ToInt32(Math.Floor((dms - deg) * 60.0));
            sec = (dms - deg - minu / 60.0) * 3600.0;

            dms = deg + minu / 100.0 + sec / 10000.0;
            dms = sign * dms;
            return dms;
        }   
       
    }
}

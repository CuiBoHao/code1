using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoorLib
{
    /// <summary>
    /// 三维坐标转换
    /// </summary>
    public class Position
    {
        private Ellipsoid ell;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ellipsoid">椭球</param>
        public Position(Ellipsoid ellipsoid)
        {
            this.ell = ellipsoid;
        }

        /// <summary>
        /// convert geodetic to cartesian (ECEF) coordinates
        /// 输入： geodetic lat(deg N), lon(deg E),
        ////       height above ellipsoid (meters)
        /// 输出： X,Y,Z in meters in cartesian (ECEF)  coordinates
        /// </summary>
        public void GeodeticToCartesian(double lat, double lon, double height,
            out double X, out double Y, out double Z)//GeodeticToCartesian大地to笛卡尔
            //BLH to XYZ
        {
            double N = ell.N(lat);
            double slat = Math.Sin(lat);//lat是纬度 latitude
            double clat = Math.Cos(lat);

            X = (N + height) * clat * Math.Cos(lon);//lon是经度 longitude
            Y = (N + height) * clat * Math.Sin(lon);
            Z = (N * (1 - ell.eccSq) + height) * slat;//eccentricity偏心率
        }

        /// <summary>
        ///  convert cartesian (ECEF) to geodetic coordinates,
        ///   输路： X,Y,Z in meters in cartesian (ECEF)  coordinates   
        ///   输出： geodetic lat(deg N), lon(deg E),
        ////         height above ellipsoid (meters) 
        /// </summary>
        public void CartesianToGeodetic(double X, double Y, double Z,
            out double lat, out double lon, out double height)
            //XYZ to BLH
        {

            double POSITION_TOLERANCE = 0.0001;//TOLERANCE：公差

            double p, slat, N, htold, latold;
            p = Math.Sqrt(X * X + Y * Y);
            if (p < POSITION_TOLERANCE)
            {  // pole or origin 极点 或是 0度经线
                lat = (Z > 0 ? 90.0 : -90.0);
                lon = 0;// lon undefined, really
                height = Math.Abs( Z - ell.a * Math.Sqrt(1.0 - ell.eccSq));
               
            }
            else
            {

                lat = Math.Atan2(Z, p);  // Math.Atan2(Z, p * (1.0 - ell.eccSq));
                height = 0;
                for (int i = 0; i < 5; i++)
                {
                    slat = Math.Sin(lat);
                    N = ell.N(lat);         // A / Math.Sqrt(1.0 - eccSq * slat * slat);
                    htold = height;
                    height = p / Math.Cos(lat) - N;
                    latold = lat;
                    lat = Math.Atan2(Z + N * ell.eccSq * slat, p);// Math.Atan2(Z, p * (1.0 - ell.eccSq * (N / (N + height))));
                    if (Math.Abs(lat - latold) < 1.0e-9 && Math.Abs(height - htold) < 1.0e-9 * ell.a) break;
                }
                lon = Math.Atan2(Y, X);
                {
                    if (lon < 0.0) lon += 2 * Math.PI;
                }

            }

        }

    }
}

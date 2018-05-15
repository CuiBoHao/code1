using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CoorLib
{
    public class ObsData//observe data 观察数据
    {
        public Ellipsoid Datum;//这条语句语法叫什么呢？声明？创建实例？
        //Datum：n.数据
        //返回值类型是类名？
        public double L0;
        public List<PointInfo> Data;

        public ObsData()
        {
            Datum = new Ellipsoid();
            Data = new List<PointInfo>();

        }
        public ObsData(Ellipsoid ellisoid)
        {
            Datum = ellisoid;
            Data = new List<PointInfo>();
        }
        /// <summary>
        /// 增加一组记录
        /// </summary>
        /// <param name="record">一次测量记录</param>
        public void Add(PointInfo record)
        {
            Data.Add(record);
        }

        string Title()
        {
            string line = string.Format("\n空间直接坐标（XYZ） <-- 大地坐标（ BLH）--> 平面坐标（ xy）\n");
            line += "--------------------------------------\n";
            line += string.Format("{0,-5} {1,15} {2,15} {3,15}",
                 "点名", "X", "Y", "Z");
            line += string.Format(" {0,15} {1,15} {2,10}", "B", "L", "    H");
            line += string.Format(" {0,12} {1,12}\n", "x", "y");
            return line;
        }
        public override string ToString()
        {
            string res = Title();
            foreach (var d in Data)
            {
                res += d.ToString() + "\n";
            }
            return res;
        }

        public DataTable ToDataTable()//和第一句一样？
            //DataTable表示内存中数据的一个表
        {
            DataTable table = InitTable();
            try
            {
                foreach (var d in Data)
                {
                    DataRow row = table.NewRow();
                    //Class.system.data.datarow:表示行中的数据Datatable
                    row["Name"] = d.Name;
                    row["X"] = $"{d.X:f3}";
                    row["Y"] = $"{d.Y:f3}";
                    row["Z"] = $"{d.Z:f3}";
                    row["B"] = GeoPro.Rad2Str(d.B);
                    row["L"] = GeoPro.Rad2Str(d.L);
                    row["H"] = $"{d.H:f3}"; 
                    row["x"] = $"{d.x:f3}"; 
                    row["y"] = $"{d.y:f3}";     
                    table.Rows.Add(row);
                }
            }
            catch (Exception)
            {
            
            }
            return table;
        }
        DataTable InitTable()
        {
            DataTable table = new DataTable("Coor");
            table.Columns.Add("Name", typeof(string));//Columns获取此表属于列的集合
            table.Columns.Add("X", typeof(string));
            table.Columns.Add("Y", typeof(string));
            table.Columns.Add("Z", typeof(string));
            table.Columns.Add("B", typeof(string));
            table.Columns.Add("L", typeof(string));
            table.Columns.Add("H", typeof(string));
            table.Columns.Add("x", typeof(string));
            table.Columns.Add("y", typeof(string));
            return table;
        }
    }
}

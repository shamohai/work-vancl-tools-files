using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GetFiles
{
    /// <summary>
    /// 获取当前路径及子目录所有文件列表
    /// create by jt
    /// </summary>
    class Program
    {
        static List<string> msg = new List<string>();
        private static string path = "";
        static void Main(string[] args)
        {

            path = System.Environment.CurrentDirectory;//Console.ReadLine();

            if (path != null)
            {
                var di = new DirectoryInfo(path);

                GetDicts(di);
                FileInfo[] files = di.GetFiles();
                foreach (var f in files)
                {
                    msg.Add(GetMsg(path, f.FullName));
                }
                //Console.WriteLine(files[0].FullName);
            }

            var fs = new FileStream(path + "\\list.txt", FileMode.OpenOrCreate);
            var writer = new StreamWriter(fs);
            foreach (var m in msg)
            {
                writer.WriteLine(m);
            }
            writer.Flush();
            writer.Close();
            Console.Write("获取完成，请查看当前目录的list.txt文件");
            Console.Read();
        }

        private static string GetMsg(string root, string fullPath)
        {
            return fullPath.Replace(root, "\\");
        }

        private static void GetDicts(DirectoryInfo di)
        {
            var dis = di.GetDirectories();
            if (dis.Any())
            {
                foreach (var d in dis)
                {
                    //Console.WriteLine(d.FullName);
                    msg.Add(GetMsg(path, d.FullName));
                    var files = d.GetFiles();
                    foreach (var file in files)
                    {
                        //Console.WriteLine(file.FullName);
                        msg.Add(GetMsg(path, file.FullName));
                    }
                    GetDicts(d);
                }
            }
            //else
            //{
            //    var files = di.GetFiles();
            //    foreach (var file in files)
            //    {
            //        //Console.WriteLine(file.FullName);
            //        msg.Add(GetMsg(path, file.FullName));
            //    }
            //}

        }
    }
}

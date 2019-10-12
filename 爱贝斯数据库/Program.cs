using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace 艾贝斯数据库
{
    public class Program
    {
        private static string usedata = string.Empty;
        private static List<string> lssql = null;
        private static List<string> Arrsql = null;
        private static string CurrentWorkDir = Directory.GetCurrentDirectory();
        private static string sql = string.Empty;
        static void Main(string[] args)
        {
            string path = CurrentWorkDir + @"\master\master.tb";
            // string strPath = CurrentWorkDir + @"\master\采购入库产品导入样本.txt";
            DateTime begin = DateTime.Now;
            #region 测试
            //string aaa = File.ReadAllText(strPath);
            //FileInfo f = new FileInfo(strPath);
            //StreamReader reader = f.OpenText();
            //string a = "", b = "";
            //int i = 0;
            //b = reader.ReadLine();
            //while (b != null)
            //{
            //    i++;
            //    if (i >= 4000 && i <= 5000)
            //    {
            //        if (b.Split('\t').Contains("fasdfasdf"))
            //        {
            //            a += b + "\r\n";
            //        }
            //    }
            //    b = reader.ReadLine();
            //}

            //reader.Close(); 

            // FileStream streamIn = new FileStream(strPath, FileMode.Open);
            //// FileStream streamOut = new FileStream(CurrentWorkDir + @"\master\写入.txt", FileMode.Create);

            // byte[] read = new byte[1024000];
            // long readlen = 0;
            // int len;
            // while (readlen < streamIn.Length)
            // {
            //     len = streamIn.Read(read, 0, 1024000);
            //     //streamOut.Write(read, 0, len);
            //     readlen += len;
            // }
            // string ss = Encoding.UTF8.GetString (read);
            // //streamOut.Close();
            // streamIn.Close();

            #endregion
            //Other.Row(0, begin);
            //FileStream file = new FileStream("strPath", FileMode.OpenOrCreate, FileAccess.Write);
            //StreamWriter writer = new StreamWriter(file, Encoding.Default);
            //writer.Write(a);
            //writer.Flush();
            //writer.Close();

            if (!File.Exists(path))
            {
                Directory.CreateDirectory(CurrentWorkDir + @"\master");
                File.Create(path).Close();
                File.WriteAllText(path, "root\troot");
            }
            Console.Write(Other.Login);
            string login = Console.ReadLine().Trim();
            string[] loginName_Pwd = login.Split(' ');
            List<string> AllUser = File.ReadAllLines(path).ToList();
            if (AllUser.Contains(string.Join("\t", loginName_Pwd)))
            {
                Console.WriteLine(Other.Success);
                while (true)
                {
                    try
                    {
                        Console.Write("sql> ");
                        sql = Console.ReadLine().Replace(';', ' ').Trim();
                        lssql = sql.Split(' ').ToList();
                        Arrsql = sql.ToUpper().Split(' ').ToList();
                        //反射              
                        Type type = Type.GetType("艾贝斯数据库.Program");
                        MethodInfo Info = type.GetMethod(Arrsql[0]);
                        if (Arrsql[0] == string.Empty)
                        {
                            //回车
                        }
                        else if (Info == null)
                        {
                            Console.WriteLine(Other.NotCommand);
                        }
                        else
                        {
                            object obj = Activator.CreateInstance(type);
                            Info.Invoke(obj, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else
            {
                Environment.Exit(0);
            }
        }

        public static void USE()
        {
            List<string> lsdbName = new DirectoryInfo(CurrentWorkDir).GetDirectories().Select(a => a.Name).ToList();
            if (lsdbName.Contains(lssql[1]))
            {
                usedata = lssql[1];
                Console.WriteLine(Other.DatabaseChanged);
            }
            else
            {
                Console.WriteLine(string.Format("ERROR 1049 (42000): Unknown database '{0}'", lssql[1]));
            }
        }
        public static void DROP()
        {
            if (Arrsql[1] == "TABLE")
            {
                string path = CurrentWorkDir + @"\" + usedata + @"\" + lssql[2];
                if (File.Exists(path + ".data") && File.Exists(path + ".frm") && File.Exists(path + ".index"))
                {
                    File.Delete(path + ".data");
                    File.Delete(path + ".frm");
                    File.Delete(path + ".index");
                    Console.WriteLine(Other.Success);
                }
                else
                {
                    Console.WriteLine(Other.Failing);
                }
            }
            if (Arrsql[1] == "DATABASE")
            {
                string path = CurrentWorkDir + @"\" + lssql[2];
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    Console.WriteLine(Other.Success);
                }
                else
                {
                    Console.WriteLine(Other.Failing);
                }
            }
        }
        public static void CREATE()
        {
            if (Arrsql[1] == "DATABASE")
            {
                Directory.CreateDirectory(CurrentWorkDir + @"\" + lssql[2]);
                usedata = lssql[2];
                Console.WriteLine(Other.Success);
            }
            else if (Arrsql[1] == "TABLE")
            {
                if (!string.IsNullOrEmpty(usedata))
                {
                    string path = CurrentWorkDir + @"\" + usedata + @"\" + lssql[2];
                    File.Create(path + ".data").Close();
                    File.Create(path + ".frm").Close();
                    File.Create(path + ".index").Close();

                    Console.WriteLine(Other.Success);
                }
                else
                {
                    Console.WriteLine(Other.Failing);
                }
            }
            else
            {
                Console.WriteLine(Other.NotCommand);
            }
        }
        public static void SHOW()
        {
            if (Arrsql[1] == "DATABASES")
            {
                DateTime beginTime = DateTime.Now;
                List<string> lsdbName = new DirectoryInfo(CurrentWorkDir).GetDirectories().Select(a => a.Name).ToList();
                if (lsdbName.Count == 0) { Other.Empty(beginTime); return; }
                Grid.print("Database", lsdbName);
                Other.Row(lsdbName.Count, beginTime);
            }
            else if (Arrsql[1] == "TABLES")
            {
                if (string.IsNullOrEmpty(usedata)) { Console.WriteLine("Please select the database"); }
                else
                {
                    DateTime beginTime = DateTime.Now;
                    FileInfo[] ArrFileInfo = new DirectoryInfo(CurrentWorkDir + @"\" + usedata).GetFiles("*.data");
                    List<string> lstbName = ArrFileInfo.Select(a => a.Name.Substring(0, a.Name.LastIndexOf('.'))).ToList();
                    if (lstbName.Count == 0) { Other.Empty(beginTime); return; }
                    Grid.print(string.Format("Tables_in_{0}", usedata), lstbName);
                    Other.Row(lstbName.Count, beginTime);
                }
            }
            else if (Arrsql[1] == "CURRENTDB")
            {
                Console.WriteLine(usedata);
            }
            else
            {
                Console.WriteLine(Other.Failing);
            }
        }
        //update tablename set a='',b=''
        public static void UPDATE() 
        {

        }

        public static void DELETE()
        {
            if (!Arrsql.Contains("WHERE"))
            {
                File.WriteAllText(usedata + @"\" + lssql[1] + ".data", string.Empty);
                Console.WriteLine(Other.Success);
            }
        }

        public static void INSERT()
        {
            string path = usedata + @"\" + lssql[2];
            if (File.Exists(path + ".data"))
            {
                int startIndex = sql.ToUpper().IndexOf("INTO") + 5;
                string[] str = sql.Substring(sql.ToUpper().IndexOf("VALUES") + 8).Split(',').Select(a => a.Replace("'", "").Replace(")", "").Replace(";", "")).ToArray();
                string[] lsFieldType = File.ReadAllLines(path + ".frm", Encoding.UTF8).Select(a => a.Split(' ')[1]).ToArray();
                if (str.Length == lsFieldType.Length)
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        string res;
                        if (!Convert.To(lsFieldType[i], str[i], out res))
                        {
                            Console.WriteLine(str[i] + "类型不正确!");
                            return;
                        }
                        if (res != "") str[i] = res;
                    }
                }
                else
                {
                    if (str.Length > lsFieldType.Length)
                    {
                        Console.WriteLine("列数不一致");
                    }
                    else if (str.Length < lsFieldType.Length) { }
                }
                File.AppendAllText(path + ".data", string.Join("\t", str) + "\r\n", Encoding.UTF8);
                Console.WriteLine(Other.Success);
            }
            else
            {
                Console.WriteLine(Other.NotFind);
            }
        }

        public static void SELECT()
        {
            try
            {
                DateTime beginTime = DateTime.Now;
                string tablename = lssql[Arrsql.IndexOf("FROM") + 1];
                string path = usedata + @"\" + tablename;
                List<string> AllField = File.ReadAllLines(path + ".frm").Select(a => a.Split(' ')[0]).ToList();
                if (Arrsql[1] != "*")
                {
                    List<string> lsField = sql.Substring(sql.ToUpper().IndexOf("SELECT") + 7, sql.ToUpper().IndexOf("FROM") - 8).Split(',').Select(a => a.Trim()).ToList();//获取要查询的字段名
                    List<int> index = new List<int>();//要获取的索引                 
                    foreach (string field in lsField)
                    {
                        for (int i = 0; i < AllField.Count; i++)
                        {
                            if (field.ToUpper() == AllField[i].ToUpper())
                            {
                                index.Add(i);
                            }
                        }
                    }
                    string[] lsData = File.ReadAllLines(path + ".data");
                    for (int i = 0; i < lsData.Length; i++)
                    {
                        string[] i_1 = lsData[i].Split('\t');
                        lsData[i] = string.Empty;
                        foreach (int ii in index)
                        {
                            lsData[i] += i_1[ii] + "\t";
                        }
                        lsData[i] = lsData[i].Substring(0, lsData[i].Length - 1);
                    }
                    Grid.printTb(lsField, lsData.ToList());
                    Other.Row(lsData.Count(), beginTime);
                }
                else
                {
                    List<string> lsData = File.ReadAllLines(path + ".data").Select(a => a.Replace("\r\n", "")).ToList();
                    Grid.printTb(AllField, lsData);
                    Other.Row(lsData.Count(), beginTime);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void CLEAR()
        {
            Console.Clear();
        }
        public static void EXIT()
        {
            Environment.Exit(0);
        }
        public static void LOGOUT()
        {
            usedata = string.Empty;
            Program.Main(null);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace unidata
{
    public static class Grid
    {
        public static void print(string tableName, List<string> ls)
        {
            if (ls.Count == 0) return;
            int Max_Length = ls.Max(a => a.Length);
            Max_Length = Max_Length < tableName.Length ? tableName.Length : Max_Length;
            #region 1.0 线
            //+-------------------------------------+
            Console.Write("+");
            for (int i = 0; i < Max_Length + 2; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
            #endregion
            #region 2.0 表头
            Console.Write("|");
            for (int i = 0; i < Max_Length + 2 - tableName.Length + 1; i++)
            {
                if (i == 0)
                {
                    Console.Write(" ");
                }
                else if (i == 1)
                {
                    Console.Write(tableName);
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine("|");
            #endregion
            #region 3.0 线
            //+-------------------------------------+
            Console.Write("+");
            for (int i = 0; i < Max_Length + 2; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
            #endregion
            #region 4.0 内容
            foreach (string dbName in ls)
            {
                Console.Write("|");
                for (int i = 0; i < Max_Length + 2 - dbName.Length + 1; i++)
                {
                    if (i == 0)
                    {
                        Console.Write(" ");
                    }
                    else if (i == 1)
                    {
                        Console.Write(dbName);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("|");
            }
            #endregion
            #region 5.0 线
            //+-------------------------------------+
            Console.Write("+");
            for (int i = 0; i < Max_Length + 2; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
            #endregion
        }

        public static int getStringLength(string str)
        {

            if (str.Equals(string.Empty))

                return 0;

            int strlen = 0;

            ASCIIEncoding strData = new ASCIIEncoding();

            //将字符串转换为ASCII编码的字节数字

            byte[] strBytes = strData.GetBytes(str);

            for (int i = 0; i <= strBytes.Length - 1; i++)
            {

                if (strBytes[i] == 63) //中文都将编码为ASCII编码63,即"?"号

                    strlen++;

                strlen++;

            }

            return strlen;

        }

        public static void printTb(List<string> ColumnsName, List<string> tbContent)
        {
            List<int> lsMaxLength = new List<int>();
            for (int i = 0; i < ColumnsName.Count; i++)
            {
                int MaxLenght = tbContent.Max(a => getStringLength(a.Split('\t')[i]));
                int cLenght = getStringLength(ColumnsName[i]);
                MaxLenght = MaxLenght < cLenght ? cLenght : MaxLenght;
                lsMaxLength.Add(MaxLenght);
            }
            #region 1.0 线
            //+-------------------------------------+
            Console.Write("+");
            for (int i = 0; i < lsMaxLength.Sum() + lsMaxLength.Count*3-1; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
            #endregion

            #region 2.0 表头
            for (int i = 0; i < ColumnsName.Count; i++)
            {
                Console.Write("|");
                for (int j = 0; j < lsMaxLength[i] + 2 - ColumnsName[i].Length + 1; j++)
                {
                    if (j == 0)
                    {
                        Console.Write(" ");
                    }
                    else if (j == 1)
                    {
                        Console.Write(ColumnsName[i]);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
            }
            Console.WriteLine("|");
            #endregion

            #region 3.0 线
            //+-------------------------------------+
            Console.Write("+");
            for (int i = 0; i < lsMaxLength.Sum() + lsMaxLength.Count * 3 - 1; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
            #endregion

            #region 4.0 内容
            foreach (string[] str in tbContent.Select(a => a.Split('\t')))
            {
                for (int i = 0; i < ColumnsName.Count; i++)
                {
                    Console.Write("|");
                    for (int j = 0; j < lsMaxLength[i] + 2 - getStringLength(str[i]) + 1; j++)
                    {
                        if (j == 0)
                        {
                            Console.Write(" ");
                        }
                        else if (j == 1)
                        {
                            Console.Write(str[i]);
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                }
                Console.WriteLine("|");
            }
            #endregion

            #region 5.0 线
            //+-------------------------------------+
            Console.Write("+");
            for (int i = 0; i < lsMaxLength.Sum() + lsMaxLength.Count * 3 - 1; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
            #endregion
        }
    }
}

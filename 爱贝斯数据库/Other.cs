using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace unidata
{
    public class Other
    {
        public static string Login
        {
            get { return "Please Login:"; }
        }
        public static string Success
        {
            get { return "Successful"; }
        }
        public static string Failing
        {
            get { return "Failing"; }
        }
        public static string NotCommand
        {
            get { return "Not Command"; }
        }
        public static string DatabaseChanged
        {
            get { return "Database Changed"; }
        }
        public static string NotFind
        {
            get { return "Not Find"; }
        }
        public static void Empty(DateTime beginTime)
        {
            Console.WriteLine(string.Format("Empty set ({0:F2} sec)", (DateTime.Now - beginTime).TotalSeconds));
        }
        public static void Row(int rowCount, DateTime beginTime)
        {
            Console.WriteLine(string.Format("{0} rows in set ({1:F2} sec)", rowCount, (DateTime.Now - beginTime).TotalSeconds));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace unidata
{
    public class Convert
    {
        public static bool To(string typename, string value,out string resValue)
        {
            if (typename.ToLower().Contains("int"))
            {
                int res;  resValue = "";
                return int.TryParse(value, out res);
            }
            else if (typename.ToLower().Contains("varchar"))
            {
                resValue = "";
                return true;
            }
            else if (typename.ToLower().Contains("decimal") || typename.ToLower().Contains("money"))
            {
                double res;resValue = "";
                return double.TryParse(value, out res);
            }
            else if (typename.ToLower().Contains("datetime"))
            {
                DateTime res;
                if (DateTime.TryParse(value, out res))
                {
                    resValue = res.ToString();
                    return true;
                }
                else { resValue = ""; return false; }
            }
            else if (typename.ToLower().Contains("bit")) 
            {
                if (value == "1"  || value.ToLower() == "true")
                {
                    resValue = "True";
                    return true;
                }
                else if (value.ToLower() == "false" || value == "0")
                {
                    resValue = "False";
                    return true;
                }
                else
                {
                    resValue = "";
                    return false;
                }
            }
            else
            {
                resValue = "";
                return false;
            }
        }
    }
}

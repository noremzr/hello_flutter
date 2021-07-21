using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Conversoes
{
    public static int CInt(object obj)
    {
        return Convert.ToInt32(obj);
    }

    public static string CStr(object obj)
    {
        return Convert.ToString(obj);
    }

    public static string ValueSqlString(object obj)
    {
        return $"'{CStr(obj).Replace("'", "")}'";
    }
}

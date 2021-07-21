using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DataMemberField<T>
{

    public T Value;

    public string DataMemberName;

    public string Alias;

    public bool IsKey;

    public DataMemberField(string dataMemberName, T valor, bool isKey, string alias = "")
    {
        this.Value = valor;

        this.DataMemberName = dataMemberName;

        this.IsKey = isKey;

        if (!string.IsNullOrEmpty(alias))
        {
            this.Alias = alias;
        }
    }
}

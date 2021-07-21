using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

public class DataMember<T>
{

    public static string Salvar(T obj, SqlConnection conexao, string tableName, bool existe)
    {
        string erro = "";
        if (!existe)
        {
            erro = InsertItem(obj, conexao, tableName);
        }
        else
        {
            erro = UpdateItem(obj, conexao, tableName);
        }
        return erro;
    }

    private static string InsertItem(T obj, SqlConnection conexao, string tableName)
    {
        string erro = "";

        StringBuilder sqlInsertTable = new StringBuilder();

        StringBuilder sqlInsertFields = new StringBuilder();

        StringBuilder sqlInsertValues = new StringBuilder();

        sqlInsertTable.Append($"INSERT INTO {tableName} (");

        bool isFirst = true;

        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (FieldInfo property in fields)
        {
            if (!isFirst)
            {
                sqlInsertFields.Append(" , ");
                sqlInsertValues.Append(" , ");
            }
            Type type = property.FieldType;
            if (type.Name.Contains("DataMemberField"))
            {
                try
                {
                    DataMemberField<string> dmf = (DataMemberField<string>)property.GetValue(obj);
                    sqlInsertFields.Append(dmf.DataMemberName);
                    sqlInsertValues.Append(dmf.Value);
                }
                catch
                {
                    try
                    {
                        DataMemberField<int> dmf = (DataMemberField<int>)property.GetValue(obj);
                        sqlInsertFields.Append(dmf.DataMemberName);
                        sqlInsertValues.Append(dmf.Value);
                    }
                    catch
                    {
                        DataMemberField<DateTime> dmf = (DataMemberField<DateTime>)property.GetValue(obj);
                        sqlInsertFields.Append(dmf.DataMemberName);
                        sqlInsertValues.Append(dmf.Value);
                    }
                }
            }
            isFirst = false;
        }
        try
        {
            StringBuilder insert = new StringBuilder();

            insert.Append($"{sqlInsertTable.ToString()} {sqlInsertFields.ToString()}) VALUES({sqlInsertValues.ToString()})");

            SqlCommand command = new SqlCommand(insert.ToString(), conexao);
            command.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            erro = ex.Message;
        }

        return erro;
    }

    public static T PopulateObject(IDataReader dr, T obj)
    {
        if (obj == null)
        {
            return default(T);
        }

        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (FieldInfo property in fields)
        {
            Type type = property.FieldType;
            if (type.Name.Contains("DataMemberField"))
            {
                try
                {
                    DataMemberField<string> dmf = (DataMemberField<string>)property.GetValue(obj);

                    dmf.Value = (string)dr[dmf.DataMemberName];
                }
                catch (Exception ex)
                {
                    try
                    {
                        DataMemberField<int> dmf = (DataMemberField<int>)property.GetValue(obj);
                        dmf.Value = (int)dr[dmf.DataMemberName];
                    }
                    catch (Exception exs)
                    {
                        throw new Exception("Tipo não implementado, ajuda ai!");
                    }
                }
            }
        }
        return obj;
    }

    public static string getProperties(T obj, string tableAlias)
    {

        StringBuilder sqlSelect = new StringBuilder();

        bool first = true;

        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (FieldInfo property in fields)
        {
            Type type = property.FieldType;
            if (type.Name.Contains("DataMemberField"))
            {
                {
                    if (!first)
                    {
                        sqlSelect.Append(" , ");
                    }
                    first = false;
                    try
                    {
                        DataMemberField<string> dmf = (DataMemberField<string>)property.GetValue(obj);
                        sqlSelect.Append($"{tableAlias}.{dmf.DataMemberName} AS {dmf.DataMemberName}");
                    }
                    catch
                    {
                        try
                        {
                            DataMemberField<int> dmf = (DataMemberField<int>)property.GetValue(obj);
                            sqlSelect.Append($"{tableAlias}.{dmf.DataMemberName} AS {dmf.DataMemberName}");
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Tipo não implementado, ajuda ai!");
                        }
                    }
                }
            }
        }
        return sqlSelect.ToString();
    }

    private static string UpdateItem(T obj, SqlConnection conexao, string tableName)
    {
        string erro = "";

        StringBuilder sqlUpdateTable = new StringBuilder();

        StringBuilder sqlUpdateFieldValues = new StringBuilder();

        StringBuilder sqlUpdateKeyValues = new StringBuilder();

        sqlUpdateTable.Append($"UPDATE {tableName} SET  ");

        bool isFirst = true;

        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (FieldInfo property in fields)
        {
            if (!isFirst)
            {
                sqlUpdateFieldValues.Append(" , ");
                sqlUpdateKeyValues.Append(" AND ");
            }
            Type type = property.FieldType;
            if (type.Name.Contains("DataMemberField"))
            {
                try
                {
                    DataMemberField<string> dmf = (DataMemberField<string>)property.GetValue(obj);
                    if (dmf.IsKey)
                    {
                        sqlUpdateKeyValues.Append($"{dmf.DataMemberName} = '{dmf.Value.Replace("'", "")}'");
                    }
                    else
                    {
                        sqlUpdateFieldValues.Append($"{dmf.DataMemberName} = '{dmf.Value.Replace("'", "")}'");
                    }
                }
                catch
                {
                    try
                    {
                        DataMemberField<int> dmf = (DataMemberField<int>)property.GetValue(obj);
                        if (dmf.IsKey)
                        {
                            sqlUpdateKeyValues.Append($"{dmf.DataMemberName} = {dmf.Value}");
                        }
                        else
                        {
                            sqlUpdateFieldValues.Append($"{dmf.DataMemberName} = {dmf.Value}");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Tipo não implementado, ajuda ai!");
                    }
                }
            }
            isFirst = false;
        }
        try
        {
            StringBuilder update = new StringBuilder();

            update.Append($"{sqlUpdateTable.ToString()} {sqlUpdateFieldValues.ToString()}  WHERE {sqlUpdateKeyValues.ToString()}");

            SqlCommand command = new SqlCommand(update.ToString(), conexao);
            command.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            erro = ex.Message;
        }

        return erro;
    }
}

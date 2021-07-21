using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

public class AutorDAO : IDAO<AutorFilter, AutorModel>, IDAOSaveDelete<AutorModel>
{


    public AutorDAO(SqlConnection connection)
    {
        this.Connection = connection;
    }
    public string TableName
    {
        get { return AutorModel.TableName; }
    }

    public SqlConnection Connection { get; set; }
    public void ValidateFilter(AutorFilter autor)
    {
    }

    public void ValidateModel(AutorModel autor)
    {
        if (String.IsNullOrEmpty(autor.Nome))
        {
            throw new Exception("Defina um usuário");
        }
    }

    public string Save(AutorModel autor)
    {
        DataMember<AutorModel> dataMember = new DataMember<AutorModel>();
        string erro = DataMember<AutorModel>.Salvar(autor, Connection, this.TableName, autor.Existe);
        return erro;
    }

    public string Delete(AutorModel autor)
    {
        DataMember<AutorModel> dataMember = new DataMember<AutorModel>();

        //string erro = DataMember<AutorModel>.(autor.CodDocumento, TableName, Connection);
        return "";
    }

    public string GetSqlSelect(AutorFilter usuario)
    {
        this.ValidateFilter(usuario);
        StringBuilder sql = new StringBuilder();
        sql.AppendLine($"SELECT {DataMember<AutorModel>.getProperties(new AutorModel(),"Aut")}");
        sql.AppendLine("FROM NrmAutor");

        return sql.ToString();
    }

    public List<AutorModel> FindAll(AutorFilter autorFilter)
    {
        string sql = GetSqlSelect(autorFilter);
        List<AutorModel> listaAutores = new List<AutorModel>();
        using (SqlCommand cmd = new SqlCommand(sql,Connection))
        {
            using (IDataReader dr = cmd.ExecuteReader()) {
                AutorModel autor = new AutorModel();
                DataMember<AutorModel>.PopulateObject(dr, autor);
                listaAutores.Add(autor);
            }
        }
       
        return listaAutores;
    }

    public AutorModel FindOne(AutorFilter usuarioFilter)
    {
        List<AutorModel> autores = this.FindAll(usuarioFilter);
        return autores.FirstOrDefault();
    }

    public AutorModel LoadObject(IDataReader dataReader)
    {
        AutorModel usuario = new AutorModel(0, "","", false, "");
        return usuario;
    }

}

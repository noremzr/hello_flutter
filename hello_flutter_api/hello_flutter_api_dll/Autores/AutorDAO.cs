using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

public class AutorDAO : IDAO<AutorFilter, AutorModel>, IDAOSaveDelete<AutorModel>
{


    public AutorDAO(FirestoreDb connection)
    {
        this.Connection = connection;
    }
    public string TableName
    {
        get { return AutorModel.TableName; }
    }

    public FirestoreDb Connection { get; set; }
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

    public async Task<string> Save(AutorModel autor)
    {
        DataMember<AutorModel> dataMember = new DataMember<AutorModel>();
        string erro = await dataMember.Salvar(autor, Connection, this.TableName, autor.Existe);
        return erro;
    }

    public async Task<string> Delete(AutorModel autor)
    {
        DataMember<AutorModel> dataMember = new DataMember<AutorModel>();

        string erro = await dataMember.DeleteByDocument(autor.CodDocumento, TableName, Connection);
        return erro;
    }

    public string GetSqlSelect(AutorFilter usuario)
    {
        this.ValidateFilter(usuario);
        StringBuilder sql = new StringBuilder();
        sql.AppendLine("Select Exemplo não será usado!");
        return sql.ToString();
    }

    public async Task<List<AutorModel>> FindAll(AutorFilter autorFilter)
    {
        Query Qref = GetQuery(autorFilter);
        QuerySnapshot snap = await Qref.GetSnapshotAsync();
        List<AutorModel> ListaAutores = new List<AutorModel>();
        foreach (DocumentSnapshot docsnap in snap)
        {
            AutorModel autor = docsnap.ConvertTo<AutorModel>();
            if (docsnap.Exists)
            {
                autor.CodDocumento = docsnap.Id;
                ListaAutores.Add(autor);
            }
        }
        return ListaAutores;
    }

    public async Task<AutorModel> FindOne(AutorFilter usuarioFilter)
    {
        List<AutorModel> autores = await this.FindAll(usuarioFilter);
        return autores.FirstOrDefault();
    }

    private Query GetQuery(AutorFilter autorFilter)
    {
        Query Qref = Connection.Collection(TableName);
        AutorModel autor = new AutorModel();
        if (!string.IsNullOrEmpty(autorFilter.Nome))
        {
            Qref = Qref.OrderBy(autor.NomeDM.DataMemberName).StartAt(autorFilter.Nome).EndAt(autorFilter.Nome+'\uf8ff').Limit(50);
        }
        return Qref;
    }

    public AutorModel LoadObject(IDataReader dataReader)
    {
        AutorModel usuario = new AutorModel(0, "","", false, "");
        return usuario;
    }

}

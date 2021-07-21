using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Newtonsoft.Json;

[FirestoreData()]
public class AutorModel : IDAOModel<AutorModel>
    { 
    public AutorModel(int codAutor,string nome, string observacao, bool existe, string codDocumento)
    {
        this.CodAutor = codAutor;
        this.Nome = nome;
        this.Observacao = observacao;
        this.Existe = existe;
        this.CodDocumento = codDocumento;
    }

    public AutorModel() { }

    [JsonProperty("existe")]
    public bool Existe { get; set; }
    [JsonProperty("codDocumento")]
    public string CodDocumento { get; set; }

    public static string TableName = "Autores";


    [JsonIgnore()]
    public DataMemberField<string> NomeDM = new DataMemberField<string>("Nome", string.Empty, true);
    [JsonIgnore()]
    public DataMemberField<string> ObservacaoDM = new DataMemberField<string>("Observacao", string.Empty, false);
    [JsonIgnore()]
    public DataMemberField<int> CodAutorDM = new DataMemberField<int>("CodAutor", 0, false);

    [FirestoreProperty()]
    [JsonProperty("nome")]
    public string Nome
    {
        get { return this.NomeDM.Value; }
        set { this.NomeDM.Value = value; }
    }

    [FirestoreProperty()]
    [JsonProperty("senha")]
    public string Observacao
    {
        get { return this.ObservacaoDM.Value; }
        set { this.ObservacaoDM.Value = value; }
    }

    [FirestoreProperty()]
    [JsonProperty("codAutor")]
    public int CodAutor
    {
        get { return this.CodAutorDM.Value; }
        set { this.CodAutorDM.Value = value; }
    }
}


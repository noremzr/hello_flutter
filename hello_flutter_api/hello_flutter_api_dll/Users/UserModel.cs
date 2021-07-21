using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Newtonsoft.Json;

[FirestoreData()]
public class UserModel : IDAOModel<UserModel>
{
    public UserModel(string usuario, string senha, bool existe,string codDocumento)
    {
        this.usuario = usuario;
        this.senha = senha;
        this.Existe = existe;
        this.CodDocumento = codDocumento;
    }

    public UserModel(){}

        [JsonProperty("existe")]
        public bool Existe { get; set; }
    [JsonProperty("codDocumento")]
        public string CodDocumento { get; set; }

    public static string TableName = "Usuarios";


        [JsonIgnore()]
        public DataMemberField<string> usuarioDM = new DataMemberField<string>("usuario", string.Empty, true);
        [JsonIgnore()]
        public DataMemberField<string> senhaDM = new DataMemberField<string>("senha", string.Empty, false);

        [FirestoreProperty()]
        [JsonProperty("usuario")]
        public string usuario
    {
            get { return this.usuarioDM.Value; }
            set { this.usuarioDM.Value = value; }
        }

        [FirestoreProperty()]
        [JsonProperty("senha")]
        public string senha
        {
            get { return this.senhaDM.Value; }
            set { this.senhaDM.Value = value; }
}

    }


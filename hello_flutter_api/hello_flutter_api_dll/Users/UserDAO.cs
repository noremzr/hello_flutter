using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

    public class UserDAO : IDAO<UserFilter, UserModel>, IDAOSaveDelete<UserModel>
    {


        public UserDAO(FirestoreDb connection)
        {
            this.Connection = connection;
        }
        public string TableName
        {
            get { return UserModel.TableName; }
        }

        public FirestoreDb Connection { get; set; }
        public void ValidateFilter(UserFilter usuario)
        {
        }

        public void ValidateModel(UserModel usuario)
        {
            if (String.IsNullOrEmpty(usuario.usuario))
            {
                throw new Exception("Defina um usuário");
            }
            else if (String.IsNullOrEmpty(usuario.senha))
            {
                throw new Exception("Defina uma senha");
            }
        }

        public async Task<string> Save(UserModel usuario)
        {
            DataMember<UserModel> dataMember = new DataMember<UserModel>();
            string erro = await dataMember.Salvar(usuario, Connection, this.TableName, usuario.Existe);
            return erro;
        }

        public async Task<string> Delete(UserModel usuario)
        {
        DataMember<UserModel> dataMember = new DataMember<UserModel>();

        string erro = await dataMember.DeleteByDocument(usuario.CodDocumento,TableName,Connection);
        return erro;
        }

        public string GetSqlSelect(UserFilter usuario)
        {
            this.ValidateFilter(usuario);
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Select Exemplo não será usado!");
            return sql.ToString();
        }

        public async Task<List<UserModel>> FindAll(UserFilter usuarioFilter)
        {
            Query Qref = GetQuery(usuarioFilter);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            List<UserModel> ListaUsuarios = new List<UserModel>();
            foreach (DocumentSnapshot docsnap in snap)
            {
            UserModel usuario = docsnap.ConvertTo<UserModel>();
                if (docsnap.Exists)
                {
                    usuario.CodDocumento = docsnap.Id;
                    ListaUsuarios.Add(usuario);
                }
            }
            return ListaUsuarios;
        }

        public async Task<UserModel> FindOne(UserFilter usuarioFilter)
        {
            List<UserModel> usuarios = await this.FindAll(usuarioFilter);
            return usuarios.FirstOrDefault();
        }

        private Query GetQuery(UserFilter usuarioFilter)
        {
            Query Qref = Connection.Collection(TableName);
        UserModel usuario = new UserModel();
            if (!string.IsNullOrEmpty(usuarioFilter.Usuario))
            {
                Qref = Qref.WhereEqualTo(usuario.usuarioDM.DataMemberName, usuarioFilter.Usuario);
            }
            return Qref;
        }

        public UserModel LoadObject(IDataReader dataReader)
        {
        UserModel usuario = new UserModel("", "", false,"");
            return usuario;
        }

    }

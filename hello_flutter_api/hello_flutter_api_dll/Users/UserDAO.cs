using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

    public class UserDAO : IDAO<UserFilter, UserModel>, IDAOSaveDelete<UserModel>
    {


        public UserDAO(SqlConnection connection)
        {
            this.Connection = connection;
        }
        public string TableName
        {
            get { return UserModel.TableName; }
        }

        public SqlConnection Connection { get; set; }
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

        public string Save(UserModel usuario)
        {
            DataMember<UserModel> dataMember = new DataMember<UserModel>();
           string erro =  DataMember<UserModel>.Salvar(usuario, Connection, this.TableName, usuario.Existe);
            return erro;
        }

        public string Delete(UserModel usuario)
        {
        return "";
        }

        public string GetSqlSelect(UserFilter usuario)
        {
            this.ValidateFilter(usuario);
        StringBuilder sql = new StringBuilder();
        sql.AppendLine($"SELECT {DataMember<UserModel>.getProperties(new UserModel(), "Aut")}");
        sql.AppendLine(" FROM tUsuario Aut");

        return sql.ToString();
    }

        public List<UserModel> FindAll(UserFilter usuarioFilter)
        {
        string sql = GetSqlSelect(usuarioFilter);
        List<UserModel> listaAutores = new List<UserModel>();
        using (SqlCommand cmd = new SqlCommand(sql, Connection))
        {
            using (IDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read()) { 
                UserModel autor = new UserModel();
                DataMember<UserModel>.PopulateObject(dr, autor);
                listaAutores.Add(autor);
                }
            }
        }

        return listaAutores;
    }

        public UserModel FindOne(UserFilter usuarioFilter)
        {
            List<UserModel> usuarios =  this.FindAll(usuarioFilter);
            return usuarios.FirstOrDefault();
        }

        public UserModel LoadObject(IDataReader dataReader)
        {
        UserModel usuario = new UserModel("", "", false,"");
            return usuario;
        }

    }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using System.Web.Http;
using System.Diagnostics;
public abstract class BaseController : ApiController
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private FirestoreDb _conexao;



    public FirestoreDb Conexao
    {
        get
        {
            if (this._conexao == null)
            {
                Connection con = new Connection();
                this._conexao = con.DefaultConnection;
                return this._conexao;
            }
            return this.Conexao;
        }
    }

    //public string Usuario
    //{
    //    get
    //    {
    //        return this.User.Identity.Name;
    //    }
    //}

    //public bool IsAdministrador
    //{
    //    get
    //    {
    //        return this.User.IsInRole(Roles.Administrador);
    //    }
    //}



    //private void DisposeConexao()
    //{
    //    if (this._conexao != null)
    //    {
    //        this._conexao.Dispose();
    //        this._conexao = null;
    //    }
    //}

    //protected override void Dispose(bool disposing)
    //{
    //    base.Dispose(disposing);
    //    if (disposing)
    //        this.DisposeConexao();
    //}

    //public bool IsUsuario(string usuario)
    //{
    //    return string.Equals(usuario, this.Usuario, StringComparison.InvariantCultureIgnoreCase);
    //}
}
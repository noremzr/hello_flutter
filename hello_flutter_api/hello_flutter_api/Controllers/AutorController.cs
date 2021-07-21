using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

    [RoutePrefix("api/autores")]
    public class AutorController : BaseController
    {
    // GET: Users
    //[Authorize()]
    [HttpGet]
    [Route("GetAutores")]
    public async Task<List<AutorModel>> GetAutores()
    {

        AutorDAO userDAO = new AutorDAO(this.Conexao);
        List<AutorModel> listaUsuarios = userDAO.FindAll(new AutorFilter());
        return listaUsuarios;
    }
        [HttpGet]
        [Route("GetAutor/{codAutor}")]
        public async Task<AutorModel> GetAutorByCod(int? codAutor)
        {
            if (!codAutor.HasValue) { throw new ArgumentException("CodAutor"); }

                AutorFilter autorFilter = new AutorFilter((int)codAutor);
        AutorDAO autorDAO = new AutorDAO(this.Conexao);
            AutorModel autor = autorDAO.FindOne(autorFilter);
            if (autor is null)
            {
                autor = new AutorModel(0,"","",false,"");
            }
            return autor;
        }

    [HttpGet]
    [Route("GetAutorByName/{nome}")]
    public async Task<List<AutorModel>> GetAutorByName([FromUri()]string nome)
    {
        if (string.IsNullOrEmpty(nome)) { throw new ArgumentException("Nome"); }

        AutorFilter autorFilter = new AutorFilter(nome);
        AutorDAO autorDAO = new AutorDAO(this.Conexao);
        List<AutorModel> autores = autorDAO.FindAll(autorFilter);
        if (autores.Count <=0)
        {
            autores = new List<AutorModel>();
        }
        return autores;
    }

        [HttpGet]
        [Route("DeleteAutor/{codDocumento}")]
        public async Task<int> DeleteAutor(string codDocumento)
        {
            int codRetorno = 200;


            if (string.IsNullOrEmpty(codDocumento)) { throw new ArgumentException("codDocumento"); }

            AutorDAO autorDAO = new AutorDAO(this.Conexao);
            AutorModel autorModel = new AutorModel(0,"", "", true, codDocumento);
            try
            {
                autorDAO.Delete(autorModel);
            }
            catch (Exception ex)
            {
                codRetorno = 501;
                throw ex;
            }
            return codRetorno;
        }



        //[Authorize()]
        [HttpPost]
        [Route("PostAutor")]
        public async Task<int> PostAutor([FromBody] AutorModel autor)
        {
            int codRetorno = 200;
            if (autor == null)
            {
                throw new Exception("Autor");
            }
            AutorDAO autorDAO = new AutorDAO(this.Conexao);
            try
            {
                string erro = autorDAO.Save(autor);
            }
            catch (Exception ex)
            {
                codRetorno = 500;
                throw ex;
            }
            return codRetorno;
        }
    }

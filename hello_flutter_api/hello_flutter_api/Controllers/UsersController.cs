﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace hello_flutter_api.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : BaseController
    {
        // GET: Users
        //[Authorize()]
        [HttpGet]
        [Route("GetUsers")]
        public async Task<List<UserModel>> GetUsers() {

            UserDAO userDAO = new UserDAO(this.Conexao);
            List<UserModel> listaUsuarios = await userDAO.FindAll(new UserFilter());
            return listaUsuarios;
        }

        [HttpGet]
        [Route("GetUser/{codUsuario}")]
        public async Task<UserModel> GetUser(string codUsuario)
        {
            if (string.IsNullOrEmpty(codUsuario)){ throw new ArgumentException("usuario"); }

            UserFilter userFilter = new UserFilter(codUsuario);
            UserDAO userDAO = new UserDAO(this.Conexao);
            UserModel usuario = await userDAO.FindOne(userFilter);
            if (usuario is null) {
                usuario = new UserModel();
            }
            return usuario;
        }

        [HttpPost]
        [Route("ValidateUser")]
        public async Task<UserValidatorModel> ValidateUser([FromBody] UserModel usuarioLogin)
        {
            if (usuarioLogin is null) { throw new ArgumentException("usuario"); }

            UserFilter userFilter = new UserFilter(usuarioLogin.usuario);
            UserDAO userDAO = new UserDAO(this.Conexao);
            UserModel usuario = await userDAO.FindOne(userFilter);

            bool usuarioNaoExiste = false;
            bool senhasDiferentes = false;
            if (usuario is null)
            {
                usuarioNaoExiste = true;
            }
            else if (usuario.senha != usuarioLogin.senha) {
                senhasDiferentes = true;
            }

            UserValidatorModel userValidator = new UserValidatorModel(usuarioNaoExiste, senhasDiferentes);
            return userValidator;
        }

        [HttpGet]
        [Route("DeleteUser/{codDocumento}")]
        public async Task<int> DeleteUser(string codDocumento) {
            int codRetorno = 200;


            if (string.IsNullOrEmpty(codDocumento)) { throw new ArgumentException("codDocumento"); }

            UserDAO userDAO = new UserDAO(this.Conexao);
            UserModel userModel = new UserModel("", "", true,codDocumento);
            try
            {
                await userDAO.Delete(userModel);
            }
            catch (Exception ex) {
                codRetorno = 501;
                throw ex;
            }
            return codRetorno;
        }



        //[Authorize()]
        [HttpPost]
        [Route("PostUser")]
        public async Task<int> PostUser([FromBody] UserModel usuario)
        {
            int codRetorno = 200;
            if (usuario == null) {
                throw new Exception("Usuário");
            }
            UserDAO userDAO = new UserDAO(this.Conexao);
            try {
                string erro = await userDAO.Save(usuario);
            }
            catch(Exception ex) {
                codRetorno = 500;
                throw ex;
            }
            return codRetorno;
        }
    }
}
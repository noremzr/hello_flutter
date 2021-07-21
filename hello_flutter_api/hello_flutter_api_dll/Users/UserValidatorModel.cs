using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Newtonsoft.Json;

   public class UserValidatorModel
    {

        public UserValidatorModel(bool usuarioNaoExiste, bool senhaDiferente)
        {
            this.senhaDiferente = senhaDiferente;
            this.usuarioNaoExiste = usuarioNaoExiste;
        }

        [JsonProperty("senhaDiferente")]
        public bool senhaDiferente
        {get;set;}

        [JsonProperty("usuarioNaoExiste")]
        public bool usuarioNaoExiste
        {get;set;}
    }

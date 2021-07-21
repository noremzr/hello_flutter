using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class AutorFilter
{
    [JsonProperty("nome")]
    public string Nome;
    [JsonProperty("codAutor")]
    public int CodAutor;

    public AutorFilter(string nome)
    {
        this.Nome = nome;
    }

    public AutorFilter(int codAutor)
    {
        this.CodAutor = codAutor;
    }
    public AutorFilter(int codAutor,string nome)
    {
        this.CodAutor = codAutor;
        this.Nome = nome;
    }

    public AutorFilter() { }
}

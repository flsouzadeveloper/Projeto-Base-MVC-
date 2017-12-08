using System.Collections.Generic;
using System.Linq;

namespace EstudoArchictetureDDDVS2015.Models.Genericas
{
    public class BotoesVisiveisModel
    {
        private readonly IDictionary<string, bool> _visiveis = new Dictionary<string, bool>();

        private bool Get(string key)
        {
            return _visiveis.ContainsKey(key) && _visiveis[key];
        }

        public bool Criar
        {
            get { return Get("Criar"); }
            set { _visiveis["Criar"] = value; }
        }

        public bool CancelarCriacao
        {
            get { return Get("CancelarCriacao"); }
            set { _visiveis["CancelarCriacao"] = value; }
        }

        public bool Editar
        {
            get { return Get("Editar"); }
            set { _visiveis["Editar"] = value; }
        }

        public bool CancelarEdicao
        {
            get { return Get("CancelarEdicao"); }
            set { _visiveis["CancelarEdicao"] = value; }
        }

        public bool Excluir
        {
            get { return Get("Excluir"); }
            set { _visiveis["Excluir"] = value; }
        }

        public bool Visualizar
        {
            get { return Get("Visualizar"); }
            set { _visiveis["Visualizar"] = value; }
        }

        public bool Salvar
        {
            get { return Get("Salvar"); }
            set { _visiveis["Salvar"] = value; }
        }

        public bool TemAlgumVisivel()
        {
            return _visiveis.Any(a => a.Value);
        }

        public bool this[string key]
        {
            get { return Get(key); }
            set { _visiveis[key] = value; }
        }
    }
}
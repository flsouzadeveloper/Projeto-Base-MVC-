namespace EstudoArchictetureDDDVS2015Util.Security
{
    public class UsuarioLogado
    {
        public int IdUsuarioNoSistema { get; set; }
        public long? IdUsuarioNoSgu { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Perfil { get; set; }

        //Verificar como funciona a propriedade IsSuperUsuario
        public virtual bool IsSuperUsuario
        {
            get { return false; }
        }

        //Verificar o conceito de autorização no sistema
        //public Autorizacao[] autorizacoes { get; set; }
    }
}

namespace EstudoArchitectureDDDVS2015Entity.Database
{
    public partial class LogImportacaoErroEntity
    {
        public int IdLogImportacaoErro { get; set; }
        public int IdLogImportacaoLinha { get; set; }
        public string NomeColuna { get; set; }
        public string Info { get; set; }
        public string Mensagem { get; set; }
        public virtual LogImportacaoLinhaEntity LogImportacaoLinha { get; set; }
    }
}

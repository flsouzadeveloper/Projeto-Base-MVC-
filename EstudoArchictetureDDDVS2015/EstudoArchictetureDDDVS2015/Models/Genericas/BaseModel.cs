namespace EstudoArchictetureDDDVS2015.Models.Genericas
{
    public class BaseModel
    {
        private readonly BotoesVisiveisModel _botoesVisiveis = new BotoesVisiveisModel();
        public BotoesVisiveisModel BotoesVisiveis
        {
            get { return _botoesVisiveis; }
        }
    }
}
namespace EstudoArchictetureDDDVS2015Util.Mensagens
{
    public class PadraoDataAnnotations
    {
        public const string Required = "O campo {0} é obrigatório.";

        public const string StringLength = "O campo {0} não pode superar {1} caracteres.";

        public const string Integer = "O campo {0} deve ser um número inteiro maior que zero.";

        public const string IntegerRange = "O campo {0} deve ser um número inteiro entre {1} e {2}.";

        public const string IntegerOverflow = "O valor {0} é muito grande ou muito pequeno para um valor numérico inteiro.";

        public const string Ddd = "O campo {0} deve ser um valor válido para DDD.";

        public const string RegularExpressionFif = "O campo {0} deve estar no formato: 0-000000000000-00";
    }
}

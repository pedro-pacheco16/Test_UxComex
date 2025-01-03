namespace Teste_UxComex.Models
{
    public class Endereco
    {
        public int EnderecoId { get; set; }
        public int PessoaId { get; set; }
        public string EnderecoCompleto { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}

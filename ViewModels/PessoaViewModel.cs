using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Teste_UxComex.Models;

namespace Teste_UxComex.ViewModels
{
    public class PessoaViewModel
    {
        public Pessoa Pessoa { get; set; }

        [ValidateNever]
        public List<Endereco> Enderecos { get; set; } 
    }
}

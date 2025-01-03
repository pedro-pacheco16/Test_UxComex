using Teste_UxComex.Models;

namespace Teste_UxComex.Repositories.Interfaces
{
    public interface IEnderecoRepository
    {
        Task<IEnumerable<Endereco>> GetByPessoaIdAsync(int pessoaId);
        Task<Endereco?> GetByIdAsync(int enderecoId);
        Task<bool> AddAsync(Endereco endereco);
        Task<bool> UpdateAsync(Endereco endereco);
        Task<bool> DeleteAsync(int enderecoId);
    }
}

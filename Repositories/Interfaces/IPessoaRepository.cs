using Teste_UxComex.Models;

namespace Teste_UxComex.Repositories.Interfaces
{
    public interface IPessoaRepository
    {
        Task<List<Pessoa>> GetAllAsync();
        Task<Pessoa> GetAsync(int id);
        Task<bool> AddAsync(Pessoa pessoa);
        Task<bool> UpdateAsync(Pessoa pessoa);
        Task<bool> Delete(int id);
    }
}

using Microsoft.Data.SqlClient;
using Dapper;
using Teste_UxComex.Models;
using Teste_UxComex.Repositories.Interfaces;

namespace Teste_UxComex.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly string _connectionString;

       
        public PessoaRepository(IConfiguration configuration)
        {
            
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        
        public async Task<List<Pessoa>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var pessoas = await connection.QueryAsync<Pessoa>("SELECT * FROM Pessoas");
                return pessoas?.ToList() ?? new List<Pessoa>();
            }
        }

        
        public async Task<Pessoa> GetAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Pessoa>(
                    "SELECT * FROM Pessoas WHERE PessoaId = @PessoaId",
                    new { PessoaId = id }
                ) ?? new Pessoa();
            }
        }

        
        public async Task<bool> AddAsync(Pessoa pessoa)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.ExecuteAsync(
                    "INSERT INTO Pessoas (Nome, Telefone, CPF) VALUES (@Nome, @Telefone, @CPF)",
                    pessoa
                );
                return result > 0;
            }
        }

        
        public async Task<bool> UpdateAsync(Pessoa pessoa)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.ExecuteAsync(
                    "UPDATE Pessoas SET Nome = @Nome, Telefone = @Telefone, CPF = @CPF WHERE PessoaId = @PessoaId",
                    pessoa
                );
                return result > 0;
            }
        }

       
        public async Task<bool> Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.ExecuteAsync(
                    "DELETE FROM Pessoas WHERE PessoaId = @PessoaId",
                    new { PessoaId = id }
                );
                return result > 0;
            }
        }
    }
}

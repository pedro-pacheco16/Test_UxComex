using Microsoft.Data.SqlClient;
using Dapper;
using Teste_UxComex.Models;
using Teste_UxComex.Repositories.Interfaces;

namespace Teste_UxComex.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly string _connectionString;

        public EnderecoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Endereco>> GetByPessoaIdAsync(int pessoaId)
        {
            var query = @"
                SELECT EnderecoId, PessoaId, EnderecoCompleto, CEP, Cidade, Estado
                FROM Enderecos
                WHERE PessoaId = @PessoaId";

            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Endereco>(query, new { PessoaId = pessoaId });
        }

        public async Task<Endereco?> GetByIdAsync(int enderecoId)
        {
            var query = @"
                SELECT EnderecoId, PessoaId, EnderecoCompleto, CEP, Cidade, Estado
                FROM Enderecos
                WHERE EnderecoId = @EnderecoId";

            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Endereco>(query, new { EnderecoId = enderecoId });
        }

        public async Task<bool> AddAsync(Endereco endereco)
        {
            try
            {
                var query = @"
            INSERT INTO Enderecos (PessoaId, EnderecoCompleto, CEP, Cidade, Estado)
            OUTPUT INSERTED.EnderecoId
            VALUES (@PessoaId, @EnderecoCompleto, @CEP, @Cidade, @Estado)";

                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                var enderecoId = await connection.ExecuteScalarAsync<bool>(query, endereco);

                Console.WriteLine($"EnderecoId retornado: {enderecoId}");

                return enderecoId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao inserir endereço: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Endereco endereco)
        {
            var query = @"
                UPDATE Enderecos
                SET 
                    EnderecoCompleto = @EnderecoCompleto,
                    CEP = @CEP,
                    Cidade = @Cidade,
                    Estado = @Estado
                WHERE EnderecoId = @EnderecoId";

            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(query, endereco);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int enderecoId)
        {
            var query = @"
                DELETE FROM Enderecos
                WHERE EnderecoId = @EnderecoId";

            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(query, new { EnderecoId = enderecoId });
            return result > 0;
        }
    }

}



using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

namespace ScreenSound.Banco
{
    internal class ArtistaDAL
    {
        public IEnumerable<Artista> Listar()
        {
            var lista = new List<Artista>();

            using var conexao = new Connection().ObterConexao();
            conexao.Open();
            var sql = "SELECT * FROM Artistas";
            using var comando = new SqlCommand(sql, conexao);
            using SqlDataReader sqlDataReader = comando.ExecuteReader();

            while (sqlDataReader.Read())
            {
                var artista = new Artista(
                    sqlDataReader["Nome"].ToString()!,
                    sqlDataReader["Bio"].ToString()!
                );
                artista.Id = (int)sqlDataReader["Id"];
                artista.FotoPerfil = sqlDataReader["FotoPerfil"].ToString()!;
                lista.Add(artista);
            }

            return lista;
        }

        public void Adicionar(Artista artista)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @perfilPadrao, @bio)";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@nome", artista.Nome);
            command.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
            command.Parameters.AddWithValue("@bio", artista.Bio);

            int retorno = command.ExecuteNonQuery();

            Console.WriteLine($"{retorno} linha(s) inserida(s).");
        }

        public void Atualizar(Artista artista)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE Artistas SET Nome = @nome, Bio = @bio WHERE Id = @id";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@nome", artista.Nome);
            command.Parameters.AddWithValue("@bio", artista.Bio);
            command.Parameters.AddWithValue("@id", artista.Id);

            int retorno = command.ExecuteNonQuery();

            Console.WriteLine($"Linhas afetadas: {retorno}");
        }

        public void Deletar(Artista artista)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM Artistas WHERE Id = @id";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@id", artista.Id);

            int retorno = command.ExecuteNonQuery();

            Console.WriteLine($"Linhas afetadas: {retorno}");
        }
    }
}

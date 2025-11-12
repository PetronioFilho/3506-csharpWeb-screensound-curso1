using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

namespace ScreenSound.Banco
{
    internal class Connection
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ScreenSound;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public SqlConnection ObterConexao()
        {
            var conn = new SqlConnection(connectionString);
            return conn;
        }

        public IEnumerable<Artista> Listar()
        {
            var lista = new List<Artista>();

            using var conexao = ObterConexao();
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
    }
}

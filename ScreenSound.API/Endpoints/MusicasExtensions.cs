using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class MusicasExtensions
    {
        public static void AddEndpointMusicas(this WebApplication app)
        {
            #region Músicas

            app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
            {
                var musicas = dal.Listar();
                return Results.Ok(musicas);
            });

            app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
            {
                var musica = dal.RecuperarPor(m => m.Nome.ToUpper() == nome.ToUpper());
                if (musica is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(musica);
            });

            app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, DAL<Artista> dalArtista, MusicaRequest musicaRequest) =>
            {                
                var musica = new Musica(musicaRequest.nome)
                {
                    AnoLancamento = musicaRequest.anoLancamento,
                    ArtistaId = musicaRequest.artistaId
                };  

                dal.Adicionar(musica);
                return Results.Ok();
            });

            app.MapPut("/Musicas/{musica}", ([FromServices] DAL<Musica> dal, [FromBody] Musica musica) =>
            {
                var musicaArtualizar = dal.RecuperarPor(m => m.Id == musica.Id);
                if (musicaArtualizar is null)
                {
                    return Results.NotFound();
                }

                musicaArtualizar.Nome = musica.Nome;
                musicaArtualizar.AnoLancamento = musica.AnoLancamento;
                dal.Atualizar(musicaArtualizar);

                return Results.Ok();
            });

            app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) =>
            {
                var musica = dal.RecuperarPor(m => m.Id == id);
                if (musica is null)
                {
                    return Results.NotFound();
                }
                dal.Deletar(musica);
                return Results.NoContent();
            });


            #endregion

        }
    }
}

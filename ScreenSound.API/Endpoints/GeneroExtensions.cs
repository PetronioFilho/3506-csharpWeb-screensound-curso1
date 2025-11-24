using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.Banco;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class GeneroExtensions
    {
        public static void AddEndpoinGeneros(this WebApplication app)
        {
            app.MapGet("/Generos", ([FromServices] DAL<Genero> dal) => 
            {
                var generos = dal.Listar();
                return Results.Ok(generos);
            });

            app.MapGet("/Generos/{nome}", ([FromServices] DAL<Genero> dal, string nome) => 
            {
                var genero = dal.RecuperarPor(g => g.Nome.ToUpper() == nome.ToUpper());
                if (genero is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(genero);
            });

            app.MapPost("/Generos", ([FromServices] DAL<Genero> dal, GeneroRequest generoRequest) => 
            {
                var genero = new Genero()
                {
                    Nome = generoRequest.nome,
                    Descricao = generoRequest.descricao
                };
                dal.Adicionar(genero);
                return Results.Ok();
            });

            app.MapPut("/Generos", ([FromServices] DAL<Genero> dal, [FromBody] Genero genero) => 
            {
                var generoAtualizar = dal.RecuperarPor(g => g.Id == genero.Id);
                if (generoAtualizar is null)
                {
                    return Results.NotFound();
                }
                generoAtualizar.Nome = genero.Nome;
                generoAtualizar.Descricao = genero.Descricao;
                dal.Atualizar(generoAtualizar);
                return Results.Ok();
            });

            app.MapDelete("/Generos/{id}", ([FromServices] DAL<Genero> dal, int id) => 
            {
                var generoDeletar = dal.RecuperarPor(g => g.Id == id);
                if (generoDeletar is null)
                {
                    return Results.NotFound();
                }
                dal.Deletar(generoDeletar);
                return Results.Ok();
            });
        }
    }
}

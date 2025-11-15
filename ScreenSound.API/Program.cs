using ScreenSound.Banco;
using ScreenSound.Modelos;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();


// REMOVA a configuração do System.Text.Json e use APENAS isto:
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
});

var app = builder.Build();

#region Artistas

app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
{
    var artistas = dal.Listar();
    return Results.Ok(artistas);
});

app.MapGet("/Artistas/{nome}", ([FromServices] DAL<Artista> dal, string nome) =>
{
    var artista = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
    if (artista is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(artista);
});

app.MapPost("/Artistas", ([FromServices] DAL<Artista> dal, Artista artista) => {

    dal.Adicionar(artista);
    return Results.Ok();
});

app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
{
    var artista = dal.RecuperarPor(a => a.Id == id);
    if (artista is null)
    {
        return Results.NotFound();
    }

    dal.Deletar(artista);
    return Results.NoContent();
});

app.MapPut("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] Artista artista) =>
{
    var artistaAtualizar = dal.RecuperarPor(a => a.Id == artista.Id);
    if (artistaAtualizar is null)
    {
        return Results.NotFound();
    }

    artistaAtualizar.Nome = artista.Nome;
    artistaAtualizar.Bio = artista.Bio;
    artistaAtualizar.FotoPerfil = artista.FotoPerfil;

    dal.Atualizar(artistaAtualizar);
    return Results.Ok();

});

#endregion

#region Músicas

app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) => 
{
    var musicas = dal.Listar();
    return Results.Ok(musicas);
});

app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) => 
{
    var musica = dal.RecuperarPor(m => m.Nome.ToUpper() == nome.ToUpper());
    if(musica is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(musica);
});

app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, Musica musica) =>
{
    dal.Adicionar(musica);
    return Results.Ok();
});

app.MapPut("/Musicas/{musica}", ([FromServices] DAL<Musica> dal, [FromBody] Musica musica) => 
{
    var musicaArtualizar = dal.RecuperarPor(m => m.Id == musica.Id);
    if(musicaArtualizar is null)
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


app.Run();
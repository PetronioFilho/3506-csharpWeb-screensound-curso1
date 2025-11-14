using ScreenSound.Banco;
using ScreenSound.Modelos;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// REMOVA a configuração do System.Text.Json e use APENAS isto:
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
});

var app = builder.Build();

app.MapGet("/Artistas", () =>
{
    var dal = new DAL<Artista>(new ScreenSoundContext());
    var artistas = dal.Listar();
    return Results.Ok(artistas);
});

app.MapGet("/Artistas/{nome}", (string nome) =>
{
    var dal = new DAL<Artista>(new ScreenSoundContext());
    var artista = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
    if (artista is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(artista);
});

app.Run();
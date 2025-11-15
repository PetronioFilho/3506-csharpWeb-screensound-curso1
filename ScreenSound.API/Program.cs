using ScreenSound.Banco;
using ScreenSound.Modelos;
using Newtonsoft.Json;
using ScreenSound.API.Endpoints;

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

app.AddEndpointsArtistas();
app.AddEndpointMusicas();

app.Run();
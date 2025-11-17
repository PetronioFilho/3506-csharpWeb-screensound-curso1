using ScreenSound.Banco;
using ScreenSound.Modelos;
using Newtonsoft.Json;
using ScreenSound.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
});

var app = builder.Build();

app.AddEndpointsArtistas();
app.AddEndpointMusicas();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
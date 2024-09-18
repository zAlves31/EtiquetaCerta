using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using webApi.EtiquetaCerta.Contexts;
using webApi.EtiquetaCerta.Interfaces;
using webApi.EtiquetaCerta.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });


// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar DbContext com a string de conexão do appsettings.json
builder.Services.AddDbContext<EtiquetaCertaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Server=DESKTOP-O595EET;database=EtiquetaCerta;user id=sa; Pwd=Senai@134;Trustservercertificate=true")));

// Configurar Repositórios
builder.Services.AddScoped<ILegislationRepository, LegislationRepository>();
builder.Services.AddScoped<ISymbologyRepository, SymbologyRepository>();
builder.Services.AddScoped<IProcessInLegislationRepository, ProcessInLegislationRepository>();
builder.Services.AddScoped<ISymbologyTranslateRepository, SymbologyTranslateRepository>();
builder.Services.AddScoped<IConservationProcessRepository, ConservationProcessRepository>();
builder.Services.AddScoped<ILabelRepository, LabelRepository>();
builder.Services.AddScoped<ILegislationRepository, LegislationRepository>();
builder.Services.AddScoped<ILabelSymbologyRepository, LabelSymbologyRepository>();


// Configurar CORS se necessário
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure o pipeline de requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAll"); // Adicione o middleware CORS
app.MapControllers();
app.Run();

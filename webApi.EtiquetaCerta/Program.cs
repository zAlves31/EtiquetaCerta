using Microsoft.EntityFrameworkCore;
using webApi.EtiquetaCerta.Contexts;
using webApi.EtiquetaCerta.Interfaces;
using webApi.EtiquetaCerta.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar DbContext com a string de conexão do appsettings.json
builder.Services.AddDbContext<EtiquetaCertaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Server=DESKTOP-O595EET;database=EtiquetaCerta;user id=sa; Pwd=Senai@134;Trustservercertificate=true")));

// Configurar Repositórios
builder.Services.AddScoped<ILegislationRepository, LegislationRepository>();
builder.Services.AddScoped<ISymbologyRepository, SymbologyRepository>();

var app = builder.Build();

// Configure o pipeline de requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

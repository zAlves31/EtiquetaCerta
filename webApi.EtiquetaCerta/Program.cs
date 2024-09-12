using Microsoft.EntityFrameworkCore;
using webApi.EtiquetaCerta.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EtiquetaCertaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Data Source=DESKTOP-O595EET; Initial Catalog=Voluntee; User Id = sa; Password = Senai@134; TrustServerCertificate=true;")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using DeslocamentoApp.Data.Context;
using DeslocamentoApp.Data.Repository;
using DeslocamentoApp.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

///////// aqui inicia o que foi adicionado para o projeto
////adiciona o contexto aos serviços       
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ApplicationDbContext"),
        b => b.MigrationsAssembly("DeslocamentoApp.Data"));
});

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

var appAssemblie = typeof(
    DeslocamentoAPI.Application.CarrosCommands.CadastrarCarroCommand)
    .Assembly;
builder.Services.AddMediatR(appAssemblie);

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

///////////////// fim

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


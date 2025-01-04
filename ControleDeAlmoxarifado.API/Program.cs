using ControleDeAlmoxarifado.API.Model;
using ControleDeAlmoxarifado.API.Services.Repositories.Implements;
using ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar a string de conexão para uso com Dapper e MySQL
builder.Services.AddSingleton<IDbConnection>((sp) => new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection2")));

// Registrar o repositório genérico
builder.Services.AddScoped(typeof(IRepository<Categoria>), typeof(CategoriaRepository));
builder.Services.AddScoped(typeof(IRepository<Funcionario>), typeof(FuncionarioRepository));
builder.Services.AddScoped(typeof(IRepository<Fornecedor>), typeof(FornecedorRepository));
builder.Services.AddScoped(typeof(IRepository<Produto>), typeof(ProdutoRepository));
builder.Services.AddScoped(typeof(IRepository<Entrada>), typeof(EntradaRepository));
builder.Services.AddScoped(typeof(IRepository<Saida>), typeof(SaidaRepository));
builder.Services.AddScoped(typeof(ITransacoesRepository<Saida>), typeof(SaidaRepository));
builder.Services.AddScoped(typeof(ITransacoesRepository<Entrada>), typeof(EntradaRepository));
builder.Services.AddScoped(typeof(IUserRepository<User>), typeof(UserRepository));


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

using LojaApi.Repositorys;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Realiza a leitura da conexão com o banco

builder.Services.AddSingleton<UsuarioReposirys>(provider => new UsuarioReposirys(connectionString));
builder.Services.AddSingleton<ProdutosRepositorys>(provider => new ProdutosRepositorys(connectionString));
builder.Services.AddSingleton<GerenciamentoRepository>(provider => new GerenciamentoRepository(connectionString));
builder.Services.AddSingleton<CarrinhoRepositorys>(provider => new CarrinhoRepositorys(connectionString));


//Swagger Parte 1
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();

//Swagger Parte 2
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crud Pessoa V1");
        c.RoutePrefix = string.Empty;
    });
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

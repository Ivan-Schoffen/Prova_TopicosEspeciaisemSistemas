using Roger.Data;
using Roger.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();

app.MapGet("/", () => "API para gerenciamento de biblioteca!");

// app.MapPost("/api/livro/cadastrar", ([FromBody] Livro livro) =>
// {

// });

app.Run();

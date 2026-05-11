using Microsoft.AspNetCore.Mvc;
using Roger.Data;
using Roger.Models;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

app.MapGet("/", () => "API para gerenciamento de biblioteca!");

app.MapPost("/api/livro/cadastrar", ([FromBody] Livro livro, [FromServices] AppDbContext ctx) =>
{
    Livro? cadastrado = ctx.livros.FirstOrDefault(x => x.Nome == livro.Nome);

    if (cadastrado is not null)
    {
        return Results.Conflict("Este livro ja esta cadastrado!");
    }

    ctx.livros.Add(livro);
    ctx.SaveChanges();
    return Results.Ok("Livro cadastrado com sucesso!");
});

app.MapGet("/api/livro/listar", ([FromServices] AppDbContext ctx) =>
{
    if (ctx.livros.Any())
    {
        return Results.Ok(ctx.livros.ToList());
    }

    return Results.NotFound("Não nenhum livro cadastrado!");
});

app.MapGet("/api/livro/buscar/{nome}", ([FromRoute] string nome, [FromServices] AppDbContext ctx) =>
{
    Livro? buscador = ctx.livros.FirstOrDefault(x => x.Nome == nome);

    if (buscador is not null)
    {
        return Results.Ok(buscador);
    }

    return Results.NotFound("Livro não encontrado!");
});

app.MapPost("/api/livro/emprestar/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    Livro? emprestado = ctx.livros.Find(id);

    if (emprestado is not null)
    {
        if (emprestado.Disponivel == true)
        {
            emprestado.Disponivel = false;
            ctx.livros.Update(emprestado);
            ctx.SaveChanges();
            return Results.Ok("Livro emprestado com sucesso.");
        }

        return Results.Conflict("O livro não esta disponivel!");
    }

    return Results.NotFound("Livro não encontrado!");
});

app.MapPost("/api/livro/devolver/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    Livro? emprestado = ctx.livros.Find(id);

    if (emprestado is not null)
    {
        if (emprestado.Disponivel == false)
        {
            emprestado.Disponivel = true;
            ctx.livros.Update(emprestado);
            ctx.SaveChanges();
            return Results.Ok("Livro devolvido com sucesso.");
        }

        return Results.Conflict("O livro não esta emprestado!");
    }

    return Results.NotFound("Livro não encontrado!");
});

app.MapGet("/api/livro/disponiveis", ([FromServices] AppDbContext ctx) =>
{
    if (ctx.livros.Any())
    {
        List<Livro> Disponiveis = new List<Livro>();
        foreach (Livro livro in ctx.livros)
        {
            if (livro.Disponivel == true)
            {
                Disponiveis.Add(livro);
            }
        }

        if(Disponiveis.Any())
        {
            return Results.Ok(Disponiveis);
        }
    }

    return Results.NotFound("Não nenhum livro cadastrado!");
});

app.MapGet("/api/livro/emprestados", ([FromServices] AppDbContext ctx) =>
{
    if (ctx.livros.Any())
    {
        List<Livro> Emprestados = new List<Livro>();
        foreach (Livro livro in ctx.livros)
        {
            if (livro.Disponivel == false)
            {
                Emprestados.Add(livro);
            }
        }

        if(Emprestados.Any())
        {
            return Results.Ok(Emprestados);
        }
    }

    return Results.NotFound("Não nenhum livro cadastrado!");
});

app.Run();

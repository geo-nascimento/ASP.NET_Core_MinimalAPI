using Microsoft.EntityFrameworkCore;
using MinCatalogoAPI.Database;
using MinCatalogoAPI.Models;

namespace MinCatalogoAPI.ApiEndpoints;

public static class ProtudosEndpoints
{
    public static void MapProtudosEndpoints(this WebApplication app)
    {
        app.MapPost("/produtos", async (Produto produto, CatalogoContext db) =>
        {
            //Verificar se a categoria é nova ou já está cadastrada
            var categoriaDB = await db.Categorias!.FindAsync(produto.CategoriaId);
            if (categoriaDB is null) return Results.BadRequest();

            db.Add(produto);
            await db.SaveChangesAsync();

            return Results.Created($"/produtos/{produto.ProdutoId}", produto);
        });

        app.MapGet("/produtos/{id:int}", async (int id, CatalogoContext db) =>
        {
            var produto = await db.Produtos!.Include(c => c.Categoria).FirstOrDefaultAsync(a => a.ProdutoId == id);

            if (produto is null) return Results.NotFound();

            return Results.Ok(produto);
        });

        app.MapGet("/produtos", async (CatalogoContext db) =>
        {
            return await db.Produtos!.ToListAsync();
        }).RequireAuthorization();

        app.MapPut("/produtos/{id:int}", async (Produto produto, int id, CatalogoContext db) =>
        {
            if (produto.ProdutoId != id) return Results.BadRequest();

            var produtoDB = await db.Produtos!.FindAsync(id);
            if (produtoDB is null) return Results.NotFound();

            produtoDB.Nome = produto.Nome;
            produtoDB.Descricao = produto.Descricao;
            produtoDB.Preco = produto.Preco;
            produtoDB.Estoque = produto.Estoque;
            produtoDB.Imagem = produto.Imagem;

            await db.SaveChangesAsync();

            return Results.Ok(produtoDB);
        });

        app.MapDelete("/produtos/{id:int}", async (int id, CatalogoContext db) =>
        {
            var produto = await db.Produtos!.FindAsync(id);
            if (produto is null) return Results.NotFound();

            db.Produtos.Remove(produto);
            await db.SaveChangesAsync();

            return Results.Ok();
        });
    }
}

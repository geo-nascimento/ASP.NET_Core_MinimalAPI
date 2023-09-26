using Microsoft.EntityFrameworkCore;
using MinCatalogoAPI.Database;
using MinCatalogoAPI.Models;

namespace MinCatalogoAPI.ApiEndpoints;

public static class CategoriasEndpoints
{
    public static void MapCategoriasEndpoints(this WebApplication app)
    {
        app.MapPost($"/categorias", async (Categoria categoria, CatalogoContext db) =>
        {
            db.Add(categoria);
            await db.SaveChangesAsync();

            return Results.Created($"/categorias/{categoria.CategoriaId}", categoria);
        });

        app.MapGet("/categorias", async (CatalogoContext db) => await db.Categorias!.ToListAsync()).RequireAuthorization();

        app.MapGet("/categorias/{id:int}", async (CatalogoContext db, int id) =>
        {
            return await db.Categorias!.FindAsync(id) is Categoria categoria ? Results.Ok(categoria) : Results.NotFound();
        });

        app.MapPut("/categorias/{id:int}", async (CatalogoContext db, Categoria categoria, int id) =>
        {
            if (categoria.CategoriaId != id)
            {
                Results.BadRequest("Entrada de dados inválida");
            }

            var categoriaDB = await db.Categorias!.FindAsync(id);

            if (categoriaDB != null) return Results.NotFound("Categoria não encontrada");

            categoriaDB!.Nome = categoria.Nome;
            categoriaDB.Descricao = categoria.Descricao;

            await db.SaveChangesAsync();

            return Results.Ok(categoria);
        });

        app.MapDelete("/categorias/{id:int}", async (int id, CatalogoContext db) =>
        {
            var categoria = await db.Categorias!.FindAsync(id);

            if (categoria is null) return Results.NotFound();

            db.Categorias.Remove(categoria);
            await db.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}

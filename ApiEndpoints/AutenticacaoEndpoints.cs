using Microsoft.AspNetCore.Authorization;
using MinCatalogoAPI.Models;
using MinCatalogoAPI.Services;

namespace MinCatalogoAPI.ApiEndpoints;

public static class AutenticacaoEndpoints
{
    public static void MapAutenticacaoEndPoints(this WebApplication app)
    {
        //Endpoint para login
        app.MapPost("/login", [AllowAnonymous] (UserModel userModel, ITokenService tokenService) =>
        {
            if (userModel == null) return Results.BadRequest("Login Inválido");

            if (userModel.UserName == "George" && userModel.Password == "32133928")
            {
                var tokenString = tokenService.GerarToken(app.Configuration["Jwt:Key"],
                    app.Configuration["Jwt:Issuer"],
                    app.Configuration["Jwt:Audience"],
                    userModel);

                return Results.Ok(new { token = tokenString });
            }
            else
            {
                return Results.BadRequest("Login inválido");
            }
        }).Produces(StatusCodes.Status400BadRequest).Produces(StatusCodes.Status200OK).WithName("Login").WithTags("Autenticação");
    }
}

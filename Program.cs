
using MinCatalogoAPI.ApiEndpoints;
using MinCatalogoAPI.AppServicesExtensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddPersistence();
builder.Services.AddCors();
builder.AddAuthenticationJwt();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


//Chamar a autenticação
app.MapAutenticacaoEndPoints();

//EndPoints Categorias
app.MapCategoriasEndpoints();

//EndPoints Produtos
app.MapProtudosEndpoints();

//configurar ambiente
var environment = app.Environment;
app.UseExecptionHandling(environment).UseSwaggerMiddleware().UseAppCors();

//devem estar aqui antes do app.Run() e exatamente nessa ordem!!!!
app.UseAuthentication();
app.UseAuthorization();

app.Run();


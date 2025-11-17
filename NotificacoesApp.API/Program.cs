using NotificacoesApp.API.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer(); //Swagger
builder.Services.AddSwaggerGen(); //Swagger

//Registrar a classe de serviço:
builder.Services.AddSingleton<MovimentacaoService>();

var app = builder.Build();

app.MapOpenApi(); //Mapeamento da API

app.UseSwagger(); //Swagger
app.UseSwaggerUI(); //Swagger

app.MapScalarApiReference(s => s.WithTheme(ScalarTheme.BluePlanet)); //Scalar

/* ENDPOINT POST */
app.MapPost("/notificacoes/movimentacoes", async
    (NotificacaoMovimentacaoRequest request, MovimentacaoService service) =>
{
    await service.RegistrarMovimentacao(request);

    return Results.Ok(new { Message = "Notificação de movimentação recebida com sucesso!" });
});

app.Run();

/* RECORD (registro) para receber o POST da movimentação */
public record NotificacaoMovimentacaoRequest(
    string Id,    //Id da movimentação
    string Nome, //Nome da movimentação
    string Data, //Data da movimentação
    decimal Valor, //Valor da movimentação
    string CategoriaId, //Id da categoria
    int Tipo //Tipo da movimentação
);




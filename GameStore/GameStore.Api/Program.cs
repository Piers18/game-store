using GameStore.Api.Dtos;
using GameStore.Api.Endpoints;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var app = builder.Build();

GamesEndpoints.MapGamesEndpoints(app);

app.Run();

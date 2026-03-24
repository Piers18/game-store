using GameStore.Api.Dtos;
using GameStore.Api.Endpoints;
using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var connectionString = "Server=localhost;Database=GameStoreDb;User Id=sa;Password=Krypton#4826!;TrustServerCertificate=True";
builder.Services.AddDbContext<GameStoreContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

GamesEndpoints.MapGamesEndpoints(app);

app.Run();

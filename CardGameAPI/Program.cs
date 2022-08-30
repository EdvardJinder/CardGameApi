
using CardGameAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CardGameAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CardGameAPIContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("CardGameAPIContext") ?? throw new InvalidOperationException("Connection string 'CardGameAPIContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IGameSession, GameSession>();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy((p) =>
    {
        p.AllowAnyHeader();
        p.AllowAnyOrigin();
        p.AllowAnyMethod();
    });
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TodoDbContext>(opt =>
{
    Console.WriteLine(builder.Configuration.ToString());
    opt.UseNpgsql($"User ID={builder.Configuration["DatabaseConfig:POSTGRES_USER"]};" +
                  $"Password={builder.Configuration["DatabaseConfig:POSTGRES_PASSWORD"]};" +
                  $"Host=localhost;" +
                  $"Port=5432;" +
                  $"Database={builder.Configuration["DatabaseConfig:POSTGRES_DB"]};" +
                  $"Pooling=true;" +
                  $"Min Pool Size=0;" +
                  $"Max Pool Size=100;" +
                  $"Connection Lifetime=0;"
                  );
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
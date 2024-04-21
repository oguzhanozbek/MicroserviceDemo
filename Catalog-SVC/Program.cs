using Catalog_SVC.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMassTransit(x=>{
    x.UsingRabbitMq((content,cfg)=>{
        cfg.ConfigureEndpoints(content);
    });

});


builder.Services.AddDbContext<CatalogDbContext>(opt => 
{
    opt.UseNpgsql("Server=localhost:5432;User Id=postgres;Password=postgrespwd;Database=catalogDb;");
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

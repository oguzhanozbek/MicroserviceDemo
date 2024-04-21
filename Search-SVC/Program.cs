using MassTransit;
using MongoDB.Driver;
using MongoDB.Entities;
using Search_SVC.Consumers;
using Search_SVC.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddMassTransit(x=>
{
    x.AddConsumersFromNamespaceContaining<ItemCreatedConsumer>();
    x.UsingRabbitMq((content,cfg)=>
    {
        cfg.ConfigureEndpoints(content);
    });

});


var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await DB.InitAsync("searchDb", MongoClientSettings.FromConnectionString("mongodb://root:mongopw@localhost"));

await DB.Index<CatalogItem>()
    .Key(x=>x.Name, KeyType.Text)
    .Key(x=>x.Price, KeyType.Text)
    .CreateAsync();

/**
await DB.SaveAsync(new CatalogItem 
{
    Name = "iPhone 15 pro max",
    Price = 2000,
});

await DB.SaveAsync(
    new List<CatalogItem> {
        new CatalogItem
            { 
                Name = "iPhone 14 pro max",
                Price = 1200,
            },
        new CatalogItem
            { 
                Name = "Samsung s24 Ultra",
                Price = 2300,
            },
    }
);
**/

app.Run();

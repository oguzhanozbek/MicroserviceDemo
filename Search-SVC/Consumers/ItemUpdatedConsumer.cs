using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using MongoDB.Driver;
using Search_SVC.Models;

namespace Search_SVC.Consumers
{
    public class ItemUpdatedConsumer : IConsumer<ItemUpdated>
    {
        public async Task Consume(ConsumeContext<ItemUpdated> context)
        {
            Console.WriteLine($"Item received to be updated: {context.Message.CatalogBrand.Name} {context.Message.Name} for ${context.Message.Price}");
            var filter = Builders<CatalogItem>.Filter.Eq(x=> x.OriginalId, context.Message.UpdId);

            var update = Builders<CatalogItem>.Update
            .Set(x=>x.Name, context.Message.Name)
            .Set(x=>x.Price, context.Message.Price)
            .Set(x=>x.AvailableStock, context.Message.AvailableStock)
            .Set(x=>x.CatalogBrand, context.Message.CatalogBrand);

            await DB.Collection<CatalogItem>().UpdateOneAsync(filter, update);
            Console.WriteLine($"It has been updated: {context.Message.UpdId}");
        }
    }
}
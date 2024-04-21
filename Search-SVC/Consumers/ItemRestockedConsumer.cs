using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Contracts;
using Search_SVC.Models;
using MongoDB.Entities;
using MongoDB.Driver;

namespace Search_SVC.Consumers
{
    public class ItemRestockedConsumer : IConsumer<ItemRestocked>
    {
        public async Task Consume(ConsumeContext<ItemRestocked> context)
        {
            Console.WriteLine($"Item restock count has been received: {context.Message.RestockCount}");

            var update = Builders<CatalogItem>.Update.Inc(x=> x.AvailableStock, context.Message.RestockCount);
            await DB.Collection<CatalogItem>().UpdateManyAsync(x=>true, update);

            Console.WriteLine($"Items have been restocked: {context.Message.RestockCount}");
        }
    }
}
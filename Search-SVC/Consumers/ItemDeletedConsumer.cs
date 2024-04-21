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
    public class ItemDeletedConsumer : IConsumer<ItemDeleted>
    {
        public async Task Consume(ConsumeContext<ItemDeleted> context)
        {
            var filter = Builders<CatalogItem>.Filter.Eq("OriginalId", context.Message.Id);
            var result = await DB.Collection<CatalogItem>().DeleteOneAsync(filter);
            Console.WriteLine($"It has been deleted: {context.Message.Id}");
        }
            
    }
}
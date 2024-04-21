using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using Search_SVC.Models;

namespace Search_SVC.Consumers
{
    public class ItemCreatedConsumer : IConsumer<ItemCreated>
    {
        public async Task Consume(ConsumeContext<ItemCreated> context)
        {
            Console.WriteLine($"Item received: {context.Message.CatalogBrand.Name} {context.Message.Name} for ${context.Message.Price}");
            await DB.SaveAsync(new CatalogItem 
            {
                OriginalId = context.Message.Id.ToString(),
                Name = context.Message.Name,
                Price = context.Message.Price,
                CatalogBrand = context.Message.CatalogBrand
            });
        }
    }
}
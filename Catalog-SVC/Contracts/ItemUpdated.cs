using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog_SVC.Models;

namespace Contracts
{
    public class ItemUpdated
    {
        public Guid Id { get; set; }
        public string UpdId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Brand CatalogBrand { get; set; }
        public string? PictureFileName { get; set; }
        public string? PictureUri { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        public bool OnReorder { get; set; } 
        
    }
}
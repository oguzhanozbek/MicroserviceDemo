using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog_SVC.Data;
using Catalog_SVC.Models;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog_SVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private CatalogDbContext _context;
        private IPublishEndpoint _publisher;
        public CatalogController(CatalogDbContext context, IPublishEndpoint publisher)
        {
            _context = context;
            _publisher = publisher;
        }

        //RETRIVE OPERATION (All) > GET > /api/catalog 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allitems = await _context.Items.ToListAsync();
            return Ok(allitems);
        }

        //RETRIEVE OPERATION (Single Row by Id) > GET > /api/catalog/getbyid/{id}
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var itembyid = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(itembyid);
        }

        //RETRIEVE OPERATION (Single Row by Name) > GET > /api/catalog/getbyname/{name}
        [HttpGet]
        [Route("[action]/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var itembyname = await _context.Items.FirstOrDefaultAsync(x => x.Name == name);
            return Ok(itembyname);
        }


        //CREATE OPERATION > POST > /api/catalog
        [HttpPost]
        public async Task<IActionResult> CreateItem(CatalogItem item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            await _publisher.Publish(new ItemCreated
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CatalogBrand = item.CatalogBrand
            });
            return Ok(item);
        }

        //DELETE OPERATION > DELETE > /api/catalog/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveItem(Guid id)
        {
            var itemInDb = await _context.Items.FindAsync(id);
            if (itemInDb == null)
                return NotFound();
            _context.Items.Remove(itemInDb);
            await _context.SaveChangesAsync();
            await _publisher.Publish(new ItemDeleted
            {
                Id = id.ToString(),
            });

            return Ok(new {Message = "Item deleted"});
        }
    }
}
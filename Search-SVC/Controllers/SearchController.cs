using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Entities;
using Search_SVC.Models;

namespace Search_SVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        //RETRIEVE OPERATION > GET > /api/search/searchTerm={searchTerm}
        [HttpGet("{searchTerm}")]
        public async Task<IActionResult> SearchItems(string searchTerm ="")
        {
            var query = DB.Find<CatalogItem>();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query.Match(Search.Full, searchTerm);
            }
            
            var result = await query.ExecuteAsync();
            return Ok(result);
        }

        //RETRIEVE OPERATION > GET > /api/search/getAvailableItems
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> getAvailableItems()
        {
            var filter = Builders<CatalogItem>.Filter.Gt("AvailableStock",0);
            List<CatalogItem> result = await DB.Collection<CatalogItem>().Find(filter).ToListAsync();
            return Ok(result);
        }

    }


}
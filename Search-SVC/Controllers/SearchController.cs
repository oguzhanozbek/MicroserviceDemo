using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using Search_SVC.Models;

namespace Search_SVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        [HttpGet]
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
    }
}
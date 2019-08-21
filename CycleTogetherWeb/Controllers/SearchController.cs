using CycleTogether.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebModels;

namespace CycleTogetherWeb.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IDataRetriever _search;

        public SearchController(IDataRetriever search)
        {
            _search = search;
        }

        [HttpGet("{input}")]
        public IEnumerable<RouteSearch> Search(string input)
        {
            return _search.Find(input);
        }

    }
}
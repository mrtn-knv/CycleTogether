using CycleTogether.Contracts;
using System.Collections.Generic;
using WebModels;
using System.Linq;
using System.Data;
using Lucene.Net.Store;
using Lucene.Net.Documents;
using System.IO;
using AutoMapper;

namespace CycleTogether.DataRetriever
{
    public  class SearchManager : ISearchManager

    {
        private readonly IRoutesCache _cache;
        private readonly IMapper _mapper;

        public SearchManager(IRoutesCache cache, IMapper mapper)
        {
            _cache = cache;
            _mapper = mapper;
        }

        public void CreateIndex()
        {

            LuceneSearch.AddUpdateLuceneIndex(_cache.All().Select(route => _mapper.Map<RouteSearch>(route)));          
        }

        public void AddRouteToIndex(Route route)
        {

            LuceneSearch.AddUpdateLuceneIndex(_mapper.Map<RouteSearch>(route));
        }

        public void RemoveFromIndex(Route route)
        {
           var toDelete = _cache.All().FirstOrDefault(r => r.Id == route.Id);

            if (toDelete != null)
            LuceneSearch.ClearLuceneIndexRecord(toDelete.Id.ToString());
        }

        public void UpdateIndex()
        {
            LuceneSearch.ClearLuceneIndex();
            LuceneSearch.AddUpdateLuceneIndex(_cache.All().Select(route => _mapper.Map<RouteSearch>(route)));
        }
    }
}

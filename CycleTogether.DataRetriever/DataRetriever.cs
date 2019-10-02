using CycleTogether.Contracts;
using System.Collections.Generic;
using System.Linq;
using WebModels;

namespace CycleTogether.DataRetriever
{
    public class DataRetriever : IDataRetriever
    {
        private readonly ISearchManager _manager;
        public DataRetriever(ISearchManager manager)
        {
            _manager = manager;
        }
        public IEnumerable<RouteView> Find(string input)
        {
            try
            {
                return LuceneSearch.Search(input).ToList();
            }
            catch (Lucene.Net.Store.NoSuchDirectoryException)
            {
                _manager.CreateIndex();
                return LuceneSearch.Search(input);
            }
            catch (System.IO.FileNotFoundException)
            {
                _manager.CreateIndex();
                return LuceneSearch.Search(input).ToList();
            }           
        }
    }
}

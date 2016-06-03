using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastico;
using Nest;
using Xunit;

namespace Elastikken.Tests 
{
    class SearchTest : IDisposable
    {
        Elastico.ElasticManager _manager = new Elastico.ElasticManager();
        [Fact]
        public void SearchInDa()
        {
            string searchword = "abe";
            int from = 0;
            string index = "da";
            string searchInBooks = "dan-sko-ret";
            _manager.EntrySearchByHeadWord(searchword, from, index, searchInBooks);
        }




        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

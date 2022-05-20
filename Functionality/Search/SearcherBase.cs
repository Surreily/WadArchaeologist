using Surreily.WadArchaeologist.Functionality.Context;
using Surreily.WadArchaeologist.Functionality.Model;

namespace Surreily.WadArchaeologist.Functionality.Search {
    public abstract class SearcherBase {
        private Wad _wad;
        private SearchOptions _options;

        public SearcherBase(Wad wad, SearchOptions options) {
            _wad = wad;
            _options = options;
        }

        protected Wad Wad => _wad;

        protected SearchOptions Options => _options;

        public abstract void Search();
    }
}

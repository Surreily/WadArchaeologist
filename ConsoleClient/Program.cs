using Surreily.WadArchaeologist.Functionality;
using Surreily.WadArchaeologist.Functionality.Context;
using Surreily.WadArchaeologist.Functionality.Model;
using Surreily.WadArchaeologist.Functionality.Search;

namespace Surreily.WadArchaeologist.ConsoleClient {
    public class Program {
        public static void Main(string[] args) {
            // Get search context from arguments.
            SearchOptions searchContext = GetSearchContext(args);

            // TODO: Don't use hardcoded WAD file!
            string wadFilePath = @"E:\Doom\IWADs\DOOM2.WAD";

            Wad wad = WadFactory.Create(wadFilePath);

            // TODO: This should not be done from the main method.
            SideSearcher searcher = new SideSearcher();
            searcher.Search(searchContext, wad);
        }

        private static SearchOptions GetSearchContext(string[] args) {
            // TODO: Actually use arguments here.
            return new SearchOptions {
                ShouldIgnoreDirectory = true,
                MinimumNumberOfSidesPerMap = 10,
            };
        }
    }
}

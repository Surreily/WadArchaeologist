using System;
using System.IO;
using Surreily.WadArchaeologist.Functionality;
using Surreily.WadArchaeologist.Functionality.Context;
using Surreily.WadArchaeologist.Functionality.Search;

namespace Surreily.WadArchaeologist.ConsoleClient {
    public class Program {
        public static void Main(string[] args) {
            // Get search context from arguments.
            SearchContext searchContext = GetSearchContext(args);

            // TODO: Don't use hardcoded WAD file!
            string wadFilePath = @"E:\Doom\IWADs\DOOM2.WAD";

            Wad wad = WadFactory.Create(searchContext, wadFilePath);

            // TODO: This should not be done from the main method.
            SideSearcher searcher = new SideSearcher();
            searcher.Search(searchContext, wad);
        }

        private static SearchContext GetSearchContext(string[] args) {
            // TODO: Actually use arguments here.
            return new SearchContext {
                ShouldIgnoreDirectory = true,
                MinimumNumberOfSidesPerMap = 10,
            };
        }
    }
}

using Surreily.WadArchaeologist.Functionality;
using Surreily.WadArchaeologist.Functionality.Context;
using Surreily.WadArchaeologist.Functionality.Model;
using Surreily.WadArchaeologist.Functionality.Search;

namespace Surreily.WadArchaeologist.ConsoleClient {
    public class Program {
        public static void Main(string[] args) {
            // Get search context from arguments.
            SearchOptions options = GetSearchContext(args);

            // TODO: Don't use hardcoded WAD file!
            string wadFilePath = @"E:\Doom\IWADs\DOOM2.WAD";

            Wad wad = WadFactory.Create(wadFilePath, options);

            // TODO: This should not be done from the main method.
            new SideSearcher(wad, options).Search();
            new SectorSearcher(wad, options).Search();
            new LineSearcher(wad, options).Search();
            ////new VertexSearcher().Search(options, wad);
            new ThingSearcher(wad, options).Search();
        }

        private static SearchOptions GetSearchContext(string[] args) {
            // TODO: Actually use arguments here.
            return new SearchOptions {
                ShouldIgnoreDirectory = true,
                MinimumLineCount = 30,
                MinimumSideCount = 20,
                MinimumNumberOfSectorsPerMap = 10,
            };
        }
    }
}

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
            new SideSearcher().Search(options, wad);
            new SectorSearcher().Search(options, wad);
        }

        private static SearchOptions GetSearchContext(string[] args) {
            // TODO: Actually use arguments here.
            return new SearchOptions {
                ShouldIgnoreDirectory = true,
                MinimumNumberOfSidesPerMap = 10,
                MinimumNumberOfSectorsPerMap = 10,
            };
        }
    }
}

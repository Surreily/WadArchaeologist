namespace Surreily.WadArchaeologist.Functionality.Context {
    public class SearchOptions {
        public bool ShouldIgnoreDirectory { get; set; }

        public int MinimumLineCount { get; set; }

        public int MinimumSideCount { get; set; }

        public int MinimumNumberOfSectorsPerMap { get; set; }
    }
}

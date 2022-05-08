using System.Collections.Generic;
using Surreily.WadArchaeologist.Model;

namespace Surreily.WadArchaeologist.Functionality.Model {
    public class Wad {
        public Wad(IWadData data) {
            Data = data;
        }

        public IWadData Data { get; }

        public List<WadDirectoryEntry> DirectoryEntries { get; set; }

        public List<DataRegion> UnallocatedRegions { get; set; }

        public List<List<Line>> LineLists { get; set; }

        public List<List<Side>> SideLists { get; set; }

        public List<List<Sector>> SectorLists { get; set; }
    }
}

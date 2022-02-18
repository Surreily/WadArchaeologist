using System.Collections.Generic;

namespace Surreily.WadArchaeologist.Functionality.DataRegions {
    public class EntireFileDataRegionInitializer : IDataRegionInitializer {
        public void Initialize(Wad wad) {
            wad.DataRegions = new List<DataRegion> {
                new DataRegion {
                    StartOffset = 0,
                    EndOffset = wad.Data.Length,
                },
            };
        }
    }
}

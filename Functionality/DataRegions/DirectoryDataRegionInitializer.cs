using System;
using System.Collections.Generic;
using System.Linq;

namespace Surreily.WadArchaeologist.Functionality.DataRegions {
    class DirectoryDataRegionInitializer : IDataRegionInitializer {
        public void Initialize(Wad wad) {
            // Read the WAD header to get directory information.
            int directoryLength = BitConverter.ToInt32(wad.Data, 4);
            int directoryPosition = BitConverter.ToInt32(wad.Data, 8);

            // Create data regions for each directory item.
            List<DataRegion> regions = new List<DataRegion>();

            for (int i = 0; i < directoryLength; i++) {
                int entryPosition = directoryPosition + (i * 16);
                int dataPosition = BitConverter.ToInt32(wad.Data, entryPosition);
                int dataLength = BitConverter.ToInt32(wad.Data, entryPosition + 4);

                regions.Add(new DataRegion {
                    StartOffset = dataPosition,
                    EndOffset = dataPosition + dataLength,
                });
            }

            regions = regions
                .OrderBy(r => r.StartOffset)
                .ToList();

            // TODO: Invert the regions (so we get the 'negative space') and add those to the WAD.
            throw new NotImplementedException();
        }
    }
}

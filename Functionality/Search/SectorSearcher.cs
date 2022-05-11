using System.Collections.Generic;
using System.Linq;
using Surreily.WadArchaeologist.Functionality.Context;
using Surreily.WadArchaeologist.Functionality.Helpers;
using Surreily.WadArchaeologist.Functionality.Model;

namespace Surreily.WadArchaeologist.Functionality.Search {
    public class SectorSearcher {
        public void Search(SearchOptions options, Wad wad) {
            wad.SectorLists = new List<List<Sector>>();

            foreach (DataRegion region in wad.UnallocatedRegions.ToList()) {
                int position = region.Position;

                while (position < (region.Position + region.Length) - 26) {
                    if (TryFindSectors(options, wad, position, out int newPosition)) {
                        WadHelper.MarkRegionAsAllocated(wad, position, newPosition - position);
                        position = newPosition;
                    } else {
                        position++;
                    }
                }
            }
        }

        private bool TryFindSectors(SearchOptions options, Wad wad, int position, out int newPosition) {
            // Attempt to find and create sectors until we hit an invalid one.
            List<Sector> sectors = new List<Sector>();
            int currentPosition = position;

            while (ValidationHelper.GetIsValidSector(wad, currentPosition)) {
                sectors.Add(new Sector {
                    FloorHeight = wad.Data.ReadShort(currentPosition),
                    CeilingHeight = wad.Data.ReadShort(currentPosition + 2),
                    FloorTextureName = wad.Data.ReadString(currentPosition + 4, 8),
                    CeilingTextureName = wad.Data.ReadString(currentPosition + 12, 8),
                    Brightness = wad.Data.ReadShort(currentPosition + 20),
                    Effect = wad.Data.ReadUnsignedShort(currentPosition + 22),
                    Tag = wad.Data.ReadUnsignedShort(currentPosition + 24),
                });

                currentPosition += 26;
            }

            // We must have at least N number of sectors.
            if (sectors.Count < 10) {
                newPosition = 0;
                return false;
            }

            // Validation passed.
            wad.SectorLists.Add(sectors);
            newPosition = currentPosition;
            return true;
        }

        
    }
}

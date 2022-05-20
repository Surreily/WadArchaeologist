using System.Collections.Generic;
using System.Linq;
using Surreily.WadArchaeologist.Functionality.Context;
using Surreily.WadArchaeologist.Functionality.Helpers;
using Surreily.WadArchaeologist.Functionality.Model;

namespace Surreily.WadArchaeologist.Functionality.Search {
    public class SectorSearcher : SearcherBase {
        public SectorSearcher(Wad wad, SearchOptions options)
            : base(wad, options) {
        }

        public override void Search() {
            Wad.SectorLists = new List<List<Sector>>();

            foreach (DataRegion region in Wad.UnallocatedRegions.ToList()) {
                int position = region.Position;
                int regionEnd = region.Position + region.Length;

                while (position < regionEnd - 26) {
                    if (TryFindSectors(position, regionEnd, out int newPosition)) {
                        WadHelper.MarkRegionAsAllocated(Wad, position, newPosition - position);
                        position = newPosition;
                    } else {
                        position++;
                    }
                }
            }
        }

        private bool TryFindSectors(int position, int regionEnd, out int newPosition) {
            // Attempt to find and create sectors until we hit an invalid one.
            List<Sector> sectors = new List<Sector>();
            int currentPosition = position;

            while (currentPosition < regionEnd - 26) {
                Sector sector = new Sector {
                    FloorHeight = Wad.Data.ReadShort(currentPosition),
                    CeilingHeight = Wad.Data.ReadShort(currentPosition + 2),
                    FloorTextureName = Wad.Data.ReadString(currentPosition + 4, 8),
                    CeilingTextureName = Wad.Data.ReadString(currentPosition + 12, 8),
                    Brightness = Wad.Data.ReadShort(currentPosition + 20),
                    Effect = Wad.Data.ReadUnsignedShort(currentPosition + 22),
                    Tag = Wad.Data.ReadUnsignedShort(currentPosition + 24),
                };

                if (!SectorHelper.GetIsValidSector(sector)) {
                    break;
                }

                currentPosition += 26;
                sectors.Add(sector);
            }

            // We must have at least N number of sectors.
            if (sectors.Count < 10) {
                newPosition = 0;
                return false;
            }

            // Validation passed.
            Wad.SectorLists.Add(sectors);
            newPosition = currentPosition;
            return true;
        }
    }
}

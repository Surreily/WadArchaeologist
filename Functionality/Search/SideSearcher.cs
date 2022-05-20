using System.Collections.Generic;
using System.Linq;
using Surreily.WadArchaeologist.Functionality.Context;
using Surreily.WadArchaeologist.Functionality.Helpers;
using Surreily.WadArchaeologist.Functionality.Model;
using Surreily.WadArchaeologist.Model;

namespace Surreily.WadArchaeologist.Functionality.Search {
    public class SideSearcher : SearcherBase {
        public SideSearcher(Wad wad, SearchOptions options)
            : base(wad, options) {
        }

        public override void Search() {
            Wad.SideLists = new List<List<Side>>();

            foreach (DataRegion region in Wad.UnallocatedRegions.ToList()) {
                int position = region.Position;
                int regionEnd = region.Position + region.Length;

                while (position < regionEnd - 30) {
                    if (TryFindSides(position, regionEnd, out int newPosition)) {
                        WadHelper.MarkRegionAsAllocated(Wad, position, newPosition - position);
                        position = newPosition;
                    } else {
                        position++;
                    }
                }
            }
        }

        private bool TryFindSides(int position, int regionEnd, out int newPosition) {
            // Attempt to find and create sides until we hit an invalid one.
            List<Side> sides = new List<Side>();
            int currentPosition = position;

            while (currentPosition < regionEnd - 30) {
                Side side = new Side {
                    OffsetX = Wad.Data.ReadShort(currentPosition),
                    OffsetY = Wad.Data.ReadShort(currentPosition + 2),
                    UpperTextureName = Wad.Data.ReadString(currentPosition + 4, 8),
                    LowerTextureName = Wad.Data.ReadString(currentPosition + 12, 8),
                    MiddleTextureName = Wad.Data.ReadString(currentPosition + 20, 8),
                    SectorId = Wad.Data.ReadUnsignedShort(currentPosition + 28),
                };

                if (!SideHelper.GetIsValidSide(side)) {
                    break;
                }

                sides.Add(side);
                currentPosition += 30;
            }

            // We must have at least N number of sides.
            if (sides.Count < Options.MinimumSideCount) {
                newPosition = 0;
                return false;
            }

            // We must have at least 1 entry of "-".
            if (sides.All(s => s.UpperTextureName != "-" || s.LowerTextureName != "-" || s.MiddleTextureName != "-")) {
                newPosition = 0;
                return false;
            }

            // Validation passed.
            Wad.SideLists.Add(sides);
            newPosition = currentPosition;
            return true;
        }
    }
}

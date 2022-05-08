using System.Collections.Generic;
using System.Linq;
using Surreily.WadArchaeologist.Functionality.Context;
using Surreily.WadArchaeologist.Functionality.Helpers;
using Surreily.WadArchaeologist.Functionality.Model;
using Surreily.WadArchaeologist.Model;

namespace Surreily.WadArchaeologist.Functionality.Search {
    public class SideSearcher {
        public void Search(SearchOptions search, Wad wad) {
            wad.SideLists = new List<List<Side>>();

            int position = 0;

            while (position < wad.Data.Length - 30) {
                if (TryFindSides(search, wad, position, out int newPosition)) {
                    position = newPosition;
                } else {
                    position++;
                }
            }
        }

        private bool TryFindSides(
            SearchOptions options, Wad wad, int position, out int newPosition) {

            // Attempt to find and create sides until we hit an invalid one.
            List<Side> sides = new List<Side>();
            int currentPosition = position;

            while (GetIsValidSide(wad, currentPosition)) {
                sides.Add(new Side {
                    OffsetX = wad.Data.ReadShort(currentPosition),
                    OffsetY = wad.Data.ReadShort(currentPosition + 2),
                    UpperTextureName = wad.Data.ReadString(currentPosition + 4, 8),
                    LowerTextureName = wad.Data.ReadString(currentPosition + 12, 8),
                    MiddleTextureName = wad.Data.ReadString(currentPosition + 20, 8),
                    SectorId = wad.Data.ReadUnsignedShort(currentPosition + 28),
                });

                currentPosition += 30;
            }

            // We must have at least N number of sides.
            if (sides.Count < options.MinimumNumberOfSidesPerMap) {
                newPosition = 0;
                return false;
            }

            // We must have at least 1 entry of "-".
            if (sides.All(s => s.UpperTextureName != "-" || s.LowerTextureName != "-" || s.MiddleTextureName != "-")) {
                newPosition = 0;
                return false;
            }

            // Validation passed.
            wad.SideLists.Add(sides);
            newPosition = currentPosition;
            return true;
        }

        private bool GetIsValidSide(Wad wad, int position) {
            return
                ValidationHelper.GetIsValidTextureName(wad, position + 4) &&
                ValidationHelper.GetIsValidTextureName(wad, position + 12) &&
                ValidationHelper.GetIsValidTextureName(wad, position + 20);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Surreily.WadArchaeologist.Functionality.Context;
using Surreily.WadArchaeologist.Model;

namespace Surreily.WadArchaeologist.Functionality.Search {
    public class SideSearcher {
        public void Search(SearchContext search, WadContext wad, byte[] data) {
            int position = 0;

            while (position < data.Length - 30) {
                if (TryFindSides(search, wad, data, position, out int newPosition)) {
                    position = newPosition;
                } else {
                    position++;
                }
            }
        }

        private bool TryFindSides(
            SearchContext search, WadContext wad, byte[] data, int position, out int newPosition) {

            // Attempt to find and create sides until we hit an invalid one.
            List<Side> sides = new List<Side>();
            int currentPosition = position;

            while (GetIsValidSide(data, currentPosition)) {
                sides.Add(new Side {
                    OffsetX = BitConverter.ToInt16(data, currentPosition),
                    OffsetY = BitConverter.ToInt16(data, currentPosition + 2),
                    UpperTextureName = Encoding.ASCII.GetString(data, currentPosition + 4, 8).Trim('\0'),
                    LowerTextureName = Encoding.ASCII.GetString(data, currentPosition + 12, 8).Trim('\0'),
                    MiddleTextureName = Encoding.ASCII.GetString(data, currentPosition + 20, 8).Trim('\0'),
                    SectorId = BitConverter.ToUInt16(data, currentPosition + 28),
                });

                currentPosition += 30;
            }

            // We must have at least N number of sides.
            if (sides.Count < search.MinimumNumberOfSidesPerMap) {
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

        private bool GetIsValidSide(byte[] data, int position) {
            return
                GetIsValidTextureName(data, position + 4) &&
                GetIsValidTextureName(data, position + 12) &&
                GetIsValidTextureName(data, position + 20);
        }

        private bool GetIsValidTextureName(byte[] data, int position) {
            int i = 1;

            // Check if this is the null texture name: "-".
            if (data[position] == '-') {
                while (i < 8) {
                    if (data[position + i] != '\0') {
                        return false;
                    }

                    i++;
                }

                return true;
            }

            // Validate the first character.
            if (!GetIsValidTextureNameByte(data[position])) {
                return false;
            }

            // Validate the remaining characters.
            while (i < 8) {
                if (!GetIsValidTextureNameByte(data[position + i])) {
                    break;
                }

                i++;
            }

            while (i < 8) {
                if (data[position + i] != '\0') {
                    return false;
                }

                i++;
            }

            return true;
        }

        private bool GetIsValidTextureNameByte(byte b) {
            return
                (b >= 'A' && b <= 'Z') ||
                (b >= 'a' && b <= 'z') ||
                (b >= '0' && b <= '9') ||
                b == '_';
        }
    }
}

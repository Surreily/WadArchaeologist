using System.Collections.Generic;
using System.Linq;
using Surreily.WadArchaeologist.Functionality.Context;
using Surreily.WadArchaeologist.Functionality.Helpers;
using Surreily.WadArchaeologist.Functionality.Model;
using Surreily.WadArchaeologist.Model;

namespace Surreily.WadArchaeologist.Functionality.Search {
    public class LineSearcher {
        public void Search(SearchOptions options, Wad wad) {
            List<int> sideCounts = wad.SideLists
                .Select(l => l.Count)
                .ToList();

            int minimumSideCount = sideCounts.Min();
            int maximumSideCount = sideCounts.Max();

            wad.LineLists = new List<List<Line>>();

            foreach (DataRegion region in wad.UnallocatedRegions) {
                int position = region.Position;

                while (position <= region.Length - 14) {
                    if (TryFindLine(options, wad, position, maximumSideCount, out int newPosition)) {
                        position = newPosition;
                    } else {
                        position++;
                    }
                }
            }
        }

        private bool TryFindLine(SearchOptions options, Wad wad, int position, int maximumSideCount, out int newPosition) {
            List<Line> lines = new List<Line>();
            int currentPosition = position;

            while (currentPosition <= wad.Data.Length - 14 && ValidationHelper.GetIsValidLine(wad, currentPosition, maximumSideCount)) {
                lines.Add(new Line {
                    StartVertexId = wad.Data.ReadUnsignedShort(currentPosition),
                    EndVertexId = wad.Data.ReadUnsignedShort(currentPosition + 2),
                    Flags = wad.Data.ReadUnsignedShort(currentPosition + 4),
                    Effect = wad.Data.ReadUnsignedShort(currentPosition + 6),
                    Tag = wad.Data.ReadUnsignedShort(currentPosition + 8),
                    RightSideId = wad.Data.ReadUnsignedShort(currentPosition + 10),
                    LeftSideId = wad.Data.ReadUnsignedShort(currentPosition + 12),
                });

                currentPosition += 14;
            }

            // We must have at least N number of lines.
            if (lines.Count < 30) {
                newPosition = 0;
                return false;
            }

            if (!lines.Any(l => l.RightSideId == 0xFFFF || l.LeftSideId == 0xFFFF)) {
                newPosition = 0;
                return false;
            }

            // Validation passed.
            wad.LineLists.Add(lines);
            newPosition = currentPosition;
            return true;
        }
    }
}

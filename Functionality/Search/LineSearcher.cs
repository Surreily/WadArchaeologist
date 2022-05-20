using System.Collections.Generic;
using System.Linq;
using Surreily.WadArchaeologist.Functionality.Context;
using Surreily.WadArchaeologist.Functionality.Helpers;
using Surreily.WadArchaeologist.Functionality.Model;
using Surreily.WadArchaeologist.Model;

namespace Surreily.WadArchaeologist.Functionality.Search {
    public class LineSearcher : SearcherBase {
        public LineSearcher(Wad wad, SearchOptions options)
            : base(wad, options) {
        }

        public override void Search() {
            List<int> sideCounts = Wad.SideLists
                .Select(l => l.Count)
                .ToList();

            int minimumSideCount = sideCounts.Min();
            int maximumSideCount = sideCounts.Max();

            Wad.LineLists = new List<List<Line>>();

            foreach (DataRegion region in Wad.UnallocatedRegions.ToList()) {
                int position = region.Position;
                int regionEnd = region.Position + region.Length;

                while (position < regionEnd - 14) {
                    if (TryFindLine(position, regionEnd, maximumSideCount, out int newPosition)) {
                        WadHelper.MarkRegionAsAllocated(Wad, position, newPosition - position);
                        position = newPosition;
                    } else {
                        position++;
                    }
                }
            }
        }

        private bool TryFindLine(int position, int regionEnd, int maximumSideCount, out int newPosition) {
            List<Line> lines = new List<Line>();
            int currentPosition = position;

            while (currentPosition < regionEnd - 14) {
                Line line = new Line {
                    StartVertexId = Wad.Data.ReadUnsignedShort(currentPosition),
                    EndVertexId = Wad.Data.ReadUnsignedShort(currentPosition + 2),
                    Flags = Wad.Data.ReadUnsignedShort(currentPosition + 4),
                    Effect = Wad.Data.ReadUnsignedShort(currentPosition + 6),
                    Tag = Wad.Data.ReadUnsignedShort(currentPosition + 8),
                    RightSideId = Wad.Data.ReadUnsignedShort(currentPosition + 10),
                    LeftSideId = Wad.Data.ReadUnsignedShort(currentPosition + 12),
                };

                if (!LineHelper.GetIsValidLine(line, maximumSideCount)) {
                    break;
                }

                lines.Add(line);
                currentPosition += 14;
            }

            if (lines.Count < Options.MinimumLineCount) {
                newPosition = 0;
                return false;
            }

            if (!lines.Any(l => l.RightSideId == 0xFFFF || l.LeftSideId == 0xFFFF)) {
                newPosition = 0;
                return false;
            }

            // Validation passed.
            Wad.LineLists.Add(lines);
            newPosition = currentPosition;
            return true;
        }
    }
}

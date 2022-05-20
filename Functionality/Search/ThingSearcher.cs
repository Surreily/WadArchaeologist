using System.Collections.Generic;
using System.Linq;
using Surreily.WadArchaeologist.Functionality.Context;
using Surreily.WadArchaeologist.Functionality.Helpers;
using Surreily.WadArchaeologist.Functionality.Model;

namespace Surreily.WadArchaeologist.Functionality.Search {
    public class ThingSearcher : SearcherBase {
        public ThingSearcher(Wad wad, SearchOptions options)
            : base(wad, options) {
        }

        public override void Search() {
            Wad.ThingLists = new List<List<Thing>>();

            foreach (DataRegion region in Wad.UnallocatedRegions.ToList()) {
                int position = region.Position;

                while (position < (region.Position + region.Length) - 10) {
                    if (TryFindThings(region, position, out int newPosition)) {
                        WadHelper.MarkRegionAsAllocated(Wad, position, newPosition - position);
                        position = newPosition;
                    } else {
                        position++;
                    }
                }
            }
        }

        private bool TryFindThings(DataRegion region, int position, out int newPosition) {
            List<Thing> things = new List<Thing>();
            int currentPosition = position;

            while (position < (region.Position + region.Length) - 10) {
                Thing thing = new Thing {
                    X = Wad.Data.ReadShort(currentPosition),
                    Y = Wad.Data.ReadShort(currentPosition + 2),
                    Angle = Wad.Data.ReadUnsignedShort(currentPosition + 4),
                    Type = Wad.Data.ReadUnsignedShort(currentPosition + 6),
                    Flags = Wad.Data.ReadUnsignedShort(currentPosition + 8),
                };

                if (!ThingHelper.GetIsValidThing(thing)) {
                    break;
                }

                things.Add(thing);
                currentPosition += 10;
            }

            // Must have at least N things.
            if (things.Count < 20) {
                newPosition = 0;
                return false;
            }

            // Things must contain at least one player start.
            if (!things.Any(t => t.Type == 1)) {
                newPosition = 0;
                return false;
            }

            // Things must be somewhat near each other.
            float averageDistance = MathHelper.GetAverageDistanceBetweenClosestPoints(things, 4);

            if (averageDistance < 32 || averageDistance > 512) {
                newPosition = 0;
                return false;
            }

            // Validation passed.
            Wad.ThingLists.Add(things);
            newPosition = currentPosition;
            return true;
        }
    }
}

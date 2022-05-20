using System;
using System.Collections.Generic;
using System.Linq;
using Surreily.WadArchaeologist.Functionality.Context;
using Surreily.WadArchaeologist.Functionality.Helpers;
using Surreily.WadArchaeologist.Functionality.Model;

namespace Surreily.WadArchaeologist.Functionality.Search {
    public class ThingSearcher {
        public void Search(SearchOptions options, Wad wad) {
            wad.ThingLists = new List<List<Thing>>();

            foreach (DataRegion region in wad.UnallocatedRegions.ToList()) {
                int position = region.Position;

                while (position < (region.Position + region.Length) - 10) {
                    if (TryFindThings(options, wad, region, position, out int newPosition)) {
                        WadHelper.MarkRegionAsAllocated(wad, position, newPosition - position);
                        position = newPosition;
                    } else {
                        position++;
                    }
                }
            }
        }

        private bool TryFindThings(SearchOptions options, Wad wad, DataRegion region, int position, out int newPosition) {
            List<Thing> things = new List<Thing>();
            int currentPosition = position;

            while (position < (region.Position + region.Length) - 10) {
                Thing thing = new Thing {
                    X = wad.Data.ReadShort(currentPosition),
                    Y = wad.Data.ReadShort(currentPosition + 2),
                    Angle = wad.Data.ReadUnsignedShort(currentPosition + 4),
                    Type = wad.Data.ReadUnsignedShort(currentPosition + 6),
                    Flags = wad.Data.ReadUnsignedShort(currentPosition + 8),
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

            Console.WriteLine(averageDistance);

            if (averageDistance < 32 || averageDistance > 512) {
                newPosition = 0;
                return false;
            }

            // Validation passed.
            wad.ThingLists.Add(things);
            newPosition = currentPosition;
            return true;
        }

        ////private float GetAverageDistanceBetweenThings(List<Thing> things) {
        ////    float total = 0f;
        ////    float comparisonsCount = (things.Count * things.Count / 2) - (things.Count / 2);

        ////    for (int i = 0; i < things.Count; i++) {
        ////        for (int j = 1; j < i; j++) {
        ////            Thing a = things[i];
        ////            Thing b = things[j];

        ////            total += MathHelper.GetDistance((float)a.X, (float)a.Y, (float)b.X, (float)b.Y);
        ////        }
        ////    }

        ////    return total / comparisonsCount;
        ////}

        ////private float GetAverageDistanceBetweenNearestNeighbours(List<Thing> things, int count) {
        ////    float total = 0f;

        ////    foreach (Thing thing in things) {

        ////    }
        ////}
    }
}

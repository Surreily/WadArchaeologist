using System;
using System.Collections.Generic;
using System.Linq;
using Surreily.WadArchaeologist.Functionality.Model;

namespace Surreily.WadArchaeologist.Functionality.Helpers {
    public static class WadHelper {
        public static void LoadDirectory(Wad wad) {
            int directoryLength = wad.Data.ReadInt(4);
            int directoryPosition = wad.Data.ReadInt(8);

            for (int i = 0; i < directoryLength; i++) {
                int position = wad.Data.ReadInt(directoryPosition + (i * 16));
                int length = wad.Data.ReadInt(directoryPosition + (i * 16) + 4);
                string name = wad.Data.ReadString(directoryPosition + (i * 16) + 8, 8);

                wad.DirectoryEntries.Add(new WadDirectoryEntry(position, length, name));
            }

            wad.DirectoryEntries = wad.DirectoryEntries
                .OrderBy(e => e.Position)
                .ToList();
        }

        public static void InitializeUnallocatedRegions(Wad wad) {
            if (wad.DirectoryEntries == null) {
                throw new InvalidOperationException("wad.DirectoryEntries cannot be null.");
            }

            int position = 0;

            foreach (WadDirectoryEntry entry in wad.DirectoryEntries) {
                if (entry.Position > position) {
                    wad.UnallocatedRegions.Add(new DataRegion(position, entry.Position - position));
                    position = entry.Position + entry.Length;
                }
            }

            if (position < wad.Data.Length) {
                wad.UnallocatedRegions.Add(new DataRegion(position, wad.Data.Length - position));
            }
        }

        public static void MarkRegionAsAllocated(Wad wad, int position, int length) {
            if (position + length > wad.Data.Length) {
                throw new InvalidOperationException("Supplied region goes beyond the wad's data length.");
            }

            List<DataRegion> regions = wad.UnallocatedRegions
                .Where(r => r.Intersects(position, length))
                .ToList();

            foreach (DataRegion region in regions) {
                wad.UnallocatedRegions.Remove(region);

                if (region.Position < position) {
                    wad.UnallocatedRegions.Add(new DataRegion(
                        region.Position,
                        position - region.Position));
                }

                if (region.Position + region.Length > position + length) {
                    wad.UnallocatedRegions.Add(new DataRegion(
                        position + length,
                        (region.Position + region.Length) - (position + length)));
                }
            }

            wad.UnallocatedRegions = wad.UnallocatedRegions
                .OrderBy(r => r.Position)
                .ToList();
        }
    }
}

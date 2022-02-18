using System.Collections.Generic;
using System.Linq;
using Surreily.WadArchaeologist.Functionality.DataRegions;
using Surreily.WadArchaeologist.Model;

namespace Surreily.WadArchaeologist.Functionality {
    public class Wad {
        internal Wad() {
            SideLists = new List<List<Side>>();
        }

        public byte[] Data { get; set; }

        public List<DataRegion> DataRegions { get; set; }

        public List<List<Side>> SideLists { get; set; }

        #region Data Regions

        public void SetUpDataRegions(IDataRegionInitializer regionDetector) {
            regionDetector.Initialize(this);
        }

        public void ClaimDataRegion(int startOffset, int endOffset) {
            // Find affected regions.
            List<DataRegion> regions = DataRegions
                .Where(r => r.Intersects(startOffset, endOffset))
                .ToList();

            // For each affected region, remove and replace it with smaller regions (where appropriate).
            foreach (DataRegion region in regions) {
                DataRegions.Remove(region);

                if (region.StartOffset < startOffset) {
                    DataRegions.Add(new DataRegion {
                        StartOffset = region.StartOffset,
                        EndOffset = startOffset,
                    });
                }

                if (region.EndOffset > endOffset) {
                    DataRegions.Add(new DataRegion {
                        StartOffset = endOffset,
                        EndOffset = region.EndOffset,
                    });
                }
            }

            // Sort the regions by start offset.
            DataRegions = DataRegions
                .OrderBy(r => r.StartOffset)
                .ToList();
        }

        #endregion

    }
}

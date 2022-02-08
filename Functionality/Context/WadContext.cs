using System.Collections.Generic;
using System.Linq;
using Surreily.WadArchaeologist.Model;

namespace Surreily.WadArchaeologist.Functionality.Context {
    public class WadContext {
        private List<WadRegion> _regions;
        private readonly List<List<Side>> _sideLists;

        /// <summary>
        /// Initialize a WadContext with a single region.
        /// </summary>
        /// <param name="length">The length of the region.</param>
        public WadContext(int length) : this() {
            _regions = new List<WadRegion>() {
                new WadRegion {
                    StartOffset = 0,
                    EndOffset = length,
                },
            };
        }

        /// <summary>
        /// Initialize a WadContext with a list of regions.
        /// </summary>
        /// <param name="regions">The regions.</param>
        public WadContext(List<WadRegion> regions) : this() {
            _regions = regions;
        }

        private WadContext() {
            _sideLists = new List<List<Side>>();
        }

        public List<WadRegion> Regions => _regions;

        public List<List<Side>> SideLists => _sideLists;

        public void RemoveRegion(int startOffset, int endOffset) {
            // Find affected regions.
            List<WadRegion> regions = _regions
                .Where(r => r.Intersects(startOffset, endOffset))
                .ToList();

            // For each affected region, remove and replace it with smaller regions (where appropriate).
            foreach (WadRegion region in regions) {
                _regions.Remove(region);

                if (region.StartOffset < startOffset) {
                    _regions.Add(new WadRegion {
                        StartOffset = region.StartOffset,
                        EndOffset = startOffset,
                    });
                }

                if (region.EndOffset > endOffset) {
                    _regions.Add(new WadRegion {
                        StartOffset = endOffset,
                        EndOffset = region.EndOffset,
                    });
                }
            }

            // Sort the regions by start offset.
            _regions = _regions
                .OrderBy(r => r.StartOffset)
                .ToList();
        }
    }
}

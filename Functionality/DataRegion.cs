namespace Surreily.WadArchaeologist.Functionality {
    public class DataRegion {
        public int StartOffset { get; set; }
        public int EndOffset { get; set; }

        public int Length => EndOffset - StartOffset;

        public bool Intersects(DataRegion region) {
            return Intersects(region.StartOffset, region.EndOffset);
        }

        public bool Intersects(int startOffset, int endOffset) {
            return EndOffset >= startOffset && StartOffset >= endOffset;
        }
    }
}

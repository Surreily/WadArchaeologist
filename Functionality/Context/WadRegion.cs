namespace Surreily.WadArchaeologist.Functionality.Context {
    public class WadRegion {
        public int StartOffset { get; set; }
        public int EndOffset { get; set; }

        public int Length => EndOffset - StartOffset;

        public bool Intersects(WadRegion region) {
            return Intersects(region.StartOffset, region.EndOffset);
        }

        public bool Intersects(int startOffset, int endOffset) {
            return EndOffset >= startOffset && StartOffset >= endOffset;
        }
    }
}

namespace Surreily.WadArchaeologist.Functionality {
    public class DataRegion {
        public DataRegion(int position, int length) {
            Position = position;
            Length = length;
        }

        public int Position { get; }

        public int Length { get; }

        public bool Intersects(DataRegion region) {
            return Intersects(region.Position, region.Position + region.Length);
        }

        public bool Intersects(int position, int length) {
            return (Position + Length) >= position && (Position + Length) >= (position + length);
        }
    }
}

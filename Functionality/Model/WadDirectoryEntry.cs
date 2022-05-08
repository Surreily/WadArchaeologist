namespace Surreily.WadArchaeologist.Functionality.Model {
    public class WadDirectoryEntry {
        public WadDirectoryEntry(int position, int length, string name) {
            Position = position;
            Length = length;
            Name = name;
        }

        public int Position { get; }
        public int Length { get; }
        public string Name { get; }
    }
}

namespace Surreily.WadArchaeologist.Model {
    public class Sector {
        public short FloorHeight { get; set; }
        public short CeilingHeight { get; set; }
        public string FloorTextureName { get; set; }
        public string CeilingTextureName { get; set; }
        public short Brightness { get; set; }
        public ushort Effect { get; set; }
        public ushort Tag { get; set; }
    }
}

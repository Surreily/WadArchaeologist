namespace Surreily.WadArchaeologist.Model {
    public class Line {
        public ushort StartVertexId { get; set; }
        public ushort EndVertexId { get; set; }
        public ushort Flags { get; set; }
        public ushort Effect { get; set; }
        public ushort Tag { get; set; }
        public ushort RightSideId { get; set; }
        public ushort LeftSideId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Surreily.WadArchaeologist.Model {
    public class Line {
        ushort StartVertexId { get; set; }
        ushort EndVertexId { get; set; }
        ushort Flags { get; set; }
        ushort Special { get; set; }
        ushort Tag { get; set; }
        ushort RightSideId { get; set; }
        ushort LeftSideId { get; set; }
    }
}

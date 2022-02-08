using System;
using System.Collections.Generic;
using System.Text;

namespace Surreily.WadArchaeologist.Model {
    public class Side {
        public short OffsetX { get; set; }
        public short OffsetY { get; set; }
        public string UpperTextureName { get; set; }
        public string LowerTextureName { get; set; }
        public string MiddleTextureName { get; set; }
        public ushort SectorId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Surreily.WadArchaeologist.Functionality.Model {
    public class Thing : IPoint {
        public short X { get; set; }
        public short Y { get; set; }
        public ushort Angle { get; set; }
        public ushort Type { get; set; }
        public ushort Flags { get; set; }
    }
}

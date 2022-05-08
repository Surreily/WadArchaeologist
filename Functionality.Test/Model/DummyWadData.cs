using Surreily.WadArchaeologist.Functionality.Model;

namespace Surreily.WadArchaeologist.Functionality.Test.Model {
    public class DummyWadData : IWadData {
        private int _length;

        public DummyWadData(int length) {
            _length = length;
        }

        public int Length => _length;

        public byte ReadByte(int position) {
            return 0x00;
        }

        public byte[] ReadBytes(int position, int length) {
            byte[] bytes = new byte[length];

            for (int i = 0; i < length; i++) {
                bytes[i] = 0x00;
            }

            return bytes;
        }

        public string ReadString(int position, int length) {
            return new string('0', length);
        }

        public int ReadInt(int position) {
            return 0;
        }

        public short ReadShort(int position) {
            return 0;
        }

        public uint ReadUnsignedInt(int position) {
            return 0;
        }

        public ushort ReadUnsignedShort(int position) {
            return 0;
        }
    }
}

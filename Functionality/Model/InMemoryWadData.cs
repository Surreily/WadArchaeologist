using System;
using System.Text;

namespace Surreily.WadArchaeologist.Functionality.Model {
    public class InMemoryWadData : IWadData {
        private byte[] _data;

        public InMemoryWadData(byte[] data) {
            _data = data;
        }

        public int Length => _data.Length;

        public byte ReadByte(int position) {
            return _data[position];
        }

        public byte[] ReadBytes(int position, int length) {
            throw new NotSupportedException();
        }

        public string ReadString(int position, int length) {
            return Encoding.ASCII.GetString(_data, position, length).Trim('\0');
        }

        public int ReadInt(int position) {
            return BitConverter.ToInt32(_data, position);
        }

        public short ReadShort(int position) {
            return BitConverter.ToInt16(_data, position);
        }

        public uint ReadUnsignedInt(int position) {
            return BitConverter.ToUInt32(_data, position);
        }

        public ushort ReadUnsignedShort(int position) {
            return BitConverter.ToUInt16(_data, position);
        }
    }
}

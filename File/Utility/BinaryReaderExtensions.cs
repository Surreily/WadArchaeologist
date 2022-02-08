using System.IO;
using System.Text;

namespace Surreily.WadArchaeologist.File.Utility {
    public static class BinaryReaderExtensions {
        public static string ReadString(this BinaryReader reader, int length) {
            return Encoding.ASCII.GetString(reader.ReadBytes(length));
        }
    }
}

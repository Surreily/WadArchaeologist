using System;
using System.IO;
using Surreily.WadArchaeologist.Functionality.Helpers;
using Surreily.WadArchaeologist.Functionality.Model;

namespace Surreily.WadArchaeologist.Functionality {
    public static class WadFactory {
        public static Wad Create(string filePath) {
            byte[] data;

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open)) {
                using (BinaryReader binaryReader = new BinaryReader(fileStream)) {
                    data = binaryReader.ReadBytes((int)fileStream.Length);
                }
            }

            return Create(data);
        }

        public static Wad Create(Stream stream) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            byte[] data;

            using (BinaryReader binaryReader = new BinaryReader(stream)) {
                data = binaryReader.ReadBytes((int)stream.Length);
            }

            return Create(data);
        }

        public static Wad Create(byte[] data) {
            Wad wad = new Wad(new InMemoryWadData(data));

            WadHelper.LoadDirectory(wad);
            WadHelper.InitializeUnallocatedRegions(wad);

            return wad;
        }
    }
}

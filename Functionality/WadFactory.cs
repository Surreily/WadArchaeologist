using System;
using System.IO;
using Surreily.WadArchaeologist.Functionality.Context;
using Surreily.WadArchaeologist.Functionality.Helpers;
using Surreily.WadArchaeologist.Functionality.Model;

namespace Surreily.WadArchaeologist.Functionality {
    public static class WadFactory {
        public static Wad Create(string filePath, SearchOptions options) {
            byte[] data;

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open)) {
                using (BinaryReader binaryReader = new BinaryReader(fileStream)) {
                    data = binaryReader.ReadBytes((int)fileStream.Length);
                }
            }

            return Create(data, options);
        }

        public static Wad Create(Stream stream, SearchOptions options) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            byte[] data;

            using (BinaryReader binaryReader = new BinaryReader(stream)) {
                data = binaryReader.ReadBytes((int)stream.Length);
            }

            return Create(data, options);
        }

        public static Wad Create(byte[] data, SearchOptions options) {
            Wad wad = new Wad(new InMemoryWadData(data));

            if (!options.ShouldIgnoreDirectory) {
                WadHelper.LoadDirectory(wad);
            }
            
            WadHelper.InitializeUnallocatedRegions(wad);

            return wad;
        }
    }
}

using System;
using System.IO;
using Surreily.WadArchaeologist.Functionality.Context;
using Surreily.WadArchaeologist.Functionality.DataRegions;

namespace Surreily.WadArchaeologist.Functionality {
    public static class WadFactory {
        public static Wad Create(SearchContext searchContext, string filePath) {
            if (searchContext == null) {
                throw new ArgumentNullException(nameof(searchContext));
            }

            byte[] data;

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open)) {
                using (BinaryReader binaryReader = new BinaryReader(fileStream)) {
                    data = binaryReader.ReadBytes((int)fileStream.Length);
                }
            }

            return Create(searchContext, data);
        }

        public static Wad Create(SearchContext searchContext, Stream stream) {
            if (searchContext == null) {
                throw new ArgumentNullException(nameof(searchContext));
            }

            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            byte[] data;

            using (BinaryReader binaryReader = new BinaryReader(stream)) {
                data = binaryReader.ReadBytes((int)stream.Length);
            }

            return Create(searchContext, data);
        }

        public static Wad Create(SearchContext searchContext, byte[] data) {
            Wad wad = new Wad {
                Data = data,
            };

            // TODO: Not happy with this, would rather just have a static helper class instead of this.
            // TODO: Allow specification of data region initializer to use.
            wad.SetUpDataRegions(new EntireFileDataRegionInitializer());

            return wad;
        }
    }
}

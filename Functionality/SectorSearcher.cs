////using System.Collections.Generic;
////using System.IO;
////using System.Linq;
////using System.Text;
////using Surreily.WadArchaeologist.Model;

////namespace Surreily.WadArchaeologist.Functionality {
////    public class SectorSearcher {
////        public const int MinimumNumberOfSectors = 10;

////        public List<Sector> Search(Stream stream) {
////            using (BinaryReader reader = new BinaryReader(stream)) {
////                int textureNameCharacterCount = 0;

////                while (reader.BaseStream.Position != reader.BaseStream.Length) {
////                    byte b = reader.ReadByte();

////                    if (GetIsValidTextureNameByte(b)) {
////                        textureNameCharacterCount++;
////                    } else {
////                        textureNameCharacterCount = 0;
////                    }

////                    if (textureNameCharacterCount == 16) {

////                    }
////                }
////            }
////        }

////        private List<long> GetSectorCandidates(BinaryReader reader) {
////            List<long> sectorPositions = new List<long>();
////            int validTextureNameBytesCount = 0;

////            // Seek to the earliest possible location where texture names might be defined.
////            reader.BaseStream.Seek(4, SeekOrigin.Begin);

////            // Step 1: Get all stream positions where individual sectors might begin.
////            while (reader.BaseStream.Position != reader.BaseStream.Length - 26) {
////                byte b = reader.ReadByte();

////                if (GetIsValidTextureNameByte(b)) {
////                    validTextureNameBytesCount++;
////                } else {
////                    validTextureNameBytesCount = 0;
////                }

////                if (validTextureNameBytesCount >= 16) {
////                    sectorPositions.Add(reader.BaseStream.Position - 20);
////                }
////            }

////            // Step 2: Find sectors that repeat every 26 positions - this indicates a continuous sequence.
////            List<long> refinedSectorPositions = new List<long>();
////            long latestRefinedSectorPosition = 0;

////            foreach (long sectorPosition in sectorPositions) {
////                if (latestRefinedSectorPosition + 26 > sectorPosition) {
////                    continue;
////                }

                

////                for (int i = 1; i < MinimumNumberOfSectors; i++) {

////                }
////                if (sectorPositions.Contains(sectorPosition + (i * 26))
////            }

////            // Step 2: For each located texture, refine the validation.
////            List<long> refinedSectorPositions = new List<long>();
////            long latestRefinedSectorPosition = 0;

////            foreach (long sectorPosition in sectorPositions) {
////                if (latestRefinedSectorPosition + 26 > sectorPosition) {
////                    continue;
////                }

////                reader.BaseStream.Seek(sectorPosition + 4, SeekOrigin.Begin);

                
////            }

////            Dictionary<long, int> dd = new Dictionary<long, int>();

////            foreach (long sectorPosition in sectorPositions) {
////                if (dd.ContainsKey(sectorPosition - 26)) {
////                    dd[sectorPosition]
////                }
////            }

////            // Step 2: Find stream positions that indicate a continuous list of sectors.
////            long nextSectorPosition = 0;

////            foreach (long sectorPosition in sectorPositions) {
////                // TODO: Check current sector positions!
////                long i = sectorPosition + 26;

////                while (sectorPositions.Contains(i)) {
////                    i += 26;
////                }

////                if (i - sectorPosition > MinimumNumberOfSectors * 10) {

////                }

////                for (int i = 1; i < MinimumNumberOfSectors; i++) {
////                    int nextSectorPosition = sectorPosition;
                    
////                    while (sectorPositions.Contains())
////                }
////            }
////            int nextSectorPosition = 0;

////            for (int i = 0; i < sectorPositions.Count; i++) {
////                long sectorPosition = sectorPositions[i];

////                if (sectorPosition < nextSectorPosition) {
////                    continue;
////                }

////                for (int j = 1; j < ; j++) {
////                    if (!sectorPositions.Contains(sectorPosition + (j * 26))) { // TODO: Probably very slow.
////                        break;
////                    }
////                }
////            }

////            return sectorPositions;
////        }

////        public List<Sector> ReadSectors(BinaryReader reader) {
////            // Rewind 20 positions (to the start of the potential sector).
////            reader.BaseStream.Seek(-20, SeekOrigin.Current);

////            // Read sectors until we hit an invalid one.
////            List<Sector> sectors = new List<Sector>();

////            while (TryReadSector(reader, out Sector sector)) {
////                sectors.Add(sector);
////            }

////            return sectors;
////        }

////        private Sector ReadSector(BinaryReader reader) {
////            short floorHeight = reader.ReadInt16();
////            short ceilingHeight = reader.ReadInt16();
////            byte[] floorTextureName = reader.ReadBytes(8);
////            byte[] ceilingTextureName = reader.ReadBytes(8);
////            short brightness = reader.ReadInt16();
////            ushort effect = reader.ReadUInt16();
////            ushort tag = reader.ReadUInt16();

////            return new Sector {
////                FloorHeight = floorHeight,
////                CeilingHeight = ceilingHeight,
////                FloorTextureName = Encoding.ASCII.GetString(floorTextureName),
////                CeilingTextureName = Encoding.ASCII.GetString(ceilingTextureName),
////                Brightness = brightness,
////                Effect = effect,
////                Tag = tag,
////            };
////        }

////        private bool GetIsValidTextureName(byte[] textureNameBytes) {
////            for (int i = 0; i < 8; i++) {
////                if (!GetIsValidTextureNameByte(textureNameBytes[i])) {
////                    return false;
////                }
////            }

////            return true;
////        }

////        private bool GetIsValidTextureNameByte(byte textureNameByte) {
////            return
////                (textureNameByte < 'A' || textureNameByte > 'Z') &&
////                (textureNameByte < '0' || textureNameByte > '9') &&
////                textureNameByte != '-' &&
////                textureNameByte != '_' &&
////                textureNameByte != '\0';
////        }
////    }
////}

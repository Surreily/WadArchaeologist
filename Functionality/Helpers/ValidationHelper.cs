using Surreily.WadArchaeologist.Functionality.Model;

namespace Surreily.WadArchaeologist.Functionality.Helpers {
    public static class ValidationHelper {

        #region Line

        public static bool GetIsValidLine(Wad wad, int position, int maximumSideCount) {
            ushort startVertex = wad.Data.ReadUnsignedShort(position);
            ushort endVertex = wad.Data.ReadUnsignedShort(position + 2);

            if (startVertex == endVertex) {
                return false;
            }

            if (!GetIsValidLineEffect(wad, position + 6)) {
                return false;
            }

            ushort rightSideId = wad.Data.ReadUnsignedShort(position + 10);

            if (rightSideId > maximumSideCount && rightSideId != 0xFFFF) {
                return false;
            }

            ushort leftSideId = wad.Data.ReadUnsignedShort(position + 12);

            if (leftSideId > maximumSideCount && leftSideId != 0xFFFF) {
                return false;
            }

            return true;
        }

        public static bool GetIsValidLineEffect(Wad wad, int position) {
            int lineEffect = wad.Data.ReadShort(position);

            return
                (lineEffect >= 0 && lineEffect <= 77) ||
                (lineEffect >= 79 && lineEffect <= 84) ||
                (lineEffect >= 86 && lineEffect <= 141);
        }

        #endregion

        #region Sector

        public static bool GetIsValidSector(Wad wad, int position) {
            // TODO: Also validate sector effect?
            return
                ValidationHelper.GetIsValidTextureName(wad, position + 4) &&
                ValidationHelper.GetIsValidTextureName(wad, position + 12) &&
                GetIsValidSectorBrightness(wad, position + 20);
        }

        private static bool GetIsValidSectorBrightness(Wad wad, int position) {
            short brightness = wad.Data.ReadShort(position);
            return brightness >= 0 && brightness <= 256;
        }

        #endregion

        #region Side

        public static bool GetIsValidSide(Wad wad, int position) {
            return
                ValidationHelper.GetIsValidTextureName(wad, position + 4) &&
                ValidationHelper.GetIsValidTextureName(wad, position + 12) &&
                ValidationHelper.GetIsValidTextureName(wad, position + 20);
        }

        #endregion

        #region Texture Names

        private static bool GetIsValidTextureName(Wad wad, int position) {
            int i = 1;

            // Check if this is the null texture name: "-".
            if (wad.Data.ReadByte(position) == '-') {
                while (i < 8) {
                    if (wad.Data.ReadByte(position + i) != '\0') {
                        return false;
                    }

                    i++;
                }

                return true;
            }

            // Validate the first character.
            if (!GetIsValidTextureNameByte(wad.Data.ReadByte(position))) {
                return false;
            }

            // Validate the remaining characters.
            while (i < 8) {
                if (!GetIsValidTextureNameByte(wad.Data.ReadByte(position + i))) {
                    break;
                }

                i++;
            }

            while (i < 8) {
                if (wad.Data.ReadByte(position + i) != '\0') {
                    return false;
                }

                i++;
            }

            return true;
        }

        private static bool GetIsValidTextureNameByte(byte b) {
            return
                (b >= 'A' && b <= 'Z') ||
                (b >= 'a' && b <= 'z') ||
                (b >= '0' && b <= '9') ||
                b == '_';
        }

        #endregion

    }
}

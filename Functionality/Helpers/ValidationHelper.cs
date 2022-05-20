using System.Linq;

namespace Surreily.WadArchaeologist.Functionality.Helpers {
    public static class ValidationHelper {
        public static bool GetIsValidTextureName(string textureName) {
            if (textureName.Length == 0) {
                return false;
            }

            return
                textureName == "-" ||
                textureName.All(c => GetIsValidTextureNameChar(c));
        }

        public static bool GetIsValidTextureNameChar(char c) {
            return
                (c >= 'A' && c <= 'Z') ||
                (c >= 'a' && c <= 'z') ||
                (c >= '0' && c <= '9') ||
                c == '_';
        }
    }
}

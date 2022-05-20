using Surreily.WadArchaeologist.Functionality.Model;

namespace Surreily.WadArchaeologist.Functionality.Helpers {
    public static class SectorHelper {
        public static bool GetIsValidSector(Sector sector) {
            return
                ValidationHelper.GetIsValidTextureName(sector.CeilingTextureName) &&
                ValidationHelper.GetIsValidTextureName(sector.FloorTextureName) &&
                GetIsValidBrightness(sector.Brightness);
        }

        public static bool GetIsValidBrightness(short brightness) {
            return brightness >= 0 && brightness <= 256;
        }
    }
}

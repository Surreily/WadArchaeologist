using Surreily.WadArchaeologist.Model;

namespace Surreily.WadArchaeologist.Functionality.Helpers {
    public static class SideHelper {
        public static bool GetIsValidSide(Side side) {
            return
                ValidationHelper.GetIsValidTextureName(side.UpperTextureName) &&
                ValidationHelper.GetIsValidTextureName(side.MiddleTextureName) &&
                ValidationHelper.GetIsValidTextureName(side.LowerTextureName);
        }
    }
}

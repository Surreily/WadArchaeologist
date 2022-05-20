using Surreily.WadArchaeologist.Functionality.Model;

namespace Surreily.WadArchaeologist.Functionality.Helpers {
    public static class ThingHelper {
        public static bool GetIsValidThing(Thing thing) {
            return
                GetIsValidAngle(thing.Angle) &&
                GetIsValidType(thing.Type);
        }

        public static bool GetIsValidAngle(ushort angle) {
            return angle >= 0 && angle < 360 && angle % 45 == 0;
        }

        public static bool GetIsValidType (ushort thingId) {
            return
                (thingId >= 1 && thingId <= 89) ||
                (thingId >= 2001 && thingId <= 2026) ||
                thingId == 2028 ||
                thingId == 2035 ||
                (thingId >= 2045 && thingId <= 2049) ||
                (thingId >= 3001 && thingId <= 3006);
        }
    }
}

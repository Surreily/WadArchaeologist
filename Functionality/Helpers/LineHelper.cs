using Surreily.WadArchaeologist.Model;

namespace Surreily.WadArchaeologist.Functionality.Helpers {
    public static class LineHelper {
        public static bool GetIsValidLine(Line line, int maximumSideCount) {
            return
                GetIsValidVertices(line.StartVertexId, line.EndVertexId) &&
                GetIsValidEffect(line.Effect) &&
                GetIsValidSideId(line.LeftSideId, maximumSideCount) &&
                GetIsValidSideId(line.RightSideId, maximumSideCount);
        }

        public static bool GetIsValidVertices(ushort startVertexId, ushort endVertexId) {
            return startVertexId != endVertexId;
        }

        public static bool GetIsValidEffect(ushort effect) {
            return
                (effect >= 0 && effect <= 77) ||
                (effect >= 79 && effect <= 84) ||
                (effect >= 86 && effect <= 141);
        }

        public static bool GetIsValidSideId(ushort sideId, int maximumSideCount) {
            return sideId == 0xFFFF || sideId <= maximumSideCount;
        }
    }
}

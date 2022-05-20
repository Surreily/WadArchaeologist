using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Surreily.WadArchaeologist.Functionality.Model;

namespace Surreily.WadArchaeologist.Functionality.Helpers {
    public static class MathHelper {
        public static bool DoLinesIntersect(short aX, short aY, short bX, short bY, short cX, short cY, short dX, short dY) {
            var aSide = (dX - cX) * (aY - cY) - (dY - cY) * (aX - cX) > 0;
            var bSide = (dX - cX) * (bY - cY) - (dY - cY) * (bX - cX) > 0;
            var cSide = (bX - aX) * (cY - aY) - (bY - aY) * (cX - aX) > 0;
            var dSide = (bX - aX) * (dY - aY) - (bY - aY) * (dX - aX) > 0;
            return aSide != bSide && cSide != dSide;
        }

        public static float GetDistance(short aX, short aY, short bX, short bY) {
            return Math.Abs(aX - bX) + Math.Abs(aY - bY);
        }

        public static float GetAverageDistanceBetweenClosestPoints<T>(List<T> points, int neighbourCount)
            where T : IPoint {

            return points
                .SelectMany(p1 => points
                    .Where(p2 => p1.X != p2.X || p1.Y != p2.Y)
                    .Select(p2 => (float)GetDistance(p1.X, p1.Y, p2.X, p2.Y))
                    .OrderBy(d => d)
                    .Take(neighbourCount))
                .Average();
        }
    }
}

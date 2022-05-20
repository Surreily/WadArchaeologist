using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Surreily.WadArchaeologist.Functionality.Helpers;
using Surreily.WadArchaeologist.Functionality.Model;
using Surreily.WadArchaeologist.Functionality.Test.Model;

namespace Surreily.WadArchaeologist.Functionality.Test.Core {
    [TestClass]
    public class MathHelperTests {
        [TestMethod]
        [DataRow(3, 2, 3, 5, 4, 2, 4, 5, false)] // Parallel lines.
        [DataRow(3, 2, 3, 5, 3, 2, 4, 2, false)] // Joined corner.
        [DataRow(3, 2, 3, 5, 2, 4, 5, 4, true)] // Intersecting lines.
        [DataRow(3, 2, 3, 5, 3, 2, 3, 5, false)] // Overlapping lines (1 line uses the same two vertices).
        public void TestDoLinesIntersect(int aX, int aY, int bX, int bY, int cX, int cY, int dX, int dY, bool expectedResult) {
            Assert.AreEqual(
                expectedResult,
                MathHelper.DoLinesIntersect(
                    (short)aX, (short)aY, (short)bX, (short)bY, (short)cX, (short)cY, (short)dX, (short)dY));
        }

        [TestMethod]
        [DataRow(0, 0, 1, 0, 1)] // 1 point away (1 dimension).
        [DataRow(0, 0, 0, 2, 2)] // 2 points away (1 dimension).
        [DataRow(0, 0, 1, 1, 2)] // 2 points away (2 dimensions).
        [DataRow(0, 0, 0, 0, 0)] // No points away.
        [DataRow(1, 1, 6, 4, 8)] // Far away.
        [DataRow(5, 2, 2, 5, 6)] // Going 'backwards' on the graph.
        [DataRow(5, 5, 2, 2, 6)] // Going 'backwards' on both axes.
        public void TestGetDistance(int aX, int aY, int bX, int bY, int expectedResult) {
            Assert.AreEqual(
                expectedResult,
                MathHelper.GetDistance((short)aX, (short)aY, (short)bX, (short)bY));
        }

        [TestMethod]
        [DataRow(1, 13f / 6f)]
        [DataRow(2, 29f / 12f)]
        [DataRow(3, 53f / 18f)]
        [DataRow(4, 78f / 24f)]
        public void TestGetAverageDistance(int neighbourCount, float expectedResult) {
            List<IPoint> points = new List<IPoint> {
                new DummyPoint { X = 3, Y = 5, },
                new DummyPoint { X = 1, Y = 3, },
                new DummyPoint { X = 4, Y = 3, },
                new DummyPoint { X = 3, Y = 2, },
                new DummyPoint { X = 4, Y = 2, },
                new DummyPoint { X = 0, Y = 0, },
            };

            Assert.AreEqual(
                expectedResult,
                MathHelper.GetAverageDistanceBetweenClosestPoints(points, neighbourCount),
                0.001f);
        }
    }
}

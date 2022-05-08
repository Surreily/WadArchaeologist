using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Surreily.WadArchaeologist.Functionality.Helpers;
using Surreily.WadArchaeologist.Functionality.Model;
using Surreily.WadArchaeologist.Functionality.Test.Model;

namespace Surreily.WadArchaeologist.Functionality.Test.Core {
    [TestClass]
    public class WadHelperTests {
        private Wad wad;

        [TestInitialize]
        public void TestInitialize() {
            wad = new Wad(new DummyWadData(100));
            wad.UnallocatedRegions = new List<DataRegion>();
        }

        [TestMethod]
        public void TestRegionsCanBeClaimed() {
            wad.UnallocatedRegions.Add(new DataRegion(0, 100));

            WadHelper.MarkRegionAsAllocated(wad, 0, 20);
            Assert.AreEqual(wad.UnallocatedRegions.Count, 1);
            Assert.AreEqual(wad.UnallocatedRegions[0].Position, 20);
            Assert.AreEqual(wad.UnallocatedRegions[0].Length, 80);

            WadHelper.MarkRegionAsAllocated(wad, 80, 20);
            Assert.AreEqual(wad.UnallocatedRegions.Count, 1);
            Assert.AreEqual(wad.UnallocatedRegions[0].Position, 20);
            Assert.AreEqual(wad.UnallocatedRegions[0].Length, 60);

            WadHelper.MarkRegionAsAllocated(wad, 40, 20);
            Assert.AreEqual(wad.UnallocatedRegions.Count, 2);
            Assert.AreEqual(wad.UnallocatedRegions[0].Position, 20);
            Assert.AreEqual(wad.UnallocatedRegions[0].Length, 20);
            Assert.AreEqual(wad.UnallocatedRegions[1].Position, 60);
            Assert.AreEqual(wad.UnallocatedRegions[1].Length, 20);
        }
    }
}

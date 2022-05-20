using Microsoft.VisualStudio.TestTools.UnitTesting;
using Surreily.WadArchaeologist.Functionality.Helpers;

namespace Surreily.WadArchaeologist.Functionality.Test.Helpers {
    [TestClass]
    public class ValidationHelperTests {
        [TestMethod]
        [DataRow("", false)]
        [DataRow("ABCD", true)] // Uppercase characters.
        [DataRow("1234", true)] // Numbers.
        [DataRow("abcd", true)] // Lowercase characters.
        [DataRow("ABCD1234", true)] // Mix of characters and numbers.
        [DataRow("AB CD", false)] // Space.
        [DataRow("AB_CD", true)] // Underscore.
        [DataRow("AB-CD", false)] // Other special characters.
        public void TestGetIsValidTextureName(string textureName, bool expectedResult) {
            Assert.AreEqual(
                expectedResult,
                ValidationHelper.GetIsValidTextureName(textureName));
        }
    }
}

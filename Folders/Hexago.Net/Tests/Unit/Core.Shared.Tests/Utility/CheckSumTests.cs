using Core.Shared.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace $safeprojectname$.Utility
{
    [TestClass]
    public class CheckSumTests
    {
        [TestMethod]
        public void Correct_md5_hash_should_be_generated_by_GenerateChecksum()
        {
            // Arrange
            var string1 = "THis is a string";
            var string2 = "This is a string";
            var string3 = "d6991/d5506f*d(d}d1]3489&f949$09@c24!d6c3";
            var string4 = "d6991/d5506f*d(d}d1]3489&f949$09@c24!d6c3";
            var string5 = "d6991/d5506f*d(d}d1]3489&f949$09@c24!D6c3";

            // Act
            var hash1 = CheckSum.GenerateChecksum(string1);
            var hash2 = CheckSum.GenerateChecksum(string2);
            var hash3 = CheckSum.GenerateChecksum(string3);
            var hash4 = CheckSum.GenerateChecksum(string4);
            var hash5 = CheckSum.GenerateChecksum(string5);

            // Assert
            Assert.AreNotEqual(hash1, hash2);
            Assert.AreEqual(hash3, hash4);
            Assert.AreNotEqual(hash4, hash5);
        }
    }
}

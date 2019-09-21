using Core.Shared.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace $safeprojectname$.Utility
{
    [TestClass]
    public class StringUtilityTests
    {
        [TestMethod]
        public void Md5HashEncode_method_should_be_correct()
        {
            // Arrange
            var string1 = "THis is a string";
            var string2 = "This is a string";
            var string3 = "d6991/d5506f*d(d}d1]3489&f949$09@c24!d6c3";
            var string4 = "d6991/d5506f*d(d}d1]3489&f949$09@c24!d6c3";
            var string5 = "d6991/d5506f*d(d}d1]3489&f949$09@c24!D6c3";

            // Act
            var hash1 = StringUtility.Md5HashEncode(string1);
            var hash2 = StringUtility.Md5HashEncode(string2);
            var hash3 = StringUtility.Md5HashEncode(string3);
            var hash4 = StringUtility.Md5HashEncode(string4);
            var hash5 = StringUtility.Md5HashEncode(string5);

            // Assert
            Assert.AreNotEqual(hash1, hash2);
            Assert.AreEqual(hash3, hash4);
            Assert.AreNotEqual(hash4, hash5);
            Assert.AreEqual("a7655a40b64eddc4eceedd8f84a90fd0", hash1);
            Assert.AreEqual("41fb5b5ae4d57c5ee528adb00e5e8e74", hash2);
            Assert.AreEqual("6125427b4aaf41e9c2976acb7f81532c", hash3);
            Assert.AreEqual("6125427b4aaf41e9c2976acb7f81532c", hash4);
            Assert.AreEqual("0003d81d8a8e30b07bcf6580e36a9a84", hash5);
        }

        [TestMethod]
        public void VerifyMd5Hash_method_work()
        {
            // Arrange
            var input1 = "THis is a string";
            var input2 = "This is a string";
            var input3 = "d6991/d5506f*d(d}d1]3489&f949$09@c24!d6c3";
            var input4 = "d6991/d5506f*d(d}d1]3489&f949$09@c24!d6c3";
            var input5 = "d6991/d5506f*d(d}d1]3489&f949$09@c24!D6c3";

            // Act
            var hash1 = StringUtility.Md5HashEncode(input1);
            var hash2 = StringUtility.Md5HashEncode(input2);
            var hash3 = StringUtility.Md5HashEncode(input3);
            var hash4 = StringUtility.Md5HashEncode(input4);
            var hash5 = StringUtility.Md5HashEncode(input5);

            // Assert
            Assert.IsTrue(StringUtility.VerifyMd5Hash(input1, hash1));
            Assert.IsTrue(StringUtility.VerifyMd5Hash(input2, hash2));
            Assert.IsTrue(StringUtility.VerifyMd5Hash(input3, hash3));
            Assert.IsTrue(StringUtility.VerifyMd5Hash(input4, hash4));
            Assert.IsTrue(StringUtility.VerifyMd5Hash(input5, hash5));
        }

        [TestMethod]
        public void SmartTrim_method_should_remove_non_alphanumeric_character()
        {
            var input = "d69+91/d5 506f*d(d}d1]34?\\89&f.94_9$09@c24!d6-c3";

            var trimed = StringUtility.SmartTrim(input);

            Assert.AreEqual("d6991d5506fddd13489f94909c24d6c3", trimed);
        }

        [TestMethod]
        public void NormalHash_and_SanitizeHash_method_should_generate_hash_and_remove_non_alphanumeric_character()
        {
            var input = "d6991/d5506f*d(d}d1]34?\\89&f949$09@c24!d6c3";

            var normalHash = StringUtility.NormalHash(input);
            var sanitizeHash = StringUtility.SanitizeHash(input);

            Assert.AreEqual("f710a2d90bb0de4cfb8c4e7af2209fa0", normalHash);
            Assert.AreEqual("2a4d03ed2c5a8db939811bb19fbfc742", sanitizeHash);
        }

        [TestMethod]
        public void SanitizeText_method_should_remove_and_eventually_replace_non_alphanumeric_character()
        {
            var input = "d69 91/d55 06f*d(d}d1]34?\\89&f 949$09 @c2 4!d6c3";

            var sanitized = StringUtility.SanitizeText(input);
            var sanitizedAndReplaced = StringUtility.SanitizeText(input, "-");

            Assert.AreEqual("d6991d5506fddd13489f94909c24d6c3",sanitized);
            Assert.AreEqual("d69-91-d55-06f-d-d-d1-34-89-f-949-09-c2-4-d6c3",sanitizedAndReplaced);
        }

        [TestMethod]
        public void ToPascalCase_method_should_transform_sentence_to_pascal_case()
        {
            var input = "this is a simple hash: d6991/d5506f*d(d}d1]34?\\89&f949$09@c24!d6c3";

            var pascalCased = StringUtility.ToPascalCase(input);

            Assert.AreEqual("ThisIsASimpleHash:D6991/d5506f*d(d}d1]34?\\89&f949$09@c24!d6c3", pascalCased);
        }

        [TestMethod]
        public void Base64Encode_and_Base64Decode_should_generate_correct_Base64_encoding_and_decoding()
        {
            var input = "this is a simple hash: d6991/d5506f*d(d}d1]34?\\89&f949$09@c24!d6c3";

            var encoded = StringUtility.Base64Encode(input);
            var decoded = StringUtility.Base64Decode(encoded);

            Assert.AreEqual(input, decoded);
        }
    }
}

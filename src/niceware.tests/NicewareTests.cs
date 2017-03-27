using System;
using System.Collections.Generic;
using Xunit;
using niceware;

namespace niceware.tests
{
    public class NicewareTests
    {
        [Theory]
        [MemberData(nameof(ToPassphraseData))]
        public void ToPassphrase(byte[] bytes, IEnumerable<string> expected)
        {
            var actual = bytes.ToPassphrase();
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> ToPassphraseData()
        {
            yield return new object[] { new byte[0], new string[0] };
            yield return new object[] { new byte[] { 0, 0}, new [] { "a" } };
            yield return new object[] { new byte[] { 255, 255}, new [] { "zyzzyva" } };
            yield return new object[] 
            { 
                new byte[] { 0, 0, 17, 212, 12, 140, 90, 247, 46, 83, 254, 60, 54, 169, 255, 255 },
                new [] { "a", "bioengineering", "balloted", "gobbledegook", "creneled", "written", "depriving", "zyzzyva" }
            };
        }

        [Theory]
        [InlineData(2, 1)]
        [InlineData(0, 0)]
        [InlineData(8, 4)]
        [InlineData(20, 10)]
        [InlineData(512, 256)]
        public void GeneratePassphrase(int length, int expectedWords)
        {
            var actual = Niceware.GeneratePassphrase(length);
            Assert.Equal(expectedWords, actual.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(23)]
        public void GeneratePassphrase_OddBytes(int length)
        {
           var ex = Assert.Throws<ArgumentException>(() => Niceware.GeneratePassphrase(length));
           Assert.Equal("Only even-sized byte arrays are supported", ex.Message);
        }

        [Theory]
        [InlineData(1025)]
        [InlineData(-1)]
        public void GeneratePassphrase_OutOfRange(int length)
        {
           var ex = Assert.Throws<ArgumentException>(() => Niceware.GeneratePassphrase(length));
           Assert.Equal($"Size must be between 0 and {Niceware.MaxPassphraseSize} bytes", ex.Message);
        }

        [Theory]
        [MemberData(nameof(PassphraseToBytesData))]
        public void PassphraseToBytes(IEnumerable<string> passphrase, byte[] expected)
        {
            var actual = passphrase.PassphraseToBytes();
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> PassphraseToBytesData()
        {
            yield return new object[] { new [] { "A" }, new byte[] { 0, 0 } };
            yield return new object[] { new [] { "zyzzyva" }, new byte[] { 255, 255 } };
            yield return new object[] 
            { 
                new [] { "A", "bioengineering", "Balloted", "gobbledegooK", "cReneled", "wriTTen", "depriving", "zyzzyva" },
                new byte[] { 0, 0, 17, 212, 12, 140, 90, 247, 46, 83, 254, 60, 54, 169, 255, 255 }
            };
        }

        [Fact]
        public void PassphraseToBytes_NullOk()
        {
            var actual = ((string[])null).PassphraseToBytes();
            Assert.Equal(new byte[0], actual);
        }

        [Fact]
        public void PassphraseToBytes_InvalidWord()
        {
            var ex = Assert.Throws<ArgumentException>(() => new [] { "You", "love", "ninetales" }.PassphraseToBytes());
            Assert.StartsWith("Invalid word", ex.Message);
        }
    }
}


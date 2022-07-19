using testObjects.source.simple.strings;
using Xunit;

namespace testObjects.test.simple.strings
{
    public class PalindromeTests
    {
        private readonly Palindrome _palindrome;

        public PalindromeTests()
        {
            _palindrome = new Palindrome();
        }

        [Fact]
        public void ThePalindromeOfAnEmptyStringIsEmpty()
        {
            var result = _palindrome.Get("");
            
            Assert.Equal("",result.GetReturnValue());
        }

        [Theory]
        [MemberData(nameof(PalindromeCollection))]
        public void PalindromesOfStringsShouldEqual(string str, string palindrome)
        {
            var result = _palindrome.Get(str);
            
            Assert.Equal(palindrome,result.GetReturnValue());
        }
        
        [Theory]
        [MemberData(nameof(PalindromeCollection))]
        public void PalindromesRecursiveOfStringsShouldEqual(string str, string palindrome)
        {
            var result = _palindrome.GetRecursive(str);
            
            Assert.Equal(palindrome,result.GetReturnValue());
        }

        public static IEnumerable<object[]> PalindromeCollection()
        {
            yield return new object[] {"a","a" };
            yield return new object[] {"z","z" };
            
            yield return new object[] { "aa","aa"};
            yield return new object[] { "zz","zz"};
            yield return new object[] { "az","za"};
            
            yield return new object[] { "aaz","zaa"};
            yield return new object[] { "aza","aza"};
            yield return new object[] { "zaa","aaz"};

        }
    }
}
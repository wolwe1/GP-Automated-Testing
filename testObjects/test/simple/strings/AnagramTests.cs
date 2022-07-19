using testObjects.source.simple.strings;
using Xunit;

namespace testObjects.test.simple.strings
{
    public class AnagramTests
    {
        private readonly Anagram _anagram;

        public AnagramTests()
        {
            _anagram = new Anagram();
        }

        [Fact]
        public void EmptyIsAnagramOfEmpty()
        {
            var result = _anagram.Get("", "");

            Assert.True(result.GetReturnValue());
        }

        [Theory]
        [MemberData(nameof(AnagramCollection))]
        public void AnagramsOfStringsShouldEqual(string str, string strTwo, bool anagram)
        {
            var result = _anagram.Get(str, strTwo);

            Assert.Equal(anagram, result.GetReturnValue());
        }

        [Theory]
        [MemberData(nameof(AnagramCollection))]
        public void AnagramsRecursiveOfStringsShouldEqual(string str, string strTwo, bool anagram)
        {
            var result = _anagram.GetRecursive(str, strTwo);

            Assert.Equal(anagram, result.GetReturnValue());
        }

        public static IEnumerable<object[]> AnagramCollection()
        {
            yield return new object[] {"", "", true};
            yield return new object[] {"a", "a", true};
            yield return new object[] {"z", "z", true};
            
            yield return new object[] {"aa", "aa", true};
            yield return new object[] {"zz", "zz", true};
            yield return new object[] {"az", "za", true};
            
            yield return new object[] {"aaz", "zaa", true};
            yield return new object[] {"aza", "aza", true};
            yield return new object[] {"zaa", "aaz", true};
            
            yield return new object[] {"Elbow", "Below", true};
            yield return new object[] {"Night", "Thing", true};
            yield return new object[] {"Bored", "Robed", true};
            yield return new object[] {"Astronomer", "Moon starer", true};
            yield return new object[] {"Astronomer", "Moonstarer", true};
            yield return new object[] {"Astronomer", "MoonStarer", true};
            yield return new object[] {"The Morse Code", "Here come dots", true};
            
            yield return new object[] {"Elbow", "Bellow", false};
            yield return new object[] {"The Morse Code", "Here come dot", false};
            yield return new object[] {"", "Banana", false};
            yield return new object[] {"banana", "nanaba", true};
            yield return new object[] {"banana", "nanaban", false};
            yield return new object[] {"banana", "nacaba", false};
        }
    }
}
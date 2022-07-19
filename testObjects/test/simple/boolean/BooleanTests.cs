using testObjects.source.simple.boolean;
using Xunit;

namespace testObjects.test.simple.boolean
{
    public class BooleanTests
    {
        private readonly BooleanArtefacts _booleanArtefacts;

        public BooleanTests()
        {
            _booleanArtefacts = new BooleanArtefacts();
        }

        [Fact]
        public void OnlyActivatesIfTrue()
        {
            var resultWhenTrue = _booleanArtefacts.TrueOrNothing(true);
            var resultWhenFalse = _booleanArtefacts.TrueOrNothing(false);
            
            Assert.Equal("The statement was activated",resultWhenTrue.GetReturnValue());
            Assert.Equal("",resultWhenFalse.GetReturnValue());

        }
        
        [Fact]
        public void EitherOrShouldReturnEitherOnTrue()
        {
            var result = _booleanArtefacts.EitherOr(true);
            
            Assert.Equal("Either",result.GetReturnValue());
        }
        
        [Fact]
        public void EitherOrShouldReturnOrOnFalse()
        {
            var result = _booleanArtefacts.EitherOr(false);
            
            Assert.Equal("Or",result.GetReturnValue());
        }
        
        [Theory]
        [MemberData(nameof(AndFunctionCombination))]
        public void AndShouldOnlyTriggerOnBoth(bool first, bool second,string answer)
        {
            var result = _booleanArtefacts.And(first, second);
            
            Assert.Equal(answer,result.GetReturnValue());
        }
        
        [Theory]
        [MemberData(nameof(OrFunctionCombination))]
        public void OrShouldTriggerOnEither(bool first, bool second, string answer)
        {
            var result = _booleanArtefacts.Or(first, second);
            
            Assert.Equal(answer,result.GetReturnValue());

        }

        [Theory]
        [MemberData(nameof(AndOrFunctionCombination))]
        public void AndOrShouldReturnValidMessageOnCombination(bool first, bool second, string answer)
        {
            var result = _booleanArtefacts.AndOr(first, second);
            
            Assert.Equal(answer,result.GetReturnValue());
        }
        
        public static IEnumerable<object[]> OrFunctionCombination()
        {
            yield return new object[] {true,true, "The statement was activated"};
            yield return new object[] {true,false, "The statement was activated"};
            yield return new object[] {false,true, "The statement was activated"};
            yield return new object[] {false,false, ""};
        }
        public static IEnumerable<object[]> AndFunctionCombination()
        {
            yield return new object[] {true,true, "The statement was activated"};
            yield return new object[] {true,false, ""};
            yield return new object[] {false,true, ""};
            yield return new object[] {false,false, ""};
        }
        
        public static IEnumerable<object[]> AndOrFunctionCombination()
        {
            yield return new object[] {true,true, "Both values were true"};
            yield return new object[] {true,false, "The first value was true"};
            yield return new object[] {false,true, "The second value was true"};
            yield return new object[] {false,false, "Neither value were true"};
        }

    }
}
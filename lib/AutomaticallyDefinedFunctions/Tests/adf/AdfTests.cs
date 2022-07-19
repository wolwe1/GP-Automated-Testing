using lib.AutomaticallyDefinedFunctions.factories;
using lib.AutomaticallyDefinedFunctions.generators.adf;
using lib.AutomaticallyDefinedFunctions.structure.adf;
using Xunit;

namespace lib.AutomaticallyDefinedFunctions.Tests.adf
{
    public class AdfTests
    {
        [Theory]
        [MemberData(nameof(AdfTypeGeneratorSet))]
        public void CreateAdfsToEvaluate<T>(AdfGenerator<T> generator) where T : IComparable
        {
            for (var i = 0; i < 50; i++)
            {
                var adf = generator.Generate();
                AdfCorrectlyProduceId<T>(adf,generator);
            }
        }
        
        public void AdfCorrectlyProduceId<T>(Adf<T> adf,AdfGenerator<T> generator) where T : IComparable
        {
            var originalId = adf.GetId();

            var functionFromId = generator.GenerateFromId(originalId);

            Assert.Equal(functionFromId.GetId(), originalId);
        }

        public static IEnumerable<object[]> AdfTypeGeneratorSet()
        {
            yield return new object[]
            {
                NodeBuilder.CreateStateAdfGenerator<string,string>()
            };
            
            yield return new object[]
            {
                NodeBuilder.CreateStateAdfGenerator<double,string>()
            };
            
            yield return new object[]
            {
                NodeBuilder.CreateStateAdfGenerator<bool,string>()

            };
        }
        
    }
}
using lib.AutomaticallyDefinedFunctions.exceptions;
using lib.AutomaticallyDefinedFunctions.factories;
using lib.AutomaticallyDefinedFunctions.generators.adf;
using lib.AutomaticallyDefinedFunctions.structure.adf;
using Xunit;

namespace lib.AutomaticallyDefinedFunctions.Tests.adf
{
    public class MainProgramTests
    {
        [Theory]
        [MemberData(nameof(AdfTypeGeneratorSet))]
        public void CreateMainProgramsToEvaluate<T>(AdfGenerator<T> generator) where T : IComparable
        {
            for (var i = 0; i < 50; i++)
            {
                var mainProgram = generator.Generate().GetMainPrograms().First();
                MainProgramCorrectlyProduceId(mainProgram,generator);
                //ReproducedMainsReturnSameResult(mainProgram, generator);
            }
        }
        
        //Cant guarantee with random state node
        public void ReproducedMainsReturnSameResult<T>(MainProgram<T> mainProgram,AdfGenerator<T> generator) where T : IComparable
        {
            var originalId = mainProgram.GetId();

            var mainFromId = generator.GenerateMainFromId(originalId);
            
            var reproducedValue = TryRun(mainFromId);
            var originalValue = TryRun(mainProgram);
            Assert.Equal(originalValue, reproducedValue );
        }

        private T TryRun<T>(MainProgram<T> prog) where T : IComparable
        {
            try
            {
                return prog.GetValue();
            }
            catch (ProgramLoopException )
            {
                return default(T);
            }
        }
        
        public void MainProgramCorrectlyProduceId<T>(MainProgram<T> adf,AdfGenerator<T> generator) where T : IComparable
        {
            var originalId = adf.GetId();

            var mainFromId = generator.GenerateMainFromId(originalId);

            Assert.Equal(mainFromId.GetId(), originalId);
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
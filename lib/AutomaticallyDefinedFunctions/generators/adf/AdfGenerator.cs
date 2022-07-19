using lib.AutomaticallyDefinedFunctions.factories;
using lib.AutomaticallyDefinedFunctions.factories.functionFactories;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.adf;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.generators.adf
{
    public class AdfGenerator<T>  where T : IComparable
    {
        private readonly FunctionDefinitionHolder<T> _definitionHolder;
        private readonly AdfSettings _settings;
        
        //Helpers
        private readonly FunctionDefinitionGenerator<T> _definitionGenerator;
        private readonly MainGenerator<T> _mainGenerator;
        //For external use
        private readonly FunctionCreator _functionCreator;

        public AdfGenerator(int seed, AdfSettings settings)
        {
            RandomNumberFactory.SetSeed(seed);
            
            _settings = settings;
            
            _definitionHolder = new FunctionDefinitionHolder<T>();
            _definitionGenerator = new FunctionDefinitionGenerator<T>(settings);
            _mainGenerator = new MainGenerator<T>(settings);

            _functionCreator = _mainGenerator.GetGeneratorCopy();
        }
        
        public virtual Adf<T> Generate()
        {
            var newAdf = new Adf<T>();

            return LoadAdf(newAdf);
        }

        protected Adf<T> LoadAdf(Adf<T> adf)
        {for (var i = 0; i < _settings.NumberOfFunctions; i++)
            {
                var function = _definitionGenerator.GenerateFunctionDefinition(i);
                _definitionHolder.AddDefinition(function);

                adf.UseDefinition(function);
            }
            
            _mainGenerator.AddDefinitions(_definitionHolder);
            
            for (var i = 0; i < _settings.ArgumentCount; i++) 
            {
                var main = _mainGenerator.GenerateMainFunction();
                
                adf.UseMain(main);
            }

            _mainGenerator.Reset();
            return adf;
            
        }

        public INode<T> GenerateSubTree(int maxDepth)
        {
            return _definitionGenerator.CreateFunction(maxDepth);
        }

        public Adf<T> GenerateFromId(string originalId)
        {
            var (mainProgramsIdList, functionDefinitionsIdList) = AdfParser.ParseAdfId(originalId);

            var mainPrograms = _mainGenerator.GenerateMainsFromIdList(mainProgramsIdList);
            var definitions = _definitionGenerator.GenerateFunctionsFromIdList(functionDefinitionsIdList);

            return CreateAdfFrom(mainPrograms, definitions);
        }

        protected virtual Adf<T> CreateAdfFrom(IEnumerable<MainProgram<T>> mainPrograms, IEnumerable<FunctionDefinition<T>> definitions)
        {
            var adf = new Adf<T>();

            foreach (var definition in definitions)
            {
                adf.UseDefinition(definition);
            }

            foreach (var mainProgram in mainPrograms)
            {
                adf.UseMain(mainProgram);
            }

            return adf;
        }
        
        public FunctionDefinition<T> GenerateFunctionFromId(string id)
        {
            return _definitionGenerator.GenerateFunctionFromId(id,0);
        }

        public MainProgram<T> GenerateMainFromId(string id)
        {
            return _mainGenerator.GenerateMainFromId(id);
        }

        public FunctionCreator GetFunctionCreator()
        {
            return _functionCreator;
        }

        public void SetSeed(int seed)
        {
            RandomNumberFactory.SetSeed(seed);
        }
    }
}
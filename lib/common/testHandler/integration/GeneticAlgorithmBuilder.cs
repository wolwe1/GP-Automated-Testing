using lib.AutomaticallyDefinedFunctions.generators.adf;
using lib.common.connectors.ga.state;
using lib.common.connectors.operators;
using lib.common.connectors.operators.implementation;
using lib.common.connectors.output;
using lib.common.coverage.calculators;
using lib.common.fitnessFunctions;
using lib.common.settings;
using lib.GeneticAlgorithm.source.controlModel;
using lib.GeneticAlgorithm.source.controlModel.selectionMethods;
using lib.GeneticAlgorithm.source.controlModel.terminationCriteria;
using lib.GeneticAlgorithm.source.core;
using lib.GeneticAlgorithm.source.core.population;
using lib.GeneticAlgorithm.source.fitnessFunctions;
using lib.GeneticAlgorithm.source.operators;

namespace lib.common.testHandler.integration
{
    /*
     * This builder implementation loads some settings from the Global settings file and generates a GA with some standard presets
     */
    public class GeneticAlgorithmBuilder : IGeneticAlgorithmBuilder
    {
        private readonly int _terminalChance;
        private readonly int _maxFunctionDepth;
        private readonly int _maxMainDepth;
        private readonly int _maxGenerations;
        private readonly int _populationSize;

        public GeneticAlgorithmBuilder()
        {
            _terminalChance = GlobalSettings.TerminalChance;
            _maxFunctionDepth = GlobalSettings.MaxFunctionDepth;
            _maxMainDepth = GlobalSettings.MaxMainDepth;
            _maxGenerations = GlobalSettings.MaxGenerations;
            _populationSize = GlobalSettings.PopulationSize;
        }

        public IGeneticAlgorithm<T> Build<T>(Test<object> test, int seed) where T : IComparable
        {
            var returnType = test.GetUnderlyingReturnType();

            if (returnType == typeof(string))
                return BuildGa<T, string>(test,seed);

            if (returnType == typeof(double))
                return BuildGa<T, double>(test,seed);

            if (returnType == typeof(bool))
                return BuildGa<T, bool>(test,seed);

            throw new Exception("Could not create GA based on test response");

        }

        private GeneticAlgorithm<T> BuildGa<T, TU>(Test<object> test, int seed) where T : IComparable where TU : IComparable
        {
            var populationGenerator = CreatePopulationGenerator<T,TU>(test,seed
            );
            var populationMutator = CreatePopulationMutator<T, TU>(populationGenerator);
            var fitnessFunction = CreateFitnessFunction<TU>(test);
            var controlModel = CreateControlModel(fitnessFunction,populationMutator,seed);

            return CreateGa(test,populationGenerator, controlModel);
        }

        private IPopulationMutator<T> CreatePopulationMutator<T, TU>(IPopulationGenerator<T> populationGenerator) where T : IComparable where TU : IComparable
        {
            return new AdfMutator<T>(populationGenerator)
                .UseOperator(new ReproductiveOperator<T>(30))
                .UseOperator(new MutationOperator<T,TU>(40, 5))
                .UseOperator(new CrossoverOperator<T,TU>(30));
        }

        public IPopulationGenerator<T> CreatePopulationGenerator<T, TU>(Test<object> test, int seed) where T : IComparable where TU : IComparable
        {
            var settings = new StateAdfSettings<T,TU>( _maxFunctionDepth, _maxMainDepth,test.GetArguments().Count,_terminalChance);

            return new StateAdfPopulationGenerator<T,TU>(seed,settings);
        }
        public static GeneticAlgorithm<T> CreateGa<T>(Test<object> test,IPopulationGenerator<T> populationGenerator,
            IControlModel<T> controlModel) where T : IComparable
        {
            var history = new StandardExecutionOutput<T>().OutputToFile(new CsvFileOutputPrinter());
            history.AdditionalExecutionInfo = test.GetName();
            return new GeneticAlgorithm<T>(populationGenerator,controlModel,history).Print();
        }

        public IFitnessFunction CreateFitnessFunction<TU>(Test<object> test) where TU : IComparable
        {
            return new CompositeFitnessFunction()
                .AddEvaluation(new CodeCoverageFitnessFunction<TU>(test,new StatementCoverageCalculator(),5), 1);
        }

        public IControlModel<T> CreateControlModel<T>(IFitnessFunction fitnessFunction,
            IPopulationMutator<T> populationMutator, int seed) where T : IComparable
        {
            return new SteadyStateControlModel<T>(populationMutator,fitnessFunction)
                .UseSelection(new TournamentSelection(fitnessFunction,seed,3))
                .UseTerminationCriteria(new GenerationCountCriteria(_maxGenerations))
                .UseTerminationCriteria(new NoAverageImprovementCriteria(25))
                .UseTerminationCriteria(new DesiredFitnessForFitnessFunctionCriteria(typeof(StatementCoverageCalculator),100))
                .SetPopulationSize(_populationSize);
        }
    }
}
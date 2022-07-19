using lib.GeneticAlgorithm.source.controlModel;
using lib.GeneticAlgorithm.source.core.population;
using lib.GeneticAlgorithm.source.statistics;
using lib.GeneticAlgorithm.source.statistics.history;

namespace lib.GeneticAlgorithm.source.core
{
    public class GeneticAlgorithm<T> : IGeneticAlgorithm<T> where T : IComparable
    {
        protected readonly IControlModel<T> ControlModel;
        protected readonly IExecutionHistory<T> History;
        protected readonly IPopulationGenerator<T> PopulationGenerator;
        private bool _print;

        public GeneticAlgorithm(IPopulationGenerator<T> populationGenerator, IControlModel<T> controlModel, IExecutionHistory<T> history)
        {
            PopulationGenerator = populationGenerator;
            ControlModel = controlModel;
            History = history;
        }

        public List<IPopulationMember<T>> CreateInitialPopulation()
        {
            var populationMembers = new List<IPopulationMember<T>>();

            var initialPopulationSize = ControlModel.GetInitialPopulationSize();

            for (var i = 0; i < initialPopulationSize; i++)
            {
                var newMember = PopulationGenerator.GenerateNewMember();
                populationMembers.Add(newMember);
            }

            return populationMembers;
        }

        public IExecutionHistory<T> Run()
        {
            History.NewRun();

            var generationCount = 0;
            var population = CreateInitialPopulation();

            GenerationRecord<T> results = null;
            while (!ControlModel.TerminationCriteriaMet(generationCount++, results))
            {
                if (_print) Console.WriteLine($"Generation {generationCount}");
                
                History.NewGeneration();

                results = ControlModel.Evaluate(population);

                var parents = ControlModel.SelectParents(results);

                population = ControlModel.ApplyOperators(parents);

                History.CloseGeneration(results);
            }

            return History;
        }

        public void SetSeed(int seed)
        {
            ControlModel.SetSeed(seed);
            PopulationGenerator.SetSeed(seed);
        }
        public GeneticAlgorithm<T> Print()
        {
            _print = true;
            return this;
        }

    }
}
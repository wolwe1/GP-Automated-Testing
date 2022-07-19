using lib.GeneticAlgorithm.source.core.population;
using lib.GeneticAlgorithm.source.statistics;

namespace lib.GeneticAlgorithm.source.fitnessFunctions
{
    public class CompositeFitnessFunction : FitnessFunction
    {
        private readonly List<Tuple<IFitnessFunction, double>> _fitnessCriteria;
        private readonly bool _isMinimising;

        public CompositeFitnessFunction(bool isMinimising = false)
        {
            _fitnessCriteria = new List<Tuple<IFitnessFunction, double>>();
            _isMinimising = isMinimising;
        }

        public override Fitness Evaluate<T>(IPopulationMember<T> member)
        {
            var totalFitness = new Fitness();

            foreach (var criterion in _fitnessCriteria)
            {
                var multiplier = criterion.Item2;
                var rawFitness = criterion.Item1.Evaluate(member);

                totalFitness.AddEvaluation(rawFitness, multiplier); // += rawFitness * multiplier;
            }

            return totalFitness;
        }
        
        public override MemberRecord<T> GetBest<T>(IEnumerable<MemberRecord<T>> chosenMembers)
        {
            var orderedMembers = chosenMembers
                .OrderByDescending(m => m.GetFitness().GetFitness());

            return _isMinimising ? orderedMembers.Last() : orderedMembers.FirstOrDefault();
        }

        public CompositeFitnessFunction AddEvaluation(IFitnessFunction function, double multiplyer)
        {
            _fitnessCriteria.Add(new Tuple<IFitnessFunction, double>(function, multiplyer));

            return this;
        }
    }
}
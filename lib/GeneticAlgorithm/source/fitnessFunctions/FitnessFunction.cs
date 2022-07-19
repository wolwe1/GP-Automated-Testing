using lib.GeneticAlgorithm.source.core.population;
using lib.GeneticAlgorithm.source.statistics;
using lib.GeneticAlgorithm.source.statistics.calculatedResults;

namespace lib.GeneticAlgorithm.source.fitnessFunctions
{
    public abstract class FitnessFunction : IFitnessFunction
    {
        public abstract Fitness Evaluate<T>(IPopulationMember<T> member) where T : IComparable;

        public CalculationResultSet GetNormalisedFitnessValues<T>(GenerationRecord<T> results)
        {
            var fitnessValues = results.GetFitnessValues();
            var totalFitness = results.GetTotalFitness();

            return fitnessValues.Map(x => x / totalFitness);
        }

        public MemberRecord<T> GetBest<T>(GenerationRecord<T> record)
        {
            return GetBest(record.GetMemberRecords());
        }
        public abstract MemberRecord<T> GetBest<T>(IEnumerable<MemberRecord<T>> chosenMembers);
    }
}
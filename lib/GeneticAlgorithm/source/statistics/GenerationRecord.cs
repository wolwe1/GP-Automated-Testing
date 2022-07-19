using lib.GeneticAlgorithm.source.core.population;
using lib.GeneticAlgorithm.source.fitnessFunctions;
using lib.GeneticAlgorithm.source.statistics.calculatedResults;

namespace lib.GeneticAlgorithm.source.statistics
{
    /// <summary>
    ///     Holds a set of IPopulationMember ID and Result pairs
    /// </summary>
    public class GenerationRecord<T> : IGenerationRecord
    {
        private readonly List<MemberRecord<T>> _records;

        public GenerationRecord()
        {
            _records = new List<MemberRecord<T>>();
        }

        public TimeSpan RunTime { get; set; }

        public void Add(IPopulationMember<T> member, Fitness fitness)
        {
            var newResult = new MemberRecord<T>(member, fitness);

            _records.Add(newResult);
        }

        public CalculationResultSet GetFitnessValues()
        {
            return CalculationResultSet.Create(_records);
        }

        public double GetTotalFitness()
        {
            return GetFitnessValues().Accumulate((s, n) => s + n);
        }

        public int Size()
        {
            return _records.Count;
        }

        public MemberRecord<T> GetMemberWithMaxFitness()
        {
            var highestFitness = _records.Select(r => r.GetFitnessValue()).Max();

            return _records.FirstOrDefault(r => r.GetFitnessValue() == highestFitness);
        }

        public List<MemberRecord<T>> GetMemberRecords()
        {
            return _records;
        }
        
        public List<IMemberRecord> GetMembers()
        {
            return _records.Cast<IMemberRecord>().ToList();
        }
    }
}
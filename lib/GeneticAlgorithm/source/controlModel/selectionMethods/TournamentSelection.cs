using lib.GeneticAlgorithm.source.fitnessFunctions;
using lib.GeneticAlgorithm.source.statistics;

namespace lib.GeneticAlgorithm.source.controlModel.selectionMethods
{
    public class TournamentSelection : SelectionMethod
    {
        private readonly int _tournamentSize;
        private Random _numGen;

        public TournamentSelection(IFitnessFunction function, int seed, int tournamentSize) : base(function)
        {
            _tournamentSize = tournamentSize;
            _numGen = new Random(seed);
        }

        public override List<string> SelectReturningIds<T>(GenerationRecord<T> results, int maxParentsToProduce)
        {
            var parents = new List<string>();
            
            while (parents.Count < maxParentsToProduce)
            {
                var best = HoldTournament(results);
                parents.Add(best.GetMemberId());
            }

            return parents;
        }

        public override List<MemberRecord<T>> Select<T>(GenerationRecord<T> results, int maxParentsToProduce)
        {
            var parents = new List<MemberRecord<T>>();
            
            while (parents.Count < maxParentsToProduce)
            {
                var best = HoldTournament(results);
                parents.Add(best);
            }

            return parents;
        }

        public override void SetSeed(int seed)
        {
            _numGen = new Random(seed);
        }

        private MemberRecord<T> HoldTournament<T>(GenerationRecord<T> results)
        {
            var chosenMembers = GetMembersForTournament(results);

            return FitnessFunction.GetBest(chosenMembers);
        }

        private IEnumerable<MemberRecord<T>> GetMembersForTournament<T>(GenerationRecord<T> results)
        {
            var records = results.GetMemberRecords();
            
            var chosenMembers = new List<MemberRecord<T>>();
            for (var i = 0; i < _tournamentSize; i++)
            {
                var chosenMemberIndex = _numGen.Next(records.Count);

                var chosenMember = records[chosenMemberIndex];
                chosenMembers.Add(chosenMember);
            }

            return chosenMembers;
        }
    }
}
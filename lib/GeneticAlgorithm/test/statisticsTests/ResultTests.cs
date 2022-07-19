using lib.GeneticAlgorithm.source.fitnessFunctions;
using lib.GeneticAlgorithm.source.mockImplementations;
using lib.GeneticAlgorithm.source.statistics;
using Xunit;

namespace lib.GeneticAlgorithm.test.statisticsTests
{
    public class ResultTests
    {
        [Fact]
        public static void FitnessIsStoredCorrectly()
        {
            var member = new RandomNumberMember(0);
            var fitness = 10;

            var result = new MemberRecord<double>(member, new Fitness(null,fitness));

            Assert.Equal(fitness, result.GetFitnessValue());
        }

        [Fact]
        public static void AddDuplicateWorks()
        {
            var member = new RandomNumberMember(0);
            var fitness = 10;

            var result = new MemberRecord<double>(member, new Fitness(null,fitness));

            Assert.False(result.IsDuplicate);
            Assert.Equal(1, result.NumberOfDuplicates);

            result.AddDuplicate();

            Assert.True(result.IsDuplicate);
            Assert.Equal(2, result.NumberOfDuplicates);

            result.AddDuplicate();

            Assert.True(result.IsDuplicate);
            Assert.Equal(3, result.NumberOfDuplicates);
        }

        [Fact]
        public static void RepresentsMember()
        {
            var member = new RandomNumberMember(0);
            var fitness = 10;

            var result = new MemberRecord<double>(member, new Fitness(null,fitness));
            Assert.Equal(member.GetId(), result.GetMemberId());
        }
    }
}
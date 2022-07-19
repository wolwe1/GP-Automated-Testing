using lib.GeneticAlgorithm.source.statistics;

namespace lib.GeneticAlgorithm.source.controlModel.selectionMethods
{
    /// <summary>
    ///     Represents the selection method being used to select parents for the next generation
    /// </summary>
    public interface ISelectionMethod
    {
        /// <summary>
        ///     Selects the ID's of members to be used in the next population, using the chosen underlying method
        /// </summary>
        /// <param name="results">The result set produced by evaluating the population</param>
        /// <param name="maxParentsToProduce">The number of parents the method should select</param>
        /// <returns>List of member ID's selected for the next population</returns>
        List<string> SelectReturningIds<T>(GenerationRecord<T> results,int maxParentsToProduce);
        List<MemberRecord<T>> Select<T>(GenerationRecord<T> results,int maxParentsToProduce);
        void SetSeed(int seed);
    }
}
namespace lib.GeneticAlgorithm.source.core.population
{
    /// <summary>
    ///     Interface for genetic algorithm population members
    /// </summary>
    public interface IPopulationMember<T>
    {
        /// <summary>
        ///     Returns the ID that represents the member
        /// </summary>
        /// <returns>String value representing the members structure</returns>
        string GetId();

        IOutputContainer<T> GetResult();

        IPopulationMember<T> GetCopy();
    }
}
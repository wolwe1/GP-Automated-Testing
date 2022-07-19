namespace lib.GeneticAlgorithm.source.core.population
{
    public interface IOutputContainer<out T>
    {
        IEnumerable<T> GetOutputValues();

        string GetOutputString();
    }
}
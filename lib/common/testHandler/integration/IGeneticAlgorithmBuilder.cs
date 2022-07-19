using lib.GeneticAlgorithm.source.core;

namespace lib.common.testHandler.integration;

public interface IGeneticAlgorithmBuilder
{
    IGeneticAlgorithm<T> Build<T>(Test<object> test, int seed) where T : IComparable;
}
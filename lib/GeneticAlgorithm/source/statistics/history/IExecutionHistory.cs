namespace lib.GeneticAlgorithm.source.statistics.history
{
    public interface ITypelessExecutionHistory
    {
        /// <summary>
        ///     Initialised a new run in the execution history
        /// </summary>
        void NewRun();

        /// <summary>
        ///     Initialise a new generation for the current run
        /// </summary>
        void NewGeneration();
        void Summarise();

        int GetNumberOfRuns();

        List<string> GetBestPerformerIds(int topN);

        List<string> GetBestPerformerIdsForRun(int runNumber, int topN);

    }
    public interface IExecutionHistory<T> : ITypelessExecutionHistory
    {
        /// <summary>
        ///     Adds the information about a generation to the current run
        /// </summary>
        /// <param name="generationRecord">The evaluation results of the current run</param>
        void CloseGeneration(GenerationRecord<T> generationRecord);
    }
}
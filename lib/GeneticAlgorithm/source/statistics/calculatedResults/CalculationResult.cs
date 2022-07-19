namespace lib.GeneticAlgorithm.source.statistics.calculatedResults
{
    /// <summary>
    ///     Represents an IPopulationMember and a calculated value pair
    /// </summary>
    public class CalculationResult
    {
        private readonly string _memberId;
        private readonly double _result;

        public CalculationResult(double result, string memberId)
        {
            _result = result;
            _memberId = memberId;
        }

        /// <summary>
        ///     Returns the ID of the represented IPopulationMember
        /// </summary>
        /// <returns>The member ID</returns>
        public string GetMemberId()
        {
            return _memberId;
        }

        /// <summary>
        ///     Returns the stored calculation
        /// </summary>
        /// <returns>The calculated result</returns>
        public double GetResult()
        {
            return _result;
        }
    }
}
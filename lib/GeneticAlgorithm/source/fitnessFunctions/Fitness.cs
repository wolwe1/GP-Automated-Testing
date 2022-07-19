namespace lib.GeneticAlgorithm.source.fitnessFunctions
{
    public class Fitness
    {
        private Type _evaluator;
        private readonly double _fitness;
        private double _multiplier;

        private Fitness _next;

        public Fitness()
        {
            _multiplier = 1;
        }
        public Fitness(Type evaluator, double fitness) : this()
        {
            _evaluator = evaluator;
            _fitness = fitness;
        }

        public void AddEvaluation(Fitness rawFitness, double multiplier)
        {
            _next = rawFitness;
            _next.SetMultiplier(multiplier);
        }

        private void SetMultiplier(double multiplier)
        {
            _multiplier = multiplier;
        }

        public double GetFitness()
        {
            var thisMeasureFitness = _fitness * _multiplier;

            if (_next != null)
                return thisMeasureFitness + _next.GetFitness();

            return thisMeasureFitness;
        }

        public Fitness GetFitnessOfFunction(Type typeOfFitnessFunction)
        {
            if (_evaluator == typeOfFitnessFunction)
                return this;

            if (_next != null)
                return _next.GetFitnessOfFunction(typeOfFitnessFunction);

            throw new Exception($"Desired fitness function {typeOfFitnessFunction} does not exist in fitness criteria");
        }

        public double GetFitnessNoForward()
        {
            return _fitness * _multiplier;
        }
    }
}
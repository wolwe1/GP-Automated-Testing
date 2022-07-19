using lib.GeneticAlgorithm.source.controlModel;
using lib.GeneticAlgorithm.source.controlModel.selectionMethods;
using lib.GeneticAlgorithm.source.controlModel.terminationCriteria;
using lib.GeneticAlgorithm.source.core;
using lib.GeneticAlgorithm.source.core.population;
using lib.GeneticAlgorithm.source.fitnessFunctions;
using lib.GeneticAlgorithm.source.mockImplementations;
using lib.GeneticAlgorithm.source.operators;
using lib.GeneticAlgorithm.source.statistics.history;

namespace lib.individualRunners;

public class GeneticAlgorithmMain
{
    //This main shows how the genetic algorithm class can be setup and executed
    public static void Main(string[] args)
    {
        //Generators and mutators are used to generate and modify the population
        //Here the generator creates simple numbers and the mutator makes no changes
        IPopulationGenerator<double> populationGenerator = new RandomNumberGenerator();
        IPopulationMutator<double> populationMutator = new NoOpMutator<double>(populationGenerator);

        //Fitness functions calculate the fitness of an individual
        //A composite fitness function allows multiple fitness functions to be combined to create a more complex function
        var fitnessFunction =
            new CompositeFitnessFunction()
                .AddEvaluation(new ValueDistanceFitnessFunction().SetGoal(10), 1);

        //The steady state control model was the only one created for the purpose of this project
        //Control models decide when to end a run, the population size and the selection method
        IControlModel<double> controlModel = new SteadyStateControlModel<double>(populationMutator,fitnessFunction)
            .UseSelection(new TournamentSelection(fitnessFunction,0,3))
            .UseTerminationCriteria(new GenerationCountCriteria(20))
            .UseTerminationCriteria(new NoAverageImprovementCriteria(5))
            .UseTerminationCriteria(new DesiredFitnessForFitnessFunctionCriteria(typeof(ValueDistanceFitnessFunction), 10))
            .SetPopulationSize(10);

        //Execution histories are the mechanism for data collection, the implementation decides what data is collected 
        // throughout the genetic algorithms run
        IExecutionHistory<double> history = new BasicExecutionHistory<double>();
        
        var geneticAlgorithm =
            new GeneticAlgorithm<double>(populationGenerator, controlModel, history);

        //After a run, the history component is returned with its collected data
        var runHistory = geneticAlgorithm.Run();

        //The summarise method prints out the data collected by the execution history component
        // The exact type of data collected is adjusted using fitness measure and statistic classes
        runHistory.Summarise();
    }
}
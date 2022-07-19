using lib.AutomaticallyDefinedFunctions.factories.valueNodes.standard;
using lib.AutomaticallyDefinedFunctions.structure.adf;
using lib.AutomaticallyDefinedFunctions.structure.functions.comparators;
using lib.AutomaticallyDefinedFunctions.structure.functions.ifStatement;
using lib.AutomaticallyDefinedFunctions.structure.nodes.statenodes;
using lib.AutomaticallyDefinedFunctions.structure.nodes.valueNodes;
using lib.AutomaticallyDefinedFunctions.structure.state;

namespace lib.individualRunners;

public class AdfMain
{
    //This main provides a demo of how an ADF can be constructed manually and executed
    public static void Main()
    {

        //The main program's root is a an INode tree

        var main = CreateCounterNode();
        
        //Main programs wrap the INode tree
        var adf = new StateBasedAdf<string, double>();
        adf.UseMain(new MainProgram<string>(main));

        Simulate(adf,6);

        //Output the history
        var history = adf.GetHistory();
        
        Console.WriteLine("History:");
        foreach (var output in history.OutputsWithResponse)
        {
            var adfOutput = output.Key;
            var outputs = adfOutput.GetOutputs();
            foreach (var outputOfAdf in outputs)
            {
                Console.WriteLine($"Generated: {outputOfAdf.Value} - Failed: {outputOfAdf.Failed}");
            }
               
        }

    }

    /*
     * This simple node tree will print '5' when the run count is 5, otherwise 'not 5'
     */
    private static IfNode<string, double> CreateCounterNode()
    {
        return new IfNode<string, double>()
            .SetComparisonOperator(new EqualsComparator<double>(
                new ExecutionCountStateNode(),
                new ValueNode<double>(5)
            ))
            .SetFalseCodeBlock(
                new ValueNode<string>("not 5")
            )
            .SetTrueCodeBlock(new ValueNode<string>("5"));
    }

    //Executes the provided ADF for a number of runs
    private static void Simulate<T, TU>(StateBasedAdf<T, TU> adf,int numberOfRuns) where TU : IComparable where T : IComparable
    {
        for (var i = 0; i < numberOfRuns; i++)
        {
            var output = adf.GetValues();
                
            adf.Update(output,ValueNodeFactory.GetT<TU>().GetValue());
        }
    }
}
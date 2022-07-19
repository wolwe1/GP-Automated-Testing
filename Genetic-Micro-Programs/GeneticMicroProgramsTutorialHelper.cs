using lib.AutomaticallyDefinedFunctions.generators.adf;
using lib.AutomaticallyDefinedFunctions.structure.adf;
using lib.AutomaticallyDefinedFunctions.structure.adf.helpers;
using lib.common.dynamicLoading;
using lib.common.settings;
using lib.common.testHandler.Generalisation;
using lib.common.testHandler.setup;

namespace Genetic_Microprograms;

public class GeneticMicroProgramsTutorialHelper
{
    private const string MockAdfId = "ADF(MainFunc<System.Double,System.Double>[SUB<System.Double,System.Double>[Rand['0']DIV<System.Double,System.Double>[Func<System.Double,System.Double>[LOOP<System.Double,System.Double>[Func<System.Double,System.Double>[DIV<System.Double,System.Double>[Func<System.Double,System.Double>[SUB<System.Double,System.Double>[SUB<System.Double,System.Double>[V['1']ProgResp['1']]V['9']]]Rand['0']]]=<System.Double,System.Double>[ProgResp['1']LastOut['2']]SUB<System.Double,System.Double>[Func<System.Double,System.Double>[LOOP<System.Double,System.Double>[ProgResp['1']=<System.Double,System.Double>[V['6']ProgResp['1']]LastOut['2']]]ExecCount['5']]]]ExecCount['5']]]]*Func<System.Double,System.Double>[LOOP<System.Double,System.Double>[V['Null']<<System.Double,System.Double>[V['Null']V['Null']]V['Null']]])";
 
    public static Adf<double> CreateAdfFromString()
    {
        //Settings objects control how generators behave
        var settings = new StateAdfSettings<double, double>(
            GlobalSettings.MaxFunctionDepth,
            GlobalSettings.MaxMainDepth,
            1,
            GlobalSettings.TerminalChance
        );

        var generator = new StateAdfGenerator<double, double>(0, settings);

        return generator.GenerateFromId(MockAdfId);
    }
    
    public static void LoadAdfSetFromStorage()
    {
        //Pass the directory to load the ADF's stored on file
        var adfsWithOriginTests = AdfLoader.ReadFromDirectory("\\adf\\");
    
        //Create a generalisation test handler to run generalisation tests dynamically
        // by supplying ITestStrategy
        var generalisationTestHandler = new GeneralisationTestHandler(new TestFileDescriptorStrategy("Your path\\GP-Automated-Testing\\lib\\fileWithTestsToIncludeInGeneralisation.txt"));
        //The handler will check the tests for objects with interfaces that match each ADF supplied
        var results = generalisationTestHandler.Run(adfsWithOriginTests);
        results.Summarise();
    }

    public static void RunAdf<T>(Adf<T> adf) where T : IComparable
    {
        //ADF's can be run repeatedly
        var runResultList = new List<AdfOutput<T>>();
        
        for (int i = 0; i < 5; i++)
        {
            runResultList.Add(adf.GetValues());
        }


        //A single run result can have multiple outputs, depending on the number that the ADF was told to produce
        foreach (var runResult in runResultList)
        {
            //Adf's can fail to produce output for a run
            var failed = runResult.Failed();
            
            //But otherwise they have output lists, each item in the list is one of the values produced for the run
            var output = runResult.GetOutputs();

            //Each time they run, they produce an output object
            foreach (var outputValueSet in output)
            {
                //Individual outputs may fail, instead of the whole ADF output
                var failedOutput = outputValueSet.Failed;
                //Otherwise they have a value of T
                var value = outputValueSet.Value;
            }
        }
    }
}
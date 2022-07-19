using lib.common.testHandler;
using lib.common.testHandler.integration;
using lib.common.testHandler.setup;

//This main, has the components for basic usage

//The first element will load the fibonacci test objects and run the genetic algorithm against them.
//You can then comment that segment out and uncomment the section where those ADF's are loaded from file and run a generalisation test
//The sample

//ITestStrategy objects determine how the test objects are loaded
//TestFileDescriptorStrategy("sample.txt") To have the information loaded from a file
//Another option is TestLookupStrategy -> For UI based interaction

//The supplied builder is a concrete implementation of the IGeneticAlgorithmBuilder interface
var testHistory = new TestHandler(new TestFileDescriptorStrategy(
        "Your path\\GP-Automated-Testing\\lib\\sample.txt"),new GeneticAlgorithmBuilder())
  .LoadTests()
  .RunAllTests();

// //Stored ADF's can be loaded from storage
//GeneticMicroProgramsTutorialHelper.LoadAdfSetFromStorage();

// //Or generators can create an ADF from a correctly formatted string
// GeneticMicroProgramsTutorialHelper.CreateAdfFromString();


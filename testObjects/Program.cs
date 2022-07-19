//Test objects are created as basic classes and their Get/GetRecursive methods can be called with values to create coverage result objects

//Create the test case

using testObjects.source.simple.strings;

Anagram anagram = new Anagram();

//Call it to receive coverage information for the passed in parameters
var coverageResult = anagram.Get("racecar", "racecar");
    
//Coverage result values are used by fitness functions to determine coverage


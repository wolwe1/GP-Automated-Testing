# Genetic Micro-Programs for Automated Software Testing with Large Path Coverage
This project is the work of a pilot study into the usage of Genetic Micro-Programs
to test software. The goal being to produce a more re-usable & human understandable method of automated software testing
than the current methods.

If you are viewing this after CEC'2022, I hope the presentation peaked your interest.

## Components

### ADFs
These are the Micro-Programs you have heard so much about. 
Comprised of randomly put together nodes, they hold endless possibility.
The nodes present in this project are mostly fundamental, but provide a great deal of power to the programs.
The extension possibilities are endless and I will no doubt expand on the functionality.

### Genetic algorithm
The genetic algorithm is the go-to choice for search methods 
when working with genetic programming. The implementation is mostly fit for purpose
but built in a way that expansion and modification is possible.

### Test objects
The software under test in this experiment. These objects were created
with the goal of creating a diverse set of challenges for the micro-programs that were also limited in scope
so as to provide a focused effort. More complex test objects will be added as the micro-programs are 
fitted with the capability of dealing with them. Ultimately, test objects representing real world
software components is the goal.

## Usage
The Genetic-Micro-Programs' program.cs file is the best place to start.
There you will find a short run down of how to interact with the system.

To play around with the ADF's alone, or the genetic algorithm, have a look at the
'individualRunners' folder within the lib directory.

Recent efforts have been made to document the code to aid in reader understanding,
this effort, along with refactoring will continue to provide a better usage experience.

Enjoy - Jarrod Goschen
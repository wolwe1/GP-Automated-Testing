using System.Collections;
using lib.AutomaticallyDefinedFunctions.factories;
using lib.AutomaticallyDefinedFunctions.structure.adf;
using lib.AutomaticallyDefinedFunctions.structure.functions;
using lib.AutomaticallyDefinedFunctions.structure.functions.comparators;
using lib.AutomaticallyDefinedFunctions.structure.nodes;
using lib.common.connectors.ga;
using lib.common.visitors;
using lib.GeneticAlgorithm.source.core.population;
using lib.GeneticAlgorithm.source.statistics;

namespace lib.common.connectors.operators.implementation
{
    public class CrossoverOperator<T,TU> : Operator<T> where T : IComparable where TU : IComparable
    {
        public CrossoverOperator(int applicationPercentage) : base(applicationPercentage) { }

        protected override IEnumerable<IPopulationMember<T>> Operate(List<MemberRecord<T>> parents, IPopulationGenerator<T> populationGenerator)
        {
            var parent = GetAdfFromParents(parents);
            var parent2 = GetAdfFromParents(parents);
            
            var mainToMutate = RandomNumberFactory.Next(parent.GetNumberOfMainPrograms());
            var main2ToMutate = RandomNumberFactory.Next(parent2.GetNumberOfMainPrograms());

            var main = parent.GetMainPrograms().ElementAt(mainToMutate);
            var main2 = parent2.GetMainPrograms().ElementAt(main2ToMutate);
            
            //Pick a type
            var typeOfNodesToSwap = RandomNumberFactory.Next(3);

            MainProgram<T> newMain1;
            MainProgram<T> newMain2;
            switch (typeOfNodesToSwap)
            {
                case 0: 
                    (newMain1,newMain2) = PerformCrossover<string>(main, main2);
                    parent.SetMain(mainToMutate, newMain1);
                    parent2.SetMain(main2ToMutate, newMain2);
                    break;
                case 1: 
                    (newMain1,newMain2) = PerformCrossover<double>(main, main2);
                    parent.SetMain(mainToMutate, newMain1);
                    parent2.SetMain(main2ToMutate, newMain2);
                    break;
                case 2: 
                    (newMain1,newMain2) = PerformCrossover<bool>(main, main2);
                    parent.SetMain(mainToMutate, newMain1);
                    parent2.SetMain(main2ToMutate, newMain2);
                    break;

                default: throw new Exception("Crossover could not select type of node to replace");
            }

            return new List<IPopulationMember<T>>()
            {
                new StateAdfPopulationMember<T,TU>(parent),
                new StateAdfPopulationMember<T,TU>(parent2)
            };
        }

        protected override List<string> Operate(List<string> parents, IPopulationGenerator<T> generator)
        {
            var parent = GetAdfFromParents(parents,generator);
            var parent2 = GetAdfFromParents(parents,generator);
            
            var mainToMutate = RandomNumberFactory.Next(parent.GetNumberOfMainPrograms());
            var main2ToMutate = RandomNumberFactory.Next(parent2.GetNumberOfMainPrograms());

            var main = parent.GetMainPrograms().ElementAt(mainToMutate);
            var main2 = parent2.GetMainPrograms().ElementAt(main2ToMutate);
            
            //Pick a type
            var typeOfNodesToSwap = RandomNumberFactory.Next(3);

            MainProgram<T> newMain1;
            MainProgram<T> newMain2;
            switch (typeOfNodesToSwap)
            {
                case 0: 
                    (newMain1,newMain2) = PerformCrossover<string>(main, main2);
                    parent.SetMain(mainToMutate, newMain1);
                    parent2.SetMain(main2ToMutate, newMain2);
                    break;
                case 1: 
                    (newMain1,newMain2) = PerformCrossover<double>(main, main2);
                    parent.SetMain(mainToMutate, newMain1);
                    parent2.SetMain(main2ToMutate, newMain2);
                    break;
                case 2: 
                    (newMain1,newMain2) = PerformCrossover<bool>(main, main2);
                    parent.SetMain(mainToMutate, newMain1);
                    parent2.SetMain(main2ToMutate, newMain2);
                    break;

                default: throw new Exception("Crossover could not select type of node to replace");
            }

            return new List<string>() {parent.GetId(), parent2.GetId()};
        }
        
        private (MainProgram<T> main, MainProgram<T> main2) PerformCrossover<TU>(MainProgram<T> main, MainProgram<T> main2) where TU : IComparable
        {
            var (nodesOfTypeInMain, nodesOfTypeInMain2) = GetNodesFromMainsOfType<TU>(main,main2);

            var (chosenNodeToSwap, chosenNodeToSwap2) = SelectNodes(nodesOfTypeInMain, nodesOfTypeInMain2);

            if (chosenNodeToSwap is null || chosenNodeToSwap2 is null)
                return (main, main2);
            
            var copy = chosenNodeToSwap.GetCopy();
            var copy2 = chosenNodeToSwap2.GetCopy();

            var chosenNodeParent = (ChildManager) chosenNodeToSwap.Parent;
            var chosenNode2Parent = (ChildManager) chosenNodeToSwap2.Parent;
            
            chosenNodeParent.SetChild(chosenNodeToSwap,copy2);
            chosenNode2Parent.SetChild(chosenNodeToSwap2,copy);
            
            return (main,main2);

        }

        private (INode<TU>, INode<TU>) SelectNodes<TU>(List<INode<TU>> firstNodes, List<INode<TU>> secondNodes) where TU : IComparable
        {
            //Low chance that tree does not contain, no-op
            if (firstNodes.Count == 0 || secondNodes.Count == 0)
                return (null,null);

            var chosenNodeToSwap = RandomNumberFactory.SelectFromList(firstNodes);
                
            //Root node can't be swapped
            if (chosenNodeToSwap.Parent == null)
                return (null,null);

            //Check if it is a comparator, they need to be replaced with another comparator
            List<INode<TU>> secondMatchingNodes;
            if (chosenNodeToSwap is NodeComparator<TU>)
            {
                secondMatchingNodes = secondNodes
                    .OfType<NodeComparator<TU>>()
                    .Select(c => (INode<TU>)c).ToList();
            }
            else
            {
                secondMatchingNodes = secondNodes.Where( n => n is not NodeComparator<TU>).ToList();
            }

            if (secondMatchingNodes.Count == 0)
                return (null,null);

            var chosenNodeToSwap2 = RandomNumberFactory.SelectFromList(secondMatchingNodes);

            return chosenNodeToSwap2?.Parent is null ? (null,null) : (chosenNodeToSwap, chosenNodeToSwap2);
        }
        
        private static (List<INode<TU>>, List<INode<TU>>) GetNodesFromMainsOfType<TU>(MainProgram<T> main, MainProgram<T> main2) where TU : IComparable
        {
            var nodeCollector1 = new NodeCollectorVisitor<TU>();
            var nodeCollector2 = new NodeCollectorVisitor<TU>();

            main.Visit(nodeCollector1);
            main2.Visit(nodeCollector2);

            return (nodeCollector1.GetNodes(), nodeCollector2.GetNodes());
        }

        protected override int GetNumberOfOffspringToProduce(ICollection parents)
        {
            var numberOfOffspringToProduce = base.GetNumberOfOffspringToProduce(parents);

            //Ensure even number otherwise crossover cannot work
            if (numberOfOffspringToProduce % 2 != 0)
                return numberOfOffspringToProduce + 1;

            return numberOfOffspringToProduce;
        }
    }
}
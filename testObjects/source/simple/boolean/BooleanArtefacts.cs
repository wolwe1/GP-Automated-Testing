using testObjects.source.capture;

namespace testObjects.source.simple.boolean
{
    public class BooleanArtefacts
    {

        public CoverageResult<string> TrueOrNothing(bool activate)
        {
            var coverage = CoverageResultWrapper.SetupCoverage<string>("BooleanArtefacts","TrueOrNothing",3);
            
            coverage.AddStartNode(NodeType.Statement);
            const string message = "The statement was activated";

            coverage.AddNode(1,NodeType.If);
            if (activate)
            {
                coverage.AddNode(1,NodeType.Return);
                return coverage.SetResult(message);
            }
            coverage.AddNode(2,NodeType.Return);
            return coverage.SetResult("");
        }

        public CoverageResult<string> EitherOr(bool activate)
        {
            var coverage = CoverageResultWrapper.SetupCoverage<string>("BooleanArtefacts","EitherOr",7);
            
            coverage.AddStartNode(NodeType.Statement);
            const string message = "Either";
            coverage.AddNode(1,NodeType.Statement);
            const string messageTwo = "Or";
            coverage.AddNode(2,NodeType.Statement);
            string result;

            coverage.AddNode(3,NodeType.If);
            if (activate)
            {
                coverage.AddNode(4,NodeType.Statement);
                result = message;
            }
            else
            {
                coverage.AddNode(5,NodeType.Statement);
                result = messageTwo;
            }
            
            coverage.AddNode(6,NodeType.Return);
            return coverage.SetResult(result);
        }
        
        public CoverageResult<string> And(bool first, bool second)
        {
            var coverage = CoverageResultWrapper.SetupCoverage<string>("BooleanArtefacts","And",4);
            
            coverage.AddStartNode(NodeType.Statement);
            const string message = "The statement was activated";

            coverage.AddNode(1,NodeType.If);
            if (first && second)
            {
                coverage.AddNode(2,NodeType.Return);
                return coverage.SetResult(message);
            }

            coverage.AddNode(3,NodeType.Return);
            return coverage.SetResult("");
        }
        
        public CoverageResult<string> Or(bool first, bool second)
        {
            var coverage = CoverageResultWrapper.SetupCoverage<string>("BooleanArtefacts","Or",4);
            
            coverage.AddStartNode(NodeType.Statement);
            const string Message = "The statement was activated";

            coverage.AddNode(1,NodeType.If);
            if (first || second)
            {
                coverage.AddNode(2,NodeType.Return);
                return coverage.SetResult(Message);
            }
            coverage.AddNode(3,NodeType.Return);
            return coverage.SetResult("");
        }
        
        public CoverageResult<string> AndOr(bool first, bool second)
        {
            var coverage = CoverageResultWrapper.SetupCoverage<string>("BooleanArtefacts","AndOr",9);
            
            coverage.AddStartNode(NodeType.Statement);
            var result = "Neither value were true";
            coverage.AddNode(1,NodeType.Statement);
            const string messageAnd = "Both values were true";
            coverage.AddNode(2,NodeType.Statement);
            const string messageOrOne = "The first value was true";
            coverage.AddNode(3,NodeType.Statement);
            const string messageOrTwo = "The second value was true";
            
            coverage.AddNode(4,NodeType.If);
            if (first && second)
            {
                coverage.AddNode(5,NodeType.Statement);
                result = messageAnd;
            }
            else if(first)
            {
                coverage.AddNode(6,NodeType.Statement);
                result = messageOrOne;
            }
            else if (second)
            {
                coverage.AddNode(7,NodeType.Statement);
                result = messageOrTwo;
            }
            
            coverage.AddEndNode(8,NodeType.Return);
            return coverage.SetResult(result);
        }
        
        public CoverageResult<string> Xor(bool first, bool second)
        {
            var coverage = CoverageResultWrapper.SetupCoverage<string>("BooleanArtefacts","Xor",5);
            
            coverage.AddStartNode(NodeType.If);
            if (first == false && second == false)
            {
                coverage.AddNode(1,NodeType.Return);
                return coverage.SetResult("True");
            }
            
            coverage.AddNode(2,NodeType.If);
            if (first && second)
            {
                coverage.AddNode(3,NodeType.Return);
                return coverage.SetResult("False");
            }
                 
            coverage.AddNode(4,NodeType.Return);
            return coverage.SetResult("True");
        }
    }
}
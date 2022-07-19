using testObjects.source.capture;

namespace testObjects.source.simple.numeric
{
    public class Remainder
    {
        //Difference?
        public CoverageResult<double> Get(double n, double m)
        {
            var coverage = CoverageResultWrapper.SetupCoverage<double>("Remainder","Get",14);

            //Be a nice user
            if (Math.Abs(n) > 20 || Math.Abs(m) > 20)
                return coverage.SetResult(-1);
                    
            coverage.AddStartNode(NodeType.If);
            if (Math.Abs(n - m) < 0.1)
            {
                coverage.AddNode(1,NodeType.Return);
                return coverage.SetResult(0);
            }

            coverage.AddNode(2,NodeType.Statement);
            var difference = 0d;
            
            coverage.AddNode(3,NodeType.If);
            if (n < m)
            {
                coverage.AddNode(4,NodeType.Loop);
                while ((n + difference )<m)
                {
                    coverage.AddNode(5,NodeType.Statement);
                    difference++;
                }

                coverage.AddNode(6,NodeType.If);
                if (difference > n)
                {
                    coverage.AddNode(7,NodeType.Return);
                    return coverage.SetResult(difference - n);
                }
                coverage.AddNode(8,NodeType.Return);
                return coverage.SetResult(difference);
            }
            
            coverage.AddNode(9,NodeType.Loop);
            while ((m + difference )<n)
            {
                coverage.AddNode(10,NodeType.Statement);
                difference++;
            }
            
            coverage.AddNode(11,NodeType.If);
            if (difference > m)
            {
                coverage.AddNode(12,NodeType.Return);
                return coverage.SetResult(difference - m);
            }
            
            coverage.AddNode(13,NodeType.Return);
            return coverage.SetResult(difference);
            
        }
    }
}
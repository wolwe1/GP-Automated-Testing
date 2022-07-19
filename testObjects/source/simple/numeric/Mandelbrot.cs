using System.Numerics;
using testObjects.source.capture;

namespace testObjects.source.simple.numeric
{
    public class Mandelbrot
    {
        private static int _maxIterations = 100;
        
        public CoverageResult<bool> Get(int x, int y)
        {
            var coverage = CoverageResultWrapper.SetupCoverage<bool>("Mandelbrot","Get",9);
            
            coverage.AddStartNode(NodeType.Statement);
            var comp = new Complex(x, y);
            coverage.AddNode(1,NodeType.Statement);
            var iterations = GetMandelbrot(comp,coverage);
            //Console.WriteLine($"{comp}{iterations}");

            coverage.AddEndNode(8,NodeType.Return);
            return coverage.SetResult(iterations < _maxIterations);
        }

        private int GetMandelbrot(Complex c, CoverageResult<bool> coverage)
        {
            coverage.AddNode(2,NodeType.Statement);
            var z = Complex.Zero;
            coverage.AddNode(3,NodeType.Statement);
            var n = 0;
            
            coverage.AddNode(4,NodeType.Loop);
            while( Complex.Abs(z) <= 2 && n < _maxIterations)
            {
                coverage.AddNode(5,NodeType.Statement);
                z = Complex.Add((z * z), c);
                coverage.AddNode(6,NodeType.Statement);
                n += 1;
            }

            coverage.AddNode(7,NodeType.Return);
            return n;
        }
    }
}
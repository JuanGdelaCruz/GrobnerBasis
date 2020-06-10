using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GröbnerBasis.PolynomialRings
{
    public class Ideal
    {
        public Polynomial[] GeneratorSet { get; private set; }
        private Ring ring;


        public Ideal(Polynomial[] generator, Ring ring)
        {
            GeneratorSet = generator;
            this.ring = ring;
        }

        //Buchberger´s algorithm
        public Polynomial[] GröbnerBasis(CancellationToken token = default, IProgress<int> progress = null)
        {
            List<Polynomial> G = GeneratorSet.Select(polynomial => polynomial.Clone()).ToList();
            var combinations = G.SelectMany((x, i) => G.Skip(i + 1), (x, y) => Tuple.Create(x, y)).ToList();

            //These two variables will be used to report the current progress of the algorithm
            int combinationsProcessed = 0;
            int totalCombinations = G.Count;

            while (combinations.Count != 0)
            {

                if (progress != null)
                {
                    var value = (int) ((double)combinationsProcessed / totalCombinations* 100);
                    if (value > 100) value = 100;
                    progress.Report(value);
                    combinationsProcessed++;
                }
                if (token != CancellationToken.None && token.IsCancellationRequested)
                    throw new OperationCanceledException();
                var tuple = combinations.First();
                combinations.Remove(tuple);
              
                var sPol = ring.SPolynomial(tuple.Item1, tuple.Item2);
                var division = ring.Divide(sPol, G.ToArray(),token);
                Polynomial remainder = division.Item2;
                if (!remainder.IsZero())
                {
                    var newCombinations = G.Select(x => Tuple.Create(x, remainder));
                   

                    combinations.AddRange(newCombinations);
                    G.Add(remainder);

                    totalCombinations += newCombinations.Count();
                }
            }

            G.ForEach(p => p.ReduceCoefficients());
                     return G.ToArray();
        }

        public Polynomial[] MinimalGröbnerBasis(CancellationToken token = default, IProgress<int> progress = null)
        {
            var grobnerBasis = GröbnerBasis(token, progress);
     
            var minimal = new List<Polynomial>(grobnerBasis);

            for (int i = 0; i < grobnerBasis.Length; i++)
            {
                if (grobnerBasis[i] == null)
                    continue;
                for (int j = 0; j < grobnerBasis.Length; j++)
                {
                    if (grobnerBasis[j] == null)
                        continue;
                    if (i != j)
                    {
                        if (grobnerBasis[i].Divides(grobnerBasis[j]))
                        {
                            minimal.Remove(grobnerBasis[j]);
                            grobnerBasis[j] = null;
                        }
                    }
                }

            }
            return minimal.ToArray();
        }

        public Polynomial[] ReducedGrobnerBasis(CancellationToken token = default, IProgress<int> progress = null)
        {
            var minimal = MinimalGröbnerBasis(token,progress).ToList();
            if (minimal == null)
                return null;

            for (int i = 0; i < minimal.Count; i++)
            {
                var g = minimal[i];
                minimal.Remove(g);
                var h = ring.Divide(g, minimal.ToArray()).Item2;
                minimal.Insert(i, h);
            }


            return minimal.ToArray();
        }


        public bool Member(Polynomial polynomial)
        {
            return ring.Divide(polynomial, ReducedGrobnerBasis()).Item2.IsZero();
        }

    }
}

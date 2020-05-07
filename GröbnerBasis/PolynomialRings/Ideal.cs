using System;
using System.Collections.Generic;
using System.Linq;

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
        public Polynomial[] GröbnerBasis()
        {
            List<Polynomial> G = GeneratorSet.Select(polynomial => polynomial.Clone()).ToList();
            var combinations = G.SelectMany((x, i) => G.Skip(i + 1), (x, y) => Tuple.Create(x, y)).ToList();
            while (combinations.Count != 0)
            {
                var tuple = combinations.First();
                combinations.Remove(tuple);
                var sPol = ring.SPolynomial(tuple.Item1, tuple.Item2);
                var division = ring.Divide(sPol, G.ToArray());
                Polynomial remainder = division.Item2;

                if (!remainder.IsZero())
                {
                    G.Add(remainder);

                    combinations = G.SelectMany((x, i) => G.Skip(i + 1), (x, y) => Tuple.Create(x, y)).ToList();

                }
            }

            return G.ToArray();
        }

    }
}

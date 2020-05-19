﻿using System;
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

            G.ForEach(p => p.ReduceCoefficients());
            return G.ToArray();
        }

        public Polynomial[] MinimalGröbnerBasis()
        {
            var grobnerBasis = GröbnerBasis();
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

        public Polynomial[] ReducedGrobnerBasis()
        {
            var minimal = MinimalGröbnerBasis().ToList();
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

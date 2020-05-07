using GröbnerBasis.PolynomialRings;
using GröbnerBasis.PolynomialRings.Fields;
using GröbnerBasis.PolynomialRings.Order;
using System;

namespace GröbnerBasis
{
    class Program
    {
        static void Main(string[] args)
        {

            Ring r = new Ring(new string[] { "x", "y" });
            r.FixOrder(new string[] { "y", "x" });



            Polynomial f1 = new Polynomial(r);
            f1.AddTerm(1, new int[] { 0, 2 });
            f1.AddTerm(1, new int[] { 1, 1 });
            f1.AddTerm(1, new int[] { 2, 0 });


            Polynomial f2 = new Polynomial(r);
            f2.AddTerm(1, new int[] { 0, 1 });
            f2.AddTerm(1, new int[] { 1, 0 });

            Polynomial f3 = new Polynomial(r);
            f3.AddTerm(1, new int[] { 0, 1 });

            Ideal I = new Ideal(new Polynomial[] { f1, f2, f3 }, r);

            var gb = I.GröbnerBasis();
            foreach (var p in gb)
                Console.WriteLine(p);



            Console.Read();

        }

    }
}

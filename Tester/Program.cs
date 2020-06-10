using GröbnerBasis.PolynomialRings;
using GröbnerBasis.PolynomialRings.Fields;
using GröbnerBasis.PolynomialRings.Order;
using System;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {

            Test17();
            return;

            Console.WriteLine("_________________TEST 0__________________________");
            Test0();
            Console.WriteLine("_________________TEST 1__________________________");
            Test1();
            Console.WriteLine("_________________TEST 3__________________________");
            Test3();
            Console.WriteLine("_________________TEST 4__________________________");
            Test4();
            return;

            Ring r = new Ring(Field.Real, new string[] { "x", "y", "z" });
            r.FixOrder(new string[] { "x", "y", "z" });

            Polynomial f1 = new Polynomial(r);
            f1.AddTerm(1, new int[] { 0, 1, 2 });
            f1.AddTerm(1, new int[] { 0, 0, 2 });

            Polynomial f2 = new Polynomial(r);
            f2.AddTerm(1, new int[] { 3, 1, 0 });
            f2.AddTerm(1, new int[] { 1, 0, 0 });
            f2.AddTerm(1, new int[] { 0, 1, 0 });
            f2.AddTerm(1, new int[] { 0, 0, 0 });

            Polynomial f3 = new Polynomial(r);
            f3.AddTerm(1, new int[] { 0, 0, 1 });
            f3.AddTerm(1, new int[] { 2, 0, 0 });
            f3.AddTerm(1, new int[] { 0, 3, 0 });

            Ideal I = new Ideal(new Polynomial[] { f1, f2, f3 }, r);


            Console.WriteLine("__________Gröbner Basis__________________");
            var gb = I.GröbnerBasis();
            foreach (var p in gb)
                Console.WriteLine(p);

            Console.WriteLine("_________Minimal Gröbner Basis___________________");

            var minimal = I.MinimalGröbnerBasis();
            foreach (var p in minimal)
                Console.WriteLine(p);

            Console.WriteLine("______________Reduced Gröbner Basis______________");


            var reduced = I.ReducedGrobnerBasis();
            foreach (var p in reduced)
                Console.WriteLine(p);

            Console.WriteLine("__________Membership__________________");
            Console.WriteLine(I.Member(f1));
            Console.Read();

        }


        static void Test0()
        {
            Ring r = new Ring(Field.Real, new string[] { "x", "y" });

            r.FixOrder(new string[] { "y", "x" });


            Polynomial f1 = new Polynomial(r);
            f1.AddTerm(1, new int[] { 1, 1 });
            f1.AddTerm(-1, new int[] { 1, 0 });

            Polynomial f2 = new Polynomial(r);
            f2.AddTerm(-1, new int[] { 0, 1 });
            f2.AddTerm(1, new int[] { 2, 0 });

            Ideal I = new Ideal(new Polynomial[] { f1, f2 }, r);

            Console.WriteLine("__________Gröbner Basis__________________");
            var gb = I.GröbnerBasis();
            foreach (var p in gb)
                Console.WriteLine(p);

            Console.WriteLine("_________Minimal Gröbner Basis___________________");

            var minimal = I.MinimalGröbnerBasis();
            foreach (var p in minimal)
                Console.WriteLine(p);

            Console.WriteLine("______________Reduced Gröbner Basis______________");


            var reduced = I.ReducedGrobnerBasis();
            foreach (var p in reduced)
                Console.WriteLine(p);

            Console.WriteLine("__________Membership__________________");
            Console.WriteLine(I.Member(f1));
        }

        static void Test1()
        {
            Ring r = new Ring(Field.Real, new string[] { "x", "y" });

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

            Console.WriteLine("__________Gröbner Basis__________________");
            var gb = I.GröbnerBasis();
            foreach (var p in gb)
                Console.WriteLine(p);

            Console.WriteLine("_________Minimal Gröbner Basis___________________");

            var minimal = I.MinimalGröbnerBasis();
            foreach (var p in minimal)
                Console.WriteLine(p);

            Console.WriteLine("______________Reduced Gröbner Basis______________");


            var reduced = I.ReducedGrobnerBasis();
            foreach (var p in reduced)
                Console.WriteLine(p);

            Console.WriteLine("__________Membership__________________");
            Console.WriteLine(I.Member(f1));
        }

        //Graph with 8 nodes
        static void Test2()
        {
            Ring ring = new Ring(Field.Real, new string[] { "x1", "x2", "x3", "x4", "x5", "x6", "x7", "x8" });
            ring.FixOrder(new string[] { "x1", "x2", "x3", "x4", "x5", "x6", "x7", "x8" });


            Polynomial f1 = new Polynomial(ring);
            f1.AddTerm(1, new int[] { 3, 0, 0, 0, 0, 0, 0, 0 });
            f1.AddTerm(-1, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            Polynomial f2 = new Polynomial(ring);
            f2.AddTerm(1, new int[] { 0, 3, 0, 0, 0, 0, 0, 0 });
            f2.AddTerm(-1, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            Polynomial f3 = new Polynomial(ring);
            f3.AddTerm(1, new int[] { 0, 0, 3, 0, 0, 0, 0, 0 });
            f3.AddTerm(-1, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            Polynomial f4 = new Polynomial(ring);
            f4.AddTerm(1, new int[] { 0, 0, 0, 3, 0, 0, 0, 0 });
            f4.AddTerm(-1, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            Polynomial f5 = new Polynomial(ring);
            f5.AddTerm(1, new int[] { 0, 0, 0, 0, 3, 0, 0, 0 });
            f5.AddTerm(-1, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            Polynomial f6 = new Polynomial(ring);
            f6.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 3, 0, 0 });
            f6.AddTerm(-1, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            Polynomial f7 = new Polynomial(ring);
            f7.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 0, 3, 0 });
            f7.AddTerm(-1, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            Polynomial f8 = new Polynomial(ring);
            f8.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 0, 0, 3 });
            f8.AddTerm(-1, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            Polynomial f9 = new Polynomial(ring);
            f9.AddTerm(1, new int[] { 2, 0, 0, 0, 0, 0, 0, 0 });
            f9.AddTerm(1, new int[] { 1, 1, 0, 0, 0, 0, 0, 0 });
            f9.AddTerm(1, new int[] { 0, 2, 0, 0, 0, 0, 0, 0 });


            Polynomial f10 = new Polynomial(ring);
            f10.AddTerm(1, new int[] { 2, 0, 0, 0, 0, 0, 0, 0 });
            f10.AddTerm(1, new int[] { 1, 0, 0, 0, 1, 0, 0, 0 });
            f10.AddTerm(1, new int[] { 0, 0, 0, 0, 2, 0, 0, 0 });

            Polynomial f11 = new Polynomial(ring);
            f11.AddTerm(1, new int[] { 2, 0, 0, 0, 0, 0, 0, 0 });
            f11.AddTerm(1, new int[] { 1, 0, 0, 0, 0, 1, 0, 0 });
            f11.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 2, 0, 0 });


            Polynomial f12 = new Polynomial(ring);
            f12.AddTerm(1, new int[] { 0, 2, 0, 0, 0, 0, 0, 0 });
            f12.AddTerm(1, new int[] { 0, 1, 0, 0, 0, 0, 0, 1 });
            f12.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 0, 0, 2 });

            Polynomial f13 = new Polynomial(ring);
            f13.AddTerm(1, new int[] { 0, 2, 0, 0, 0, 0, 0, 0 });
            f13.AddTerm(1, new int[] { 0, 1, 1, 0, 0, 0, 0, 0 });
            f13.AddTerm(1, new int[] { 0, 0, 2, 0, 0, 0, 0, 0 });

            Polynomial f14 = new Polynomial(ring);
            f14.AddTerm(1, new int[] { 0, 2, 0, 0, 0, 0, 0, 0 });
            f14.AddTerm(1, new int[] { 0, 1, 0, 1, 0, 0, 0, 0 });
            f14.AddTerm(1, new int[] { 0, 0, 0, 2, 0, 0, 0, 0 });

            Polynomial f15 = new Polynomial(ring);
            f15.AddTerm(1, new int[] { 0, 0, 2, 0, 0, 0, 0, 0 });
            f15.AddTerm(1, new int[] { 0, 0, 1, 0, 0, 0, 0, 1 });
            f15.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 0, 0, 2 });

            Polynomial f16 = new Polynomial(ring);
            f16.AddTerm(1, new int[] { 0, 0, 2, 0, 0, 0, 0, 0 });
            f16.AddTerm(1, new int[] { 0, 0, 1, 1, 0, 0, 0, 0 });
            f16.AddTerm(1, new int[] { 0, 0, 0, 2, 0, 0, 0, 0 });

            Polynomial f17 = new Polynomial(ring);
            f17.AddTerm(1, new int[] { 0, 0, 0, 2, 0, 0, 0, 0 });
            f17.AddTerm(1, new int[] { 0, 0, 0, 1, 1, 0, 0, 0 });
            f17.AddTerm(1, new int[] { 0, 0, 0, 0, 2, 0, 0, 0 });

            Polynomial f18 = new Polynomial(ring);
            f18.AddTerm(1, new int[] { 0, 0, 0, 2, 0, 0, 0, 0 });
            f18.AddTerm(1, new int[] { 0, 0, 0, 1, 0, 0, 1, 0 });
            f18.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 0, 2, 0 });

            Polynomial f19 = new Polynomial(ring);
            f19.AddTerm(1, new int[] { 0, 0, 0, 0, 2, 0, 0, 0 });
            f19.AddTerm(1, new int[] { 0, 0, 0, 0, 1, 0, 1, 0 });
            f19.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 0, 2, 0 });

            Polynomial f20 = new Polynomial(ring);
            f20.AddTerm(1, new int[] { 0, 0, 0, 0, 2, 0, 0, 0 });
            f20.AddTerm(1, new int[] { 0, 0, 0, 0, 1, 1, 0, 0 });
            f20.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 2, 0, 0 });

            Polynomial f21 = new Polynomial(ring);
            f21.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 2, 0, 0 });
            f21.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 1, 1, 0 });
            f21.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 0, 2, 0 });

            Polynomial f22 = new Polynomial(ring);
            f22.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 2, 0, 0 });
            f22.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 1, 0, 1 });
            f22.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 0, 0, 2 });

            Ideal I = new Ideal(new Polynomial[] { f1, f2, f3, f4, f5, f6, f7, f8, f9, f10 }, ring);
            Console.WriteLine("__________Gröbner Basis__________________");
            var gb = I.GröbnerBasis();
            foreach (var p in gb)
                Console.WriteLine(p);

            Console.WriteLine("_________Minimal Gröbner Basis___________________");

            var minimal = I.MinimalGröbnerBasis();
            foreach (var p in minimal)
                Console.WriteLine(p);

            Console.WriteLine("______________Reduced Gröbner Basis______________");


            var reduced = I.ReducedGrobnerBasis();
            foreach (var p in reduced)
                Console.WriteLine(p);

            Console.WriteLine("__________Membership__________________");
            Console.WriteLine(I.Member(f1));

        }


        static void Test3()
        {
            Ring r = new Ring(Field.Real, new string[] { "x", "y" });

            r.FixOrder(new string[] { "y", "x" });


            Polynomial f1 = new Polynomial(r);
            f1.AddTerm(1, new int[] { 1, 1 });
            f1.AddTerm(-1, new int[] { 1, 0 });


            Polynomial f2 = new Polynomial(r);
            f2.AddTerm(1, new int[] { 0, 1 });
            f2.AddTerm(-1, new int[] { 2, 0 });

            Polynomial f3 = new Polynomial(r);
            f3.AddTerm(1, new int[] { 3, 0 });
            f3.AddTerm(-1, new int[] { 1, 0 });



            Ideal I = new Ideal(new Polynomial[] { f2, f3 }, r);
            Console.WriteLine("__________Gröbner Basis__________________");
            var gb = I.GröbnerBasis();
            foreach (var p in gb)
                Console.WriteLine(p);

            Console.WriteLine("_________Minimal Gröbner Basis___________________");

            var minimal = I.MinimalGröbnerBasis();
            foreach (var p in minimal)
                Console.WriteLine(p);

            Console.WriteLine("______________Reduced Gröbner Basis______________");


            var reduced = I.ReducedGrobnerBasis();
            foreach (var p in reduced)
                Console.WriteLine(p);

            Console.WriteLine("__________Membership__________________");
            Console.WriteLine(I.Member(f1));
        }

        //Graph with 3 nodes and 3-colorable
        static void Test4()
        {
            Ring r = new Ring(Field.Real, new string[] { "x", "y", "z" });

            r.FixOrder(new string[] { "x", "y", "z" });


            Polynomial f1 = new Polynomial(r);
            f1.AddTerm(1, new int[] { 3, 0, 0 });
            f1.AddTerm(-1, new int[] { 0, 0, 0 });


            Polynomial f2 = new Polynomial(r);
            f2.AddTerm(1, new int[] { 0, 3, 0 });
            f2.AddTerm(-1, new int[] { 0, 0, 0 });

            Polynomial f3 = new Polynomial(r);
            f3.AddTerm(1, new int[] { 0, 0, 3 });
            f3.AddTerm(-1, new int[] { 0, 0, 0 });

            Polynomial f4 = new Polynomial(r);
            f4.AddTerm(1, new int[] { 0, 2, 0 });
            f4.AddTerm(1, new int[] { 0, 1, 1 });
            f4.AddTerm(1, new int[] { 0, 0, 2 });

            Polynomial f5 = new Polynomial(r);
            f5.AddTerm(1, new int[] { 2, 0, 0 });
            f5.AddTerm(1, new int[] { 1, 0, 1 });
            f5.AddTerm(1, new int[] { 0, 0, 2 });

            Polynomial f6 = new Polynomial(r);
            f6.AddTerm(1, new int[] { 2, 0, 0 });
            f6.AddTerm(1, new int[] { 1, 1, 0 });
            f6.AddTerm(1, new int[] { 0, 2, 0 });

            Ideal I = new Ideal(new Polynomial[] { f1, f2, f3, f6, f5, f4 }, r);
            Console.WriteLine("__________Gröbner Basis__________________");
            var gb = I.GröbnerBasis();
            foreach (var p in gb)
                Console.WriteLine(p);

            Console.WriteLine("_________Minimal Gröbner Basis___________________");

            var minimal = I.MinimalGröbnerBasis();
            foreach (var p in minimal)
                Console.WriteLine(p);

            Console.WriteLine("______________Reduced Gröbner Basis______________");


            var reduced = I.ReducedGrobnerBasis();
            foreach (var p in reduced)
                Console.WriteLine(p);

            Console.WriteLine("__________Membership__________________");
            Console.WriteLine(I.Member(f1));
        }


        //Division Test for a case in the previous test
        static void Test5()
        {
            Ring r = new Ring(Field.Real, new string[] { "x", "y", "z" });

            r.FixOrder(new string[] { "x", "y", "z" });


            Polynomial f1 = new Polynomial(r);
            f1.AddTerm(1, new int[] { 3, 0, 0 });
            f1.AddTerm(-1, new int[] { 0, 0, 0 });


            Polynomial f2 = new Polynomial(r);
            f2.AddTerm(1, new int[] { 0, 3, 0 });
            f2.AddTerm(-1, new int[] { 0, 0, 0 });

            Polynomial f3 = new Polynomial(r);
            f3.AddTerm(1, new int[] { 0, 0, 3 });
            f3.AddTerm(-1, new int[] { 0, 0, 0 });

            Polynomial f4 = new Polynomial(r);
            f4.AddTerm(1, new int[] { 0, 2, 0 });
            f4.AddTerm(1, new int[] { 0, 1, 1 });
            f4.AddTerm(1, new int[] { 0, 0, 2 });

            Polynomial f5 = new Polynomial(r);
            f5.AddTerm(1, new int[] { 2, 0, 0 });
            f5.AddTerm(1, new int[] { 1, 0, 1 });
            f5.AddTerm(1, new int[] { 0, 0, 2 });

            Polynomial f6 = new Polynomial(r);
            f6.AddTerm(1, new int[] { 2, 0, 0 });
            f6.AddTerm(1, new int[] { 1, 1, 0 });
            f6.AddTerm(1, new int[] { 0, 2, 0 });


            Polynomial dividend = new Polynomial(r);
            dividend.AddTerm(-1, new int[] { 2, 1, 0 });
            dividend.AddTerm(-1, new int[] { 1, 2, 0 });
            dividend.AddTerm(-1, new int[] { 0, 0, 0 });


            Console.WriteLine("---------------------------------");
            var res = r.Divide(dividend, new Polynomial[] { f1, f2, f3, f6, f5, f4 });
            Console.WriteLine(res.Item2);
            Console.WriteLine("---------------------------------");


        }
        //Graph with 4 nodes and 3-colorable
        static void Test6()
        {
            Ring r = new Ring(Field.Real, new string[] { "x", "y", "z", "t" });

            r.FixOrder(new string[] { "x", "y", "z", "t" });


            Polynomial f1 = new Polynomial(r);
            f1.AddTerm(1, new int[] { 3, 0, 0, 0 });
            f1.AddTerm(-1, new int[] { 0, 0, 0, 0 });


            Polynomial f2 = new Polynomial(r);
            f2.AddTerm(1, new int[] { 0, 3, 0, 0 });
            f2.AddTerm(-1, new int[] { 0, 0, 0, 0 });

            Polynomial f3 = new Polynomial(r);
            f3.AddTerm(1, new int[] { 0, 0, 3, 0 });
            f3.AddTerm(-1, new int[] { 0, 0, 0, 0 });

            Polynomial f4 = new Polynomial(r);
            f4.AddTerm(1, new int[] { 0, 2, 0, 0 });
            f4.AddTerm(1, new int[] { 0, 1, 1, 0 });
            f4.AddTerm(1, new int[] { 0, 0, 2, 0 });

            Polynomial f5 = new Polynomial(r);
            f5.AddTerm(1, new int[] { 2, 0, 0, 0 });
            f5.AddTerm(1, new int[] { 1, 0, 1, 0 });
            f5.AddTerm(1, new int[] { 0, 0, 2, 0 });

            Polynomial f6 = new Polynomial(r);
            f6.AddTerm(1, new int[] { 2, 0, 0, 0 });
            f6.AddTerm(1, new int[] { 1, 1, 0, 0 });
            f6.AddTerm(1, new int[] { 0, 2, 0, 0 });



            Polynomial f7 = new Polynomial(r);
            f7.AddTerm(1, new int[] { 0, 0, 0, 3 });
            f7.AddTerm(-1, new int[] { 0, 0, 0, 0 });

            Polynomial f8 = new Polynomial(r);
            f8.AddTerm(1, new int[] { 2, 0, 0, 0 });
            f8.AddTerm(1, new int[] { 1, 0, 0, 1 });
            f8.AddTerm(1, new int[] { 0, 0, 0, 2 });


            Ideal I = new Ideal(new Polynomial[] { f1, f2, f3, f6, f5, f4, f7, f8 }, r);
            Console.WriteLine("__________Gröbner Basis__________________");
            var gb = I.GröbnerBasis();
            foreach (var p in gb)
                Console.WriteLine(p);

            Console.WriteLine("_________Minimal Gröbner Basis___________________");

            var minimal = I.MinimalGröbnerBasis();
            foreach (var p in minimal)
                Console.WriteLine(p);

            Console.WriteLine("______________Reduced Gröbner Basis______________");


            var reduced = I.ReducedGrobnerBasis();
            foreach (var p in reduced)
                Console.WriteLine(p);

            Console.WriteLine("__________Membership__________________");
            Console.WriteLine(I.Member(f1));
        }


        //Graph with 4 nodes and 3-colorable
        static void Test7()
        {
            Ring r = new Ring(Field.Real, new string[] { "x", "y", "z", "t" });

            r.FixOrder(new string[] { "x", "y", "z", "t" });


            Polynomial f1 = new Polynomial(r);
            f1.AddTerm(1, new int[] { 3, 0, 0, 0 });
            f1.AddTerm(-1, new int[] { 0, 0, 0, 0 });


            Polynomial f2 = new Polynomial(r);
            f2.AddTerm(1, new int[] { 0, 3, 0, 0 });
            f2.AddTerm(-1, new int[] { 0, 0, 0, 0 });

            Polynomial f3 = new Polynomial(r);
            f3.AddTerm(1, new int[] { 0, 0, 3, 0 });
            f3.AddTerm(-1, new int[] { 0, 0, 0, 0 });

            Polynomial f4 = new Polynomial(r);
            f4.AddTerm(1, new int[] { 0, 2, 0, 0 });
            f4.AddTerm(1, new int[] { 0, 1, 1, 0 });
            f4.AddTerm(1, new int[] { 0, 0, 2, 0 });

            Polynomial f5 = new Polynomial(r);
            f5.AddTerm(1, new int[] { 2, 0, 0, 0 });
            f5.AddTerm(1, new int[] { 1, 0, 1, 0 });
            f5.AddTerm(1, new int[] { 0, 0, 2, 0 });

            Polynomial f6 = new Polynomial(r);
            f6.AddTerm(1, new int[] { 2, 0, 0, 0 });
            f6.AddTerm(1, new int[] { 1, 1, 0, 0 });
            f6.AddTerm(1, new int[] { 0, 2, 0, 0 });



            Polynomial f7 = new Polynomial(r);
            f7.AddTerm(1, new int[] { 0, 0, 0, 3 });
            f7.AddTerm(-1, new int[] { 0, 0, 0, 0 });

            Polynomial f8 = new Polynomial(r);
            f8.AddTerm(1, new int[] { 2, 0, 0, 0 });
            f8.AddTerm(1, new int[] { 1, 0, 0, 1 });
            f8.AddTerm(1, new int[] { 0, 0, 0, 2 });

            Polynomial f9 = new Polynomial(r);
            f9.AddTerm(1, new int[] { 0, 0, 2, 0 });
            f9.AddTerm(1, new int[] { 0, 0, 1, 1 });
            f9.AddTerm(1, new int[] { 0, 0, 0, 2 });



            Ideal I = new Ideal(new Polynomial[] { f1, f2, f3, f6, f5, f4, f7, f8, f9 }, r);
            Console.WriteLine("__________Gröbner Basis__________________");
            var gb = I.GröbnerBasis();
            foreach (var p in gb)
                Console.WriteLine(p);

            Console.WriteLine("_________Minimal Gröbner Basis___________________");

            var minimal = I.MinimalGröbnerBasis();
            foreach (var p in minimal)
                Console.WriteLine(p);

            Console.WriteLine("______________Reduced Gröbner Basis______________");


            var reduced = I.ReducedGrobnerBasis();
            foreach (var p in reduced)
                Console.WriteLine(p);

            Console.WriteLine("__________Membership__________________");
            Console.WriteLine(I.Member(f1));
        }

        //Graph with 4 nodes and not 3-colorable
        static void Test8()
        {
            Ring r = new Ring(Field.Real, new string[] { "x", "y", "z", "t" });

            r.FixOrder(new string[] { "x", "y", "z", "t" });


            Polynomial f1 = new Polynomial(r);
            f1.AddTerm(1, new int[] { 3, 0, 0, 0 });
            f1.AddTerm(-1, new int[] { 0, 0, 0, 0 });


            Polynomial f2 = new Polynomial(r);
            f2.AddTerm(1, new int[] { 0, 3, 0, 0 });
            f2.AddTerm(-1, new int[] { 0, 0, 0, 0 });

            Polynomial f3 = new Polynomial(r);
            f3.AddTerm(1, new int[] { 0, 0, 3, 0 });
            f3.AddTerm(-1, new int[] { 0, 0, 0, 0 });

            Polynomial f4 = new Polynomial(r);
            f4.AddTerm(1, new int[] { 0, 2, 0, 0 });
            f4.AddTerm(1, new int[] { 0, 1, 1, 0 });
            f4.AddTerm(1, new int[] { 0, 0, 2, 0 });

            Polynomial f5 = new Polynomial(r);
            f5.AddTerm(1, new int[] { 2, 0, 0, 0 });
            f5.AddTerm(1, new int[] { 1, 0, 1, 0 });
            f5.AddTerm(1, new int[] { 0, 0, 2, 0 });

            Polynomial f6 = new Polynomial(r);
            f6.AddTerm(1, new int[] { 2, 0, 0, 0 });
            f6.AddTerm(1, new int[] { 1, 1, 0, 0 });
            f6.AddTerm(1, new int[] { 0, 2, 0, 0 });



            Polynomial f7 = new Polynomial(r);
            f7.AddTerm(1, new int[] { 0, 0, 0, 3 });
            f7.AddTerm(-1, new int[] { 0, 0, 0, 0 });

            Polynomial f8 = new Polynomial(r);
            f8.AddTerm(1, new int[] { 2, 0, 0, 0 });
            f8.AddTerm(1, new int[] { 1, 0, 0, 1 });
            f8.AddTerm(1, new int[] { 0, 0, 0, 2 });

            Polynomial f9 = new Polynomial(r);
            f9.AddTerm(1, new int[] { 0, 0, 2, 0 });
            f9.AddTerm(1, new int[] { 0, 0, 1, 1 });
            f9.AddTerm(1, new int[] { 0, 0, 0, 2 });


            Polynomial f10 = new Polynomial(r);
            f10.AddTerm(1, new int[] { 0, 2, 0, 0 });
            f10.AddTerm(1, new int[] { 0, 1, 0, 1 });
            f10.AddTerm(1, new int[] { 0, 0, 0, 2 });


            Ideal I = new Ideal(new Polynomial[] { f1, f2, f6, f3, f9, f4, f5, f7, f10, f8 }, r);

            Console.WriteLine("__________Generator_________________");
            var sys = I.GeneratorSet;
            foreach (var p in sys)
                Console.WriteLine(p);


            Console.WriteLine("__________Gröbner Basis__________________");
            var gb = I.GröbnerBasis();
            foreach (var p in gb)
                Console.WriteLine(p);

            Console.WriteLine("_________Minimal Gröbner Basis___________________");

            var minimal = I.MinimalGröbnerBasis();
            foreach (var p in minimal)
                Console.WriteLine(p);

            Console.WriteLine("______________Reduced Gröbner Basis______________");


            var reduced = I.ReducedGrobnerBasis();
            foreach (var p in reduced)
                Console.WriteLine(p);

            Console.WriteLine("__________Membership__________________");
            Console.WriteLine(I.Member(f1));
        }

        static void Test9()
        {
            Ring ring = new Ring(Field.Real, new string[] { "x1", "x2", "x3", "x4", "x5", "x6", "x7", "x8" });
            ring.FixOrder(new string[] { "x1", "x2", "x3", "x4", "x5", "x6", "x7", "x8" });


            Polynomial f1 = new Polynomial(ring);
            f1.AddTerm(1, new int[] { 3, 0, 0, 0, 0, 0, 0, 0 });
            f1.AddTerm(-1, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            Polynomial f2 = new Polynomial(ring);
            f2.AddTerm(1, new int[] { 0, 3, 0, 0, 0, 0, 0, 0 });
            f2.AddTerm(-1, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            Polynomial f3 = new Polynomial(ring);
            f3.AddTerm(1, new int[] { 0, 0, 3, 0, 0, 0, 0, 0 });
            f3.AddTerm(-1, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            Polynomial f4 = new Polynomial(ring);
            f4.AddTerm(1, new int[] { 0, 0, 0, 3, 0, 0, 0, 0 });
            f4.AddTerm(-1, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            Polynomial f5 = new Polynomial(ring);
            f5.AddTerm(1, new int[] { 0, 0, 0, 0, 3, 0, 0, 0 });
            f5.AddTerm(-1, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            Polynomial f6 = new Polynomial(ring);
            f6.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 3, 0, 0 });
            f6.AddTerm(-1, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            Polynomial f7 = new Polynomial(ring);
            f7.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 0, 3, 0 });
            f7.AddTerm(-1, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            Polynomial f8 = new Polynomial(ring);
            f8.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 0, 0, 3 });
            f8.AddTerm(-1, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            Polynomial f9 = new Polynomial(ring);
            f9.AddTerm(1, new int[] { 2, 0, 0, 0, 0, 0, 0, 0 });
            f9.AddTerm(1, new int[] { 1, 1, 0, 0, 0, 0, 0, 0 });
            f9.AddTerm(1, new int[] { 0, 2, 0, 0, 0, 0, 0, 0 });


            Polynomial f10 = new Polynomial(ring);
            f10.AddTerm(1, new int[] { 0, 2, 0, 0, 0, 0, 0, 0 });
            f10.AddTerm(1, new int[] { 0, 1, 1, 0, 0, 0, 0, 0 });
            f10.AddTerm(1, new int[] { 0, 0, 2, 0, 0, 0, 0, 0 });

            Polynomial f11 = new Polynomial(ring);
            f11.AddTerm(1, new int[] { 0, 0, 2, 0, 0, 0, 0, 0 });
            f11.AddTerm(1, new int[] { 0, 0, 1, 1, 0, 0, 0, 0 });
            f11.AddTerm(1, new int[] { 0, 0, 0, 2, 0, 0, 0, 0 });


            Polynomial f12 = new Polynomial(ring);
            f12.AddTerm(1, new int[] { 0, 0, 0, 2, 0, 0, 0, 0 });
            f12.AddTerm(1, new int[] { 0, 0, 0, 1, 1, 0, 0, 0 });
            f12.AddTerm(1, new int[] { 0, 0, 0, 0, 2, 0, 0, 0 });

            Polynomial f13 = new Polynomial(ring);
            f13.AddTerm(1, new int[] { 0, 0, 0, 0, 2, 0, 0, 0 });
            f13.AddTerm(1, new int[] { 0, 0, 0, 0, 1, 1, 0, 0 });
            f13.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 2, 0, 0 });

            Polynomial f14 = new Polynomial(ring);
            f14.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 2, 0, 0 });
            f14.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 1, 1, 0 });
            f14.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 0, 2, 0 });

            Polynomial f15 = new Polynomial(ring);
            f15.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 0, 2, 0 });
            f15.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 0, 1, 1 });
            f15.AddTerm(1, new int[] { 0, 0, 0, 0, 0, 0, 0, 2 });

            Ideal I = new Ideal(new Polynomial[] { f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15 }, ring);
            Console.WriteLine("__________Gröbner Basis__________________");
            var gb = I.GröbnerBasis();
            foreach (var p in gb)
                Console.WriteLine(p);

            Console.WriteLine("_________Minimal Gröbner Basis___________________");

            var minimal = I.MinimalGröbnerBasis();
            foreach (var p in minimal)
                Console.WriteLine(p);

            Console.WriteLine("______________Reduced Gröbner Basis______________");


            var reduced = I.ReducedGrobnerBasis();
            foreach (var p in reduced)
                Console.WriteLine(p);

            Console.WriteLine("__________Membership__________________");
            Console.WriteLine(I.Member(f1));
        }
        static void Test10()
        {
            Ring r = new Ring(Field.Real, new string[] { "x1", "x2", "x3", "x4" });

            r.FixOrder(new string[] { "x2", "x3", "x4", "x1" });

            // x2*x3 -x2*x4 + x3^2 -x4^2 
            Polynomial f1 = new Polynomial(r);
            f1.AddTerm(1, new int[] { 0, 1, 1, 0 });
            f1.AddTerm(-1, new int[] { 0, 1, 0, 1 });
            f1.AddTerm(1, new int[] { 0, 0, 2, 0 });
            f1.AddTerm(-1, new int[] { 0, 0, 0, 2 });

            //x3^2 + x3*x1 + x1^2
            Polynomial f2 = new Polynomial(r);
            f2.AddTerm(1, new int[] { 0, 0, 2, 0 });
            f2.AddTerm(-1, new int[] { 1, 0, 1, 0 });
            f2.AddTerm(-1, new int[] { 1, 0, 1, 0 });

            Console.WriteLine(f1 + " divided by " + f2);
            Console.WriteLine("________________STEPS__________________");

            var div = r.Divide(f1, new Polynomial[] { f2 });
            Console.WriteLine("________________RESULT_________________");

            Console.WriteLine("Remainder: " + div.Item2);
            Console.WriteLine("Quotients:");

            for (int i = 0; i < div.Item1.Length; i++)
                Console.WriteLine(i.ToString() + ") " + div.Item1[i]);


        }

        static void Test11()
        {
            Ring r = new Ring(Field.Real, new string[] { "x1", "x2", "x3", "x4" });

            r.FixOrder(new string[] { "x2", "x3", "x4", "x1" });

            // x2*x3 -x2*x4 + x3^2 -x4^2 
            Polynomial f1 = new Polynomial(r);
            f1.AddTerm(1, new int[] { 0, 1, 1, 0 });
            f1.AddTerm(-1, new int[] { 0, 1, 0, 1 });
            f1.AddTerm(1, new int[] { 0, 0, 2, 0 });
            f1.AddTerm(-1, new int[] { 0, 0, 0, 2 });

            //x3^2 + x3*x1 + x1^2
            Polynomial f2 = new Polynomial(r);
            f2.AddTerm(1, new int[] { 0, 0, 2, 0 });
            f2.AddTerm(-1, new int[] { 1, 0, 1, 0 });
            f2.AddTerm(-1, new int[] { 1, 0, 1, 0 });

            Console.WriteLine(f1 + " divided by " + f2);
            Console.WriteLine("________________STEPS__________________");

            var div = r.Divide(f1, new Polynomial[] { f2 });
            Console.WriteLine("________________RESULT_________________");

            Console.WriteLine("Remainder: " + div.Item2);
            Console.WriteLine("Quotients:");

            for (int i = 0; i < div.Item1.Length; i++)
                Console.WriteLine(i.ToString() + ") " + div.Item1[i]);


        }



        static void Test12()
        {
            Ring r = new Ring(Field.Real, new string[] { "x1", "x2", "x3", "x4" });

            r.FixOrder(new string[] { "x2", "x3", "x4", "x1" });
            /*
             * Current Dividend:-0,666666666666667x4^2*x1^4 +0,666666666666667x4^2*x1 -0,333333333333333x1^3 +0,333333333333333
             *   Current divisor: x1^3 -1
             *   Quotient:-0,666666666666667x4^2*x1
             *   subtract: 0,666666666666667x4^2*x1^4
             *   subtract: -0,666666666666667x4^2*x1
             */

            // x2*x3 -x2*x4 + x3^2 -x4^2 
            Polynomial f1 = new Polynomial(r);
            f1.AddTerm(-0.666666666666667, new int[] { 4, 0, 0, 2 });
            f1.AddTerm(0.666666666666667, new int[] { 1, 0, 0, 2 });
            f1.AddTerm(-0.333333333333333, new int[] { 3, 0, 0, 0 });
            f1.AddTerm(0.333333333333333, new int[] { 0, 0, 0, 0 });

            //x3^2 + x3*x1 + x1^2
            Polynomial f2 = new Polynomial(r);
            f2.AddTerm(1, new int[] { 3, 0, 0, 0 });
            f2.AddTerm(-1, new int[] { 0, 0, 0, 0 });

            Console.WriteLine(f1 + " divided by " + f2);
            Console.WriteLine("________________STEPS__________________");

            var div = r.Divide(f1, new Polynomial[] { f2 });
            Console.WriteLine("________________RESULT_________________");

            Console.WriteLine("Remainder: " + div.Item2);
            Console.WriteLine("Quotients:");

            for (int i = 0; i < div.Item1.Length; i++)
                Console.WriteLine(i.ToString() + ") " + div.Item1[i]);


        }


        //Division Test for a case in the previous test
        static void Test13()
        {
            Ring r = new Ring(Field.Real, new string[] { "x", "y", "z" });

            r.FixOrder(new string[] { "x", "y", "z" });
            r.Order = MonomialOrder.deglex;
            Polynomial f1 = new Polynomial(r);
            f1.AddTerm(1, new int[] { 1, 0, 0 });
            f1.AddTerm(1, new int[] { 1, 2, 0 });

            Polynomial f2 = new Polynomial(r);
            f2.AddTerm(1, new int[] { 0, 1, 1 });

            Polynomial f3 = new Polynomial(r);
            f3.AddTerm(1, new int[] { 0, 1, 0 });
            f3.AddTerm(1, new int[] { 0, 0, 0 });


            Polynomial f4 = new Polynomial(r);
            f4.AddTerm(1, new int[] { 0, 0, 1 });
            f4.AddTerm(1, new int[] { 1, 0, 0 });



            Polynomial dividend = new Polynomial(r);
            dividend.AddTerm(1, new int[] { 3, 3, 1 });
            dividend.AddTerm(1, new int[] { 3, 2, 1 });
            dividend.AddTerm(1, new int[] { 2, 3, 1 });
            dividend.AddTerm(1, new int[] { 2, 2, 1 });

   
            var quotients = new Polynomial[] { f1, f2, f3, f4 };

            foreach (Polynomial p in quotients)
                Console.WriteLine(p);
            Console.WriteLine("_----------dividend-----");
            Console.WriteLine(dividend);
            //                                                            f2   f3   f1   f4
            //            var res = r.Divide(dividend, new Polynomial[] { f1, f2,   f3,  f4 });

            var res = r.Divide(dividend, quotients);
            Console.WriteLine("------------------RESULTADO---------------");

            Console.WriteLine("resto:" + res.Item2);
            Console.WriteLine("---------------------------------");
            foreach (Polynomial p in res.Item1)
                Console.WriteLine(p);

        }

        static void Test14()
        {
            Ring r = new Ring(Field.Real, new string[] { "x", "y", "z" });

            r.FixOrder(new string[] { "z", "y", "x" });
            r.Order = MonomialOrder.deglex;

            Polynomial f1 = new Polynomial(r);
            f1.AddTerm(1, new int[] { 1, 1, 0 });
            f1.AddTerm(-1, new int[] { 0, 1, 0 });

            Polynomial f2 = new Polynomial(r);
            f2.AddTerm(1, new int[] { 0, 2, 0 });
            f2.AddTerm(-1, new int[] { 1, 0, 0 });




            Polynomial dividend = new Polynomial(r);
            dividend.AddTerm(-1, new int[] { 1, 0, 0 });
            dividend.AddTerm(1, new int[] { 1, 2, 0 });


            var res = r.Divide(dividend, new Polynomial[] { f1, f2 });
            Console.WriteLine("------------------RESULTADO---------------");

            Console.WriteLine("resto:" + res.Item2);
            Console.WriteLine("---------------------------------");


        }
        static void Test15()
        {
            Ring r = new Ring(Field.Real, new string[] { "x", "y", "z" });

            r.FixOrder(new string[] { "x", "y", "z" });

            Polynomial dividend = new Polynomial(r);
            dividend.AddTerm(-1, new int[] { 0, 2, 2 });
            dividend.AddTerm(-1, new int[] { 0, 2, 0 });

            Polynomial f1 = new Polynomial(r);
            f1.AddTerm(1, new int[] { 1, 0, 1 });
            f1.AddTerm(-1, new int[] { 0, 1, 0 });

            Polynomial f2 = new Polynomial(r);
            f2.AddTerm(1, new int[] { 0, 1, 1 });
            f2.AddTerm(1, new int[] { 1, 0, 0 });


            Console.WriteLine("------------------RESULTADO---------------");
            Console.WriteLine(r.Divide(dividend, new Polynomial[] { f1,f2 }).Item2);

            Console.WriteLine("---------------------------------");
          

        }

        static void Test16()
        {
            Ring r = new Ring(Field.Real, new string[] { "x", "y", "z" });
            r.Order = MonomialOrder.deglex;

            r.FixOrder(new string[] { "z","y","x" });
           
     
            Polynomial f1 = new Polynomial(r);
            f1.AddTerm(1, new int[] { 1, 0, 1 });
            f1.AddTerm(1, new int[] { 0, 1, 0 });

            Polynomial f2 = new Polynomial(r);
            f2.AddTerm(-1, new int[] { 2, 1, 1 });
            f2.AddTerm(1, new int[] { 0, 2, 0 });


            Polynomial f3 = new Polynomial(r);
            f3.AddTerm(1, new int[] { 1, 2, 0 });
            f3.AddTerm(-1, new int[] { 0, 1, 0 });
            
            Ideal I = new Ideal(new Polynomial[] { f1, f2}, r);    

            var gb = I.ReducedGrobnerBasis();

            Console.WriteLine("__________Gröbner Basis__________________");
            foreach (var p in gb)
                Console.WriteLine(p);

            Console.WriteLine("---------------------------------");
            var lex = new DegreeLexicographical();
            Console.WriteLine(gb[1].LeadingTerm);
            Console.WriteLine(lex.Compare(gb[1].LeadingTerm, gb[1].Terms[1]));

        }


        static void Test17()
        {
            Ring r = new Ring(Field.Real, new string[] { "x", "y", "z" })
            {
                Order = MonomialOrder.deglex
            };

            r.FixOrder(new string[] { "z", "y", "x" });


            Polynomial f1 = new Polynomial(r);
            f1.AddTerm(1, new int[] { 0, 1, 0 });
            f1.AddTerm(1, new int[] { 1, 0, 0 });

            Polynomial f2 = new Polynomial(r);
            f2.AddTerm(-1, new int[] { 1, 1, 1 });
            f2.AddTerm(1, new int[] { 0, 1, 2 });


            Polynomial f3 = new Polynomial(r);
            f3.AddTerm(1, new int[] { 1, 2, 0 });
            f3.AddTerm(-1, new int[] { 0, 1, 0 });

            Ideal I = new Ideal(new Polynomial[] { f1, f2, f3}, r);

            Console.WriteLine("__________Conjunto  generador__________________");
            foreach (var p in I.GeneratorSet)
                Console.WriteLine(p);
            var gb = I.ReducedGrobnerBasis();

            Console.WriteLine("__________Gröbner Basis__________________");
            foreach (var p in gb)
                Console.WriteLine(p);



        }
    }

}

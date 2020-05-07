using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GröbnerBasis.PolynomialRings.Fields;
using GröbnerBasis.PolynomialRings.Order;

namespace GröbnerBasis.PolynomialRings
{
    public class Term : IEquatable<Term>, IComparable<Term>
    {
        public int[] PowerProduct { get; private set; }
        private Polynomial _owner = null;
        private Ring ring;

        public dynamic Coefficient { get; set; }

        public Polynomial owner
        {
            get => _owner;
            set => _owner ??= value;
        }


        public Term(double real,int[] power, Ring ring) : this(power, ring) => Coefficient = real;
        public Term(long integer,int[] power, Ring ring) : this(power, ring) => Coefficient = integer;
        public Term(Complex complex,int[] power, Ring ring) : this(power, ring) => Coefficient = complex;
        public Term(Rational rational,int[] power, Ring ring) : this(power, ring) => Coefficient = rational;
        private Term(int[] power, Ring ring) => (PowerProduct, this.ring) = (power, ring);



        public bool Equals(Term other)
        {
            return PowerProduct.SequenceEqual(other.PowerProduct);
        }

        public override string ToString()
        {
            
            string temp = Coefficient.ToString();
            string power = "";
            for (int i = 0; i < PowerProduct.Length; i++)
            {
                if (PowerProduct[i] > 0)
                    power += ring.VariableIndexToString[i] + "^{" + PowerProduct[i] + "}";
            }
            return temp + power;
        }

        public int CompareTo(Term other)
        {
            IComparer<Term> comparer;
            switch (owner.Order)
            {
                case MonomialOrder.deglex:
                    comparer = new GradeLexicographical();
                    break;
                default:
                    comparer = new Lexicographical();
                    break;
            }
            return comparer.Compare(this, other);

        }

        public static Term operator +(Term a,Term b)
        {
            if (!a.Equals(b) || a.ring!= b.ring)
                throw new ArithmeticException();
            return new Term(a.Coefficient + b.Coefficient, a.PowerProduct, a.ring);

        }

        public static Term operator -(Term a, Term b)
        {
            if (!a.Equals(b) || a.ring != b.ring)
                throw new ArithmeticException();
            return new Term(a.Coefficient - b.Coefficient, a.PowerProduct, a.ring);

        }
        public static Term operator *(Term p1, Term p2)
        {
            if (p1.PowerProduct.Length != p2.PowerProduct.Length)
                throw new ArithmeticException();
            int[] pp = new int[p1.PowerProduct.Length];

            for (int i = 0; i < pp.Length; i++)
            {
                pp[i] = p1.PowerProduct[i] + p2.PowerProduct[i];
            }

            return new Term(p1.Coefficient * p2.Coefficient, pp, p1.ring);
        }

        public static Term operator *(int integer, Term p1) => p1 * integer;
        public static Term operator *(Term p1, int integer)
        {
            var term = p1.Clone(p1.owner);
            term.Coefficient *= integer;
            return term;
        }

        public Term Clone(Polynomial owner)
        {
            return new Term(Coefficient,(int[])PowerProduct.Clone(), ring);
        }

       
    }
}

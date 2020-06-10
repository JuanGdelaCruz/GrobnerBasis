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
            set
            {
                if (_owner == null)
                    _owner = value;
            }
        }


        public Term(double real, int[] power, Ring ring) : this(power, ring) => Coefficient = real;
        public Term(long integer, int[] power, Ring ring) : this(power, ring) => Coefficient = integer;
        public Term(Complex complex, int[] power, Ring ring) : this(power, ring) => Coefficient = complex;
        public Term(Rational rational, int[] power, Ring ring) : this(power, ring) => Coefficient = rational;
        private Term(int[] power, Ring ring) => (PowerProduct, this.ring) = (power, ring);



        public bool Equals(Term other)
        {
            return PowerProduct.SequenceEqual(other.PowerProduct);
        }

        public override string ToString()
        {
            StringBuilder power = new StringBuilder(PowerProduct.Length * 4);
            for (int i = 0; i < PowerProduct.Length; i++)
            {
                if (PowerProduct[i] > 0)
                {
                    power.Append(ring.VariableIndexToString[i]);
                    if (PowerProduct[i] > 1)
                        power.Append("^" + PowerProduct[i]);
                    power.Append('*');
                }
            }

            if (power.Length > 0 && power[power.Length - 1] == '*')
                power.Remove(power.Length - 1, 1);

            string coeff = "";
            var c = (Complex)Coefficient;
            if (power.Length > 0 && c.Imaginary == 0 && Math.Abs(c.Real) == 1)
            {
                coeff += (c.Real < 0 ? "- " : " ");
            }
            else
                coeff = Coefficient.ToString();

            return coeff + power;
        }



        public int CompareTo(Term other)
        {
            IComparer<Term> comparer;
            switch (owner.Order)
            {
                case MonomialOrder.deglex:
                    comparer = new DegreeLexicographical();
                    break;
                default:
                    comparer = new Lexicographical();
                    break;
            }
            return comparer.Compare(this, other);

        }

        public static Term operator +(Term a, Term b)
        {
            if (!a.Equals(b) || a.ring != b.ring)
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

        public static Term operator /(Term dividend, Term divisor)
        {
            if (divisor.ring != dividend.ring)
                throw new ArithmeticException();
            int[] pp = new int[dividend.PowerProduct.Length];

            for (int i = 0; i < pp.Length; i++)
            {
                pp[i] = dividend.PowerProduct[i] - divisor.PowerProduct[i];
            }

            return new Term(dividend.Coefficient / divisor.Coefficient, pp, dividend.ring);
        }



        public Term Clone(Polynomial owner)
        {
            return new Term(Coefficient, (int[])PowerProduct.Clone(), ring);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GröbnerBasis.PolynomialRings.Fields
{

    public struct Rational
    {

        private readonly int numerator;
        private readonly int denominator;

        public Rational(int numerator, int denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
           }

        public static Rational operator +(Rational a) => a;
    }
}

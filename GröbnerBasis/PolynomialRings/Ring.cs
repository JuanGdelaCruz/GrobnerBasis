using GröbnerBasis.PolynomialRings.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GröbnerBasis.PolynomialRings
{
    public class Ring
    {

        public Dictionary<int, string> VariableIndexToString { get; private set; } = new Dictionary<int, string>();
        public Dictionary<string, int> VariableStringToIndex { get; private set; } = new Dictionary<string, int>();

        public Dictionary<int, int> VariableOrder { get; private set; } = new Dictionary<int, int>();

        public int Dimension { get; private set; }

        public Ring(string[] vars)
        {
            Dimension = vars.Length;
            for (int i = 0; i < vars.Length; i++)
            {
                VariableOrder.Add(i, i);
                VariableIndexToString.Add(i, vars[i]);
                VariableStringToIndex.Add(vars[i], i);
            }
        }

        public Tuple<Polynomial[], Polynomial> Divide(Polynomial dividend, Polynomial[] divisors)
        {
            //Initialization
            Polynomial remainder = new Polynomial(this);
            Polynomial[] quotients = new Polynomial[divisors.Length];
            for (int i = 0; i < quotients.Length; i++)
                quotients[i] = new Polynomial(this);
            //Begin algorithm
            Polynomial h = dividend.Clone();
            while (!h.IsZero())
            {

                var first = divisors.FirstOrDefault(f_i => f_i.Divides(h));
                if (first != null)
                {

                    //CHECK if bottleneck: Using a dictionary for the quotients might improve performance.
                    int index = Array.IndexOf(divisors, first);
                    var partialQuotient = DivideTerms(h.LeadingTerm(), divisors[index].LeadingTerm());
                    quotients[index].AddTerm(partialQuotient);
                    foreach (Term term in divisors[index].Terms)
                    {
                        var subtract =-1* partialQuotient *  term;
                        h.AddTerm(subtract);
                    }

                }
                else
                {
                    var clone = h.LeadingTerm().Clone(remainder);
                    remainder.AddTerm(clone);
                    var subtract = h.LeadingTerm().Clone(h);
                    subtract.Coefficient *= -1;
                    h.AddTerm(subtract);
                }

            }

            return new Tuple<Polynomial[], Polynomial>(quotients, remainder);
        }

        public bool FixOrder(string[] order)
        {
            if (order == null || order.Length != Dimension)
                return false;
            for (int i = 0; i < order.Length; i++)
            {
                VariableOrder[i] = VariableStringToIndex[order[i]];
            }
            return true;
        }

        public Polynomial SPolynomial(Polynomial f, Polynomial g)
        {
            //S(f,g)= L/lt(f) * f - L/lt(g) *g   with L = lcm(lp(f),lp(g)).
            Polynomial s = new Polynomial(this);
            Term lcm = LCM(f.LeadingTerm(), g.LeadingTerm());
            // L / lt(f)
            {

                Term div = DivideTerms(lcm, f.LeadingTerm());
                // L / lt(f) *f
                foreach (Term term in f.Terms)
                {
                    var add = div*term;
                    s.AddTerm(add);
                }
            }
            //-L / lt(g) * g
            {
                // L / lt(g)
                Term div = DivideTerms(lcm, g.LeadingTerm());
                // - L / lt(g) * g
                foreach (Term term in g.Terms)
                {
                    var sub = div * (-1*term);                    
                   
                    s.AddTerm(sub);
                }
            }
            return s;
        }


        public Term LCM(Term first, Term second)
        {
            if (first.PowerProduct.Length != Dimension || second.PowerProduct.Length != Dimension)
                return null;

            int[] powerProduct = new int[Dimension];
            for (int i = 0; i < Dimension; i++)
            {
                powerProduct[i] = Math.Max(first.PowerProduct[i], second.PowerProduct[i]);
            }
            return new Term(1, powerProduct, this);
        }


        private Term DivideTerms(Term dividend, Term divisor)
        {
            if (divisor.PowerProduct.Length != dividend.PowerProduct.Length)
                throw new ArithmeticException();
            int[] pp = new int[dividend.PowerProduct.Length];

            for (int i = 0; i < pp.Length; i++)
            {
                pp[i] = dividend.PowerProduct[i] - divisor.PowerProduct[i];
            }

            return new Term(dividend.Coefficient / divisor.Coefficient, pp, this);
        }

    

    }
}

using GröbnerBasis.PolynomialRings.Fields;
using GröbnerBasis.PolynomialRings.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace GröbnerBasis.PolynomialRings
{
    public class Polynomial: IEquatable<Polynomial>
    { 
        private readonly List<Term> terms = new List<Term>();
        public Ring Ring { get; private set; }

        public ReadOnlyCollection<Term> Terms
        {
            get { return terms.AsReadOnly(); }
        }

        private MonomialOrder _order = MonomialOrder.lex;
      

        public MonomialOrder Order
        {
            get => _order;
            set
            {
                _order = value;
                terms.Sort(GetComparer());
            }
        }

        public Polynomial(Ring ring, Term[] terms = null)
        {
            Ring = ring;
            if (terms != null)
                AddTerms(terms);
        }


        public void AddTerm(Term term)
        {
            if (term == null)
                return;
            Term contained = terms.SingleOrDefault(x => x.PowerProduct.SequenceEqual(term.PowerProduct));
            if (contained != null)
            {
                contained  += term;
                if (contained.Coefficient == 0)
                {
                    terms.Remove(contained);
              
                }
            }
            else
            {
                //Might be interesting to insert the term directly at the appropriate position .
                terms.Add(term);
                term.owner = this;
                terms.Sort(GetComparer());
            }
        }


        public void AddTerm(double coef, int[] powerProduct)
        {
            Term term = new Term(coef, powerProduct, Ring);
            AddTerm(term);
        }

        public void AddTerms(Term[] terms) => terms.ToList().ForEach(term => AddTerm(term));

        public Term LeadingTerm() => terms[0];

        public int[] LeadingPowerProduct() => LeadingTerm().PowerProduct;



        public bool IsZero()
        {
            return terms.Count == 0;
        }

        public bool Divides(Polynomial other)
        {
            if (terms.Count == 0)
                return false;

            bool divides = LeadingTerm().CompareTo(other.LeadingTerm()) >= 0;

            var pp = other.LeadingTerm().PowerProduct;
            for (int i = 0; i < pp.Length; i++)
                divides = divides && LeadingPowerProduct()[i] <= pp[i];

            return divides;
        }


        public override string ToString()
        {
            string pstring = "";
            foreach (Term term in terms)
                pstring += (term.Coefficient >= 0 ? "+" : "") + term.ToString();
            return pstring;
        }

        public Polynomial Clone()
        {
            Polynomial copy = new Polynomial(Ring)
            {
                Order = Order
            };
            foreach (Term term in terms) 
                      copy.AddTerm(term.Clone(copy));
            return copy;
        }

        private IComparer<Term> GetComparer()
        {
            switch (Order)
            {
                default:
                    return new Lexicographical();
                case MonomialOrder.deglex:
                    return new GradeLexicographical();
     
            }
        }

        public bool Equals(Polynomial other)
        {
            return terms.SequenceEqual(other.Terms);
        }
    }
}

using GröbnerBasis.PolynomialRings.Fields;
using GröbnerBasis.PolynomialRings.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

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
            var epsilon = 0.00001d;
            if (Ring.field == Field.Real)
            {
                var round = Math.Round(term.Coefficient);
                if (Math.Abs(round - term.Coefficient) <= epsilon)
                    term.Coefficient = round;
            }
               
            if (Math.Abs(term.Coefficient) <= epsilon)
                return;
            Term contained = terms.SingleOrDefault(x => x.PowerProduct.SequenceEqual(term.PowerProduct));
            if (contained != null)
            {
                int index = terms.IndexOf(contained);
                terms[index]=  contained + term;
                terms[index].owner = this;

                if (Math.Abs( terms[index].Coefficient) <= epsilon)
                {
                    terms.Remove(terms[index]);
              
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
            dynamic value = coef;
            if (Ring.field != Field.Real)
                value = CastCoefficient(coef);
            Term term = new Term(value, powerProduct, Ring); AddTerm(term);
        }

        public void AddTerm(Complex coef, int[] powerProduct)
        {
            dynamic value = coef;
            if (Ring.field != Field.Complex)
              value=  CastCoefficient(coef);
            Term term = new Term(value, powerProduct, Ring);
            AddTerm(term);
        }

        public void AddTerm(int coef, int[] powerProduct)
        {
            dynamic value = coef;
            if (Ring.field != Field.Integer)
                value = CastCoefficient(coef);
            Term term = new Term(value, powerProduct, Ring);
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


        public void ReduceCoefficients()
        {
            var coef = LeadingTerm().Coefficient;
            foreach (Term term in terms)
            {
                term.Coefficient /= coef;
            }
        }
        
        public override string ToString()
        {
            string pstring = "";
            for(int i =0;i < Terms.Count;i++)
            {
                if (i > 0)
                {
                    var c = (Complex)Terms[i].Coefficient;
                    pstring += (c.Real >= 0 ? " +" : " ");
                }
                pstring += Terms[i].ToString();
            }
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

        private  dynamic CastCoefficient(dynamic coefficent)
        {
            switch (Ring.field)
            {
            
                case Field.Integer:
                    return (long)coefficent;
                case Field.Complex:
                    return (Complex)coefficent;
                case Field.Rational:
                    return (Rational)coefficent;
                case Field.Real:
                default:
                    return (double)coefficent;
            }
        }


    }
}

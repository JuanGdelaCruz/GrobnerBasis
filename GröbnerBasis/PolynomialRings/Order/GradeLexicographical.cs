using GröbnerBasis.PolynomialRings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GröbnerBasis.PolynomialRings.Order
{
    public class GradeLexicographical : IComparer<Term>
    {
        public int Compare(Term first, Term second)
        {

            if (first.PowerProduct.Length != second.PowerProduct.Length)
                throw new Exception("One of the power products does not belong to the given Ring!");

            int fTotalGrade = first.PowerProduct.Sum();
            int sTotalGrade = second.PowerProduct.Sum();

            if (fTotalGrade < sTotalGrade)
                return 1;
            if (fTotalGrade > sTotalGrade)
                return -1;
            
            var lex = new Lexicographical();
            return lex.Compare(first, second);
             
        }
    }
}
 
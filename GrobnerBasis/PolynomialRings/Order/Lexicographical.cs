using GröbnerBasis.PolynomialRings;
using System;
using System.Collections.Generic;

namespace GröbnerBasis.PolynomialRings.Order
{
    public class Lexicographical : IComparer<Term>
    {
        public int Compare(Term first, Term second)
        {

            if (first.PowerProduct.Length != second.PowerProduct.Length)
                throw new Exception("One of the power products does not belong to the given Ring!");

            var ring = first.owner.Ring;

            for (int i = 0; i < first.PowerProduct.Length; i++)
            {
                if (first.PowerProduct[ring.VariableOrder[i]] > second.PowerProduct[ring.VariableOrder[i]])
                    return -1;
                else if (first.PowerProduct[ring.VariableOrder[i]] < second.PowerProduct[ring.VariableOrder[i]])
                    return 1;
            }
            return 0;
        }
    }
}

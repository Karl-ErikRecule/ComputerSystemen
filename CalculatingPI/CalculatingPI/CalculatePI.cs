using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatingPI
{
    // super klasse, in deze klasse zit de basisfunctionaliteit zoals het aantal iteraties
    // het verschil tussen PI als berekende waarde en de echte PI waarde
    class CalculatePI
    {
        protected uint m_NrOfIterations { get; set; }

        public CalculatePI(uint set_NrOfIterations)
        {
            m_NrOfIterations = set_NrOfIterations;
        }

        protected decimal ErrorDifferencePi(decimal predictedPi)
        {
            decimal pi = (decimal)Math.PI;
            return Math.Abs(pi - predictedPi);
        }
    }
}

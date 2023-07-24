using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDeltaCalculator {
    public class Option {
        public double Strike { get; private set; }
        public double LastPrice { get; private set; }
        public int OpenInterest { get; private set; }

        public Option(double Strike, double LastPrice, int OpenInterest) {
            this.Strike = Strike;
            this.LastPrice = LastPrice;
            this.OpenInterest = OpenInterest;
        }
    }
}

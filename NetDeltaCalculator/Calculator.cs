using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDeltaCalculator {
    public class Calculator {
        public static double Get(string company) {
            List<int> expiryDates = ExpiryDates.Get(company);
            double netDelta = 0;

            foreach(int expiryDate in expiryDates) {
                List<Option> callOptions = Options.GetCalls(company, expiryDate);
                List<Option> putOptions = Options.GetPuts(company, expiryDate);
                netDelta += NetDelta(callOptions) + NetDelta(putOptions);
            }

            return netDelta;
        }

        private static double NetDelta(List<Option> options) {
            double netDelta = 0;
            for (int i = 0; i < options.Count; i++) {
                int index1 = (i == 0) ? i : i - 1;
                int index2 = (i == options.Count - 1) ? i : i + 1;

                Option option1 = options[index1];
                Option option2 = options[index2];
                double delta = (option1.LastPrice - option2.LastPrice) / (option2.Strike - option1.Strike);
                netDelta += delta * options[i].OpenInterest;
            }
            return netDelta;
        }
    }
}

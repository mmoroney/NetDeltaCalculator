using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDeltaCalculator {
    public static class ExpiryDates {
        public static List<int> Get(string company) {
            HtmlDocument htmlDoc = Download.Get(string.Format("https://finance.yahoo.com/quote/{0}/options?p={1}", company, company));

            HtmlNode dateNode = htmlDoc.DocumentNode.Descendants("select")
               .Where(node => node.GetAttributeValue("class", "").Equals("Fz(s) H(25px) Bd Bdc($seperatorColor)")).First();

            List<int> dates = new();

            foreach (HtmlNode node in dateNode.Descendants("option")) {
                int date = int.Parse(node.GetAttributeValue("value", ""));
                dates.Add(date);
            }

            return dates;
        }
    }
}

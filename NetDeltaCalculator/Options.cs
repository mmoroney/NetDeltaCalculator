using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDeltaCalculator {
    internal static class Options {
        public static List<Option> GetCalls(string company, int expiryDate) {
            return GetOptions(company, expiryDate, "calls W(100%) Pos(r) Bd(0) Pt(0) list-options");
        }

        public static List<Option> GetPuts(string company, int expiryDate) {
            return GetOptions(company, expiryDate, "puts W(100%) Pos(r) list-options");
        }

        private static List<Option> GetOptions(string company, int expiryDate, string className) {
            string url = string.Format("https://finance.yahoo.com/quote/{0}/options?p={1}&date={2}", company, company, expiryDate);
            HtmlDocument htmlDoc = Download.Get(url);

            IEnumerable<HtmlNode> tables = htmlDoc.DocumentNode.Descendants("table")
                .Where(node => node.GetAttributeValue("class", "").Equals(className));

            HtmlNode table = tables.First();

            List<Option> options = new();

            foreach (HtmlNode option in table.Descendants("tr")
                .Where(node => node.GetAttributeValue("class", "").StartsWith("data-row"))) {

                options.Add(ParseOption(option));
            }

            return options;
        }

        private static Option ParseOption(HtmlNode option) {
            double strike = ParseStrike(option);
            double lastPrice = ParseLastPrice(option);
            int openInterest = ParseOpenInterest(option);
            return new Option(strike, lastPrice, openInterest);
        }

        private static double ParseStrike(HtmlNode option) {
            HtmlNode strikeNode = option.Descendants("td").Where(node => node.GetAttributeValue("class", "").StartsWith("data-col2")).First();
            HtmlNode href = strikeNode.Descendants("a").First();
            return Double.Parse(href.InnerText);
        }

        private static double ParseLastPrice(HtmlNode option) {
            HtmlNode lastPriceNode = option.Descendants("td").Where(node => node.GetAttributeValue("class", "").StartsWith("data-col3")).First();
            return Double.Parse(lastPriceNode.InnerText);
        }

        private static int ParseOpenInterest(HtmlNode option) {
            HtmlNode openInterestNode = option.Descendants("td").Where(node => node.GetAttributeValue("class", "").StartsWith("data-col9")).First();
            return int.Parse(openInterestNode.InnerText.Replace(",", ""));
        }

    }
}

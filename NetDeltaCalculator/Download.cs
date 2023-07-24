using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDeltaCalculator {
    public static class Download {
        public static HtmlDocument Get(string url) {
            Console.WriteLine("Downloading from " + url);
            HttpClient httpClient = new();
            string html = httpClient.GetStringAsync(url).GetAwaiter().GetResult();
            Console.WriteLine("Downloading complete!");

            HtmlDocument htmlDoc = new();
            htmlDoc.LoadHtml(html);
            return htmlDoc;
        }
    }
}

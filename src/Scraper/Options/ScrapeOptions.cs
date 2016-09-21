using CommandLine;
using System;

namespace CbrFT.Scraper
{
    [Verb("scrape", HelpText = "Scrape currencies values.")]
    public class ScrapeOptions
    {
        [Option('d', "delay", Required = false, Default = 1000,
            HelpText = "Download delay in ms.")]
        public int Delay { get; set; }

        [Option('r', "delay-after-n-requests", Required = false, Default = 30,
            HelpText = "Should delay after N requests")]
        public int MaxRequestCount { get; set; }

        [Option('f', "from", Required = false,
            HelpText = "From which date should begin: DD-MM-YYYY")]
        public string _FromDateString { get; set; }

        [Option('t', "to", Required = false,
            HelpText = "Which date should finish: DD-MM-YYYY")]
        public string _ToDateString { get; set; }

        const string DATETIME_FORMAT = "dd-MM-yyyy";

        public DateTime FromDate
        {
            get
            {
                DateTime dtFrom = DateTime.UtcNow;

                try
                {
                    dtFrom = DateTime.ParseExact(_FromDateString, DATETIME_FORMAT, null);
                }
                catch (Exception ex) when (ex is ArgumentNullException || ex is FormatException) { }

                return dtFrom;
            }
        }

        public DateTime ToDate
        {
            get
            {
                DateTime dtTo = DateTime.UtcNow;

                try
                {
                    dtTo = DateTime.ParseExact(_ToDateString, DATETIME_FORMAT, null);
                }
                catch (Exception ex) when (ex is ArgumentNullException || ex is FormatException) { }

                return dtTo;
            }
        }
    }
}

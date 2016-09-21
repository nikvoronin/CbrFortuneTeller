using CommandLine;
using System;
using System.Data.SqlTypes;

namespace CbrFT.Scraper
{
    [Verb("clear", HelpText = "Clean data base table.")]
    public class ClearOptions
    {
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
                DateTime dtFrom = (DateTime)SqlDateTime.MinValue;

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

using CommandLine;
using System;

namespace CbrFT.Medium
{
    [Verb("next", HelpText = "Options for regression analisys.")]
    public class NextOptions
    {
        [Option('c', "code", Required = true, Default = "USD",
            HelpText = "3-symbols code of the currency")]
        public string VchCode { get; set; }

        [Option('o', "output-filename", Required = false, Default = "",
            HelpText = "Where to store prediciton")]
        public string Filename { get; set; }

        [Option('n', "next", Required = true, Default = 1,
            HelpText = "After how many days to make a prediction")]
        public int DaysNext { get; set; }

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
                DateTime dtFrom = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);

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

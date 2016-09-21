using CbrFT.DbModel;
using CbrFT.Scraper.CbrService;
using CommandLine;
using log4net;
using System;
using System.Data;
using System.Threading;

namespace CbrFT.Scraper
{
    class Program
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static int Main(string[] args)
        {
            log.Info("Scraper started -------------------------------");
            log.Info($"With args: {string.Join(" ", args)}");

            return Parser.Default.ParseArguments<ClearOptions, ScrapeOptions>(args)
                .MapResult(
                    (ScrapeOptions options) => DoScrape(options),
                    (ClearOptions options) => DoClear(options),
                    errors => { log.Error("App quit with error code 1"); return 1; });
        }

        /// <summary>
        /// Cleans CbrFT table
        /// </summary>
        /// <param name="options">Command line properties</param>
        /// <returns>Exit code</returns>
        private static int DoClear(ClearOptions options)
        {
            log.Info("Do clear table of the currencies values...");
            int exitCode = 0;

            DbManager man = new DbManager();

            try
            {
                man.ClearTable(options.FromDate, options.ToDate);
                log.Info("Done. Now table should be empty.");
            }
            catch (Exception ex)
            {
                log.Error($"Can not clear table. {Environment.NewLine}{ex.Message}");
                exitCode = 2;
            }

            return exitCode;
        }

        /// <summary>
        /// Scrapes currencies through web service
        /// </summary>
        /// <param name="options">Command line properties</param>
        /// <returns>Exit code</returns>
        private static int DoScrape(ScrapeOptions options)
        {
            log.Info("Do scraping of the CBR...");
            int restCounter = 0;

            log.Debug("Creating DailyInfo WebService context...");
            DailyInfo di = new DailyInfo();

            foreach (DateTime day in Extensions.EachDay(options.FromDate, options.ToDate))
            {
                log.Info($"Scraping on {day.ToShortDateString()}");
                DataSet dset = di.GetCursOnDate(day);
                if (dset == null)
                    continue;
                if (dset.Tables.Count < 1)
                    continue;

                DbManager man = new DbManager();
                man.Store(dset, day);

                if (restCounter++ > options.MaxRequestCount)
                {
                    restCounter = 0;
                    log.Info($"Just relaxing web service for {options.Delay} ms...");
                    Thread.Sleep(options.Delay);
                }
            } // foreach EachDay

            log.Info("Scrape done.");

            return 0;
        }
    } // class
}

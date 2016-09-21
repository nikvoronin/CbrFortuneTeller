using CommandLine;
using log4net;
using RDotNet;
using System;
using System.Collections.Generic;
using CbrFT.DbModel;
using CbrFT.DbModel.Entity;
using System.IO;

namespace CbrFT.Medium
{
    class Program
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static int Main(string[] args)
        {
            log.Info("Medium started --------------------------------");
            log.Info($"Args: {string.Join(" ", args)}");

            return Parser.Default.ParseArguments<ListOptions, NextOptions>(args)
                .MapResult(
                    (ListOptions options) => DoList(options),
                    (NextOptions options) => DoPrediction(options),
                    errors => { log.Error("App quit with error code 1"); return 1; });
        }

        /// <summary>
        /// Prints list of currencies
        /// </summary>
        /// <param name="options">Command line options</param>
        /// <returns>Exit code</returns>
        private static int DoList(ListOptions options)
        {
            log.Info("Do listing of the currencies...");
            DbManager man = new DbManager();

            List<Currency> cs =  man.GetCurrencies();
            foreach(Currency c in cs)
                log.Info($"{c.Vcode}\t{c.VchCode}\t{c.Vname.Trim()}");

            return 0;
        }

        /// <summary>
        /// Predicts currency grow rate
        /// </summary>
        /// <param name="options">Command line options</param>
        /// <returns>Exit code</returns>
        private static int DoPrediction(NextOptions options)
        {
            log.Info($"Do prediction for {options.VchCode}... do not disturb...");
            DbManager man = new DbManager();

            log.Debug("Getting values of currency");
            List<CurrencyValue> vs = man.GetValues(options.VchCode, options.FromDate, options.ToDate);

            log.Debug("Converting values into vectors");
            List<double> values = new List<double>();
            List<double> dates = new List<double>();
            DateTime firstDay = vs[0].OnDate;
            foreach (CurrencyValue cv in vs)
            {
                values.Add((double)cv.Vcurs);

                double dateDistance = (cv.OnDate - firstDay).TotalDays;
                dates.Add(dateDistance);
            }

            // Console output preparation
            TextWriter stdWriter = Console.Out;

            MemoryStream memStream = new MemoryStream();
            TextWriter writer = new StreamWriter(memStream);
            Console.SetOut(writer);

            // R-Engine
            REngine r = REngine.GetInstance();

            r.SetSymbol(
                "values",
                new NumericVector(r, values));

            r.SetSymbol(
                "dates",
                new NumericVector(r, dates));

            r.Evaluate("fit <- lm(values ~ dates)");

            double[] d = r.Evaluate("fit$coefficients").AsNumeric().ToArray();
            r.Evaluate("summary(fit)");

            // Reading output of the new console 
            writer.Flush();
            memStream.Seek(0, SeekOrigin.Begin);
            TextReader reader = new StreamReader(memStream);
            string r_output = Environment.NewLine + reader.ReadToEnd();

            writer.Close();
            reader.Close();
            memStream.Close();

            // Return to std console
            Console.SetOut(stdWriter);
            log.Info(r_output);

            // Begin prediction
            DateTime lastDay = vs[vs.Count - 1].OnDate;
            DateTime nextDate = lastDay.AddDays(options.DaysNext);
            double nextDistance = (nextDate - firstDay).TotalDays;

            double lastValue = (double)vs[vs.Count - 1].Vcurs;
            double predictedValue = d[0] + nextDistance * d[1];
            string growPrediciton =
                lastValue == predictedValue ?
                    "not change" :
                    lastValue < predictedValue ?
                        "grow" :
                        "fall";

            string prediction = $"Simon sez that {options.VchCode} will {growPrediciton} and become about {predictedValue}.";

            log.Info(prediction);

            if(!string.IsNullOrEmpty(options.Filename))
            {
                File.WriteAllText(
                    options.Filename,
                     prediction + Environment.NewLine + r_output);
            }

            log.Debug("Disposing instance of the R.NET engine");
            r.ClearGlobalEnvironment();
            r.Dispose();

            log.Info("That's all folks");
            return 0;
        } // DoPrediction
    } // class
}

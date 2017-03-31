using CbrFT.DbModel.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CbrFT.DbModel
{
    public class DbManager
    {
        const string DATETIME_FORMAT = "yyyyMMdd";

        /// <summary>
        /// Reads from DB distinct list of currencies
        /// </summary>
        /// <returns>List of currencies</returns>
        public List<Currency> GetCurrencies()
        {
            List<Currency> currencies = null;

            using (DbModelContext context = new DbModelContext())
            {
                currencies = context.Currencies.ToList();
            }

            return currencies;
        }

        /// <summary>
        /// Currency values for the given period
        /// </summary>
        /// <param name="Vcode">3-symbols of international currency code</param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="result">Array of currency values</param>
        /// <returns>Total count of results</returns>
        public List<CurrencyValue> GetValues(string VchCode, DateTime fromDate, DateTime toDate)
        {
            List<CurrencyValue> values = null;

            using (DbModelContext context = new DbModelContext())
            {
                values = context.CurrencyValues
                    .Where(v => (v.Currency.VchCode == VchCode.Trim()) &&
                                (v.OnDate >= fromDate) &&
                                (v.OnDate <= toDate))
                    .OrderBy(v => v.OnDate)
                    .ToList();
            }

            return values;
        }

        /// <summary>
        /// Clear table content
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        public void ClearTable(DateTime fromDate, DateTime toDate)
        {
            using (DbModelContext context = new DbModelContext())
            {
                context.Database.ExecuteSqlCommand(Queries.CleanTableOnDate, fromDate, toDate);
            }
        }

        /// <summary>
        /// Store new value of the currency... and currency if that not exists
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="onDate"></param>
        public void Store(DataSet dataSet, DateTime onDate)
        {
            using (DbModelContext context = new DbModelContext())
            {
                DataRowCollection rows = dataSet?.Tables[0]?.Rows;
                if (rows == null)
                    throw new NullReferenceException("DataSet MUST contains at least one talbe with one row");

                DateTime actualOnDate = onDate;
                try
                {
                    actualOnDate =
                        DateTime.ParseExact(
                            dataSet.ExtendedProperties["OnDate"] as string,
                            DATETIME_FORMAT, null);
                }
                catch (Exception ex) when (ex is ArgumentNullException || ex is FormatException) { }

                foreach (DataRow row in rows)
                {
                    Currency currency = CurrencyBuilder.Create(row);

                    // check if we suddenly got a new currency
                    Currency dbc = context.Currencies
                        .Where(c => c.Vcode == currency.Vcode)
                        .SingleOrDefault();

                    if (dbc == null)
                        context.Currencies.Add(currency);
                    else
                        currency = dbc;

                    // add currency value if we dont have one on the given date
                    var vs = context.CurrencyValues
                        .Where(v => (v.Currency.Vcode == currency.Vcode) &&
                                    (v.OnDate == onDate))
                        .SingleOrDefault();

                    if (vs == null)
                    {
                        CurrencyValue val = new CurrencyValue();
                        val.Currency = currency;
                        val.OnDate = onDate;
                        val.Vcurs = (decimal)(row["Vcurs"]?? 0);

                        context.CurrencyValues.Add(val);
                    }
                } // foreach row in rows

                context.SaveChanges();
            } // using db context
        } // Store
    } // class
}

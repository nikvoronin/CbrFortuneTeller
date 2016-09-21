using System.Data;

namespace CbrFT.DbModel.Entity
{
    public class CurrencyBuilder
    {
        public static Currency Create(DataRow row)
        {
            Currency currency = new Currency();            
            currency.Vname = row["Vname"] as string;            
            currency.Vcode = (int)(row["Vcode"] ?? -1);
            currency.VchCode = row["VchCode"] as string;
            currency.Vnom = (decimal)(row["Vnom"] ?? -1);

            return currency;
        }
    }
}

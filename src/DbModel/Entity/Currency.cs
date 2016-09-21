using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CbrFT.DbModel.Entity
{
    public class Currency
    {
        [Key]
        public int CurrencyId { get; set; }

        public string Vname { get; set; }
        public string VchCode { get; set; }    // ISO Цифровой код валюты
        public int Vcode { get; set; }    // ISO Символьный код валюты
        public decimal Vnom { get; set; }    // Номинал

        public virtual List<CurrencyValue> Values { get; set; } 
    }
}

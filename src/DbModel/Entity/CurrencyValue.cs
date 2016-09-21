using System;
using System.ComponentModel.DataAnnotations;

namespace CbrFT.DbModel.Entity
{
    public class CurrencyValue
    {
        [Key]
        public int CurrencyValueId  { get; set; }

        public Currency Currency    { get; set; }
        public decimal  Vcurs       { get; set; }    // Курс
        public DateTime OnDate      { get; set; }    // date and time
    }
}

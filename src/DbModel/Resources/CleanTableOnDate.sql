/*DELETE FROM [dbo].[ValuteCursOnDates]
      WHERE (OnDate >= @FromDate) AND (OnDate <= @ToDate)*/
DELETE FROM [dbo].[CurrencyValues]
      WHERE (OnDate >= {0}) AND (OnDate <= {1})
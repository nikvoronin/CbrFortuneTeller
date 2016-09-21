# CBR Fortune Teller

"The Scraper" downloads daily currencies values through the [WebService](https://www.cbr.ru/DailyInfoWebServ/DailyInfo.asmx) of the Central Bank of Russia. See https://www.cbr.ru/scripts/Root.asp?PrtId=DWS After that, it saves data at database with help of Entity Framework. And finally, "The Medium" could predict currency exchange rates.


## Installation

- Prepare MS SQL 2014 database.
- Configure "connectionString" in Medium.exe.config and Scraper.exe.config.
- Install [R-language](https://cran.gis-lab.info/) (both x86 and x64 versions).
- Run the Scraper.
- Run the Medium and would get you prediction.


## The Scraper

scraper.exe scrape -f2016-01-01 -t2016-09-01

- clear - Clean data base table.
- scrape - Scrape currencies values.
- help - Display more information on a specific command.
- version - Display version information.


### scrape

-d, --delay                     (Default: 1000) Download delay in ms.
-r, --delay-after-n-requests    (Default: 30) Should delay after N requests
-f, --from                      From which date should begin: DD-MM-YYYY
-t, --to                        Which date should finish: DD-MM-YYYY


### clear

-f, --from    From which date should begin: DD-MM-YYYY
-t, --to      Which date should finish: DD-MM-YYYY


## The Medium

medium.exe next -cEUR -n7 --from2016-01-01

- list - Prints list of the currencies.
- next - Options for regression analisys.
- help - Display more information on a specific command.
- version - Display version information.


### next

-c, --code               Required. (Default: USD) 3-symbols code of the currency
-o, --output-filename    Where to store prediciton
-n, --next               Required. (Default: 1) After how many days to make a prediction
-f, --from               From which date should begin: DD-MM-YYYY
-t, --to                 Which date should finish: DD-MM-YYYY

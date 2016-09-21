@echo off
if exist .\debug\scraper.exe .\debug\scraper.exe scrape -f 01-01-2016
if not exist .\debug\scraper.exe .\release\scraper.exe scrape -f 01-01-2016
pause
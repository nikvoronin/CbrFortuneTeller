@echo off
if exist .\debug\scraper.exe .\debug\scraper.exe clear
if not exist .\debug\scraper.exe .\release\scraper.exe clear
pause
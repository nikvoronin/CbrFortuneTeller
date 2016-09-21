@echo off
if exist .\debug\medium.exe .\debug\medium.exe next -cEUR -n7
::-oSimonSez.txt
if not exist .\debug\medium.exe .\release\medium.exe next -cEUR -n7
::-oSimonSez.txt
pause
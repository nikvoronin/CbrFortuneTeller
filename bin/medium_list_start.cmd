@echo off
if exist .\debug\medium.exe .\debug\medium.exe list
if not exist .\debug\medium.exe .\release\medium.exe list
pause
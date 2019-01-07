echo off
for /F "usebackq tokens=1,2,3 delims=/" %%I IN (`echo %date%`) do set formatdate=%%K%%J%%I
cd C:\Users\Administrator\Documents\backup-service\
C:\Users\Administrator\Documents\backup-service\batchcopy.bat > C:\Users\Administrator\Documents\backup-service\logs\backup-%formatdate%.log
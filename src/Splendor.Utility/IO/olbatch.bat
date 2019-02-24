echo off
rem Get start time:
for /F "tokens=1-4 delims=:.," %%a in ("%time%") do (
   set /A "start=(((%%a*60)+1%%b %% 100)*60+1%%c %% 100)*100+1%%d %% 100"
)
for /F "usebackq tokens=1,2,3 delims=/" %%I IN (`echo %date%`) do set formatdate=%%K%%J%%I
echo #######################################################################################################################
echo #                                Backup Splendor del giorno %formatdate%                                                  #
echo #######################################################################################################################
echo Inizio preparazionone variabili
rem -mx=9 zip ultra ma non serve a molto ... è ci mette il triplo del tempo
set home-folder=C:\Users\Administrator\Documents\backup-service\
set home-backup=\\splendornas\backup
set home-contab=\\splendornas\Contabilita
set home-foto=\\splendornas\foto
set home-wgescar=\\splendornas\WGESTCAR
set home-outlook=\\splendornas\outlook 
set home-video=\\splendornas\VideoTV 
set home-wincar=E:\wincar
set home-winmec=\\server-2\winmec
set username=microsoftlocal\silvia
set username2=microsoftlocal\admin
set username3=microsoftlocal\Administrator
set password=Splendor1!
set password2=Splendor123!
set wincar-filename=wincar-%formatdate%.zip
set wincar-archivi=wincar-archivi.zip
set wincar-contab=wincar-contab.zip
set wincar-fatt-electr=wincar-fatt-electr.zip
set winmec-filename=winmec%formatdate%.zip
set winmec-archivi=winmec-archivi.zip
set winmec-contab=winmec-contab.zip
set winmec-fatt-electr=winmec-fatt-electr.zip
set wintouch-splendor=wintouch.splendor.zip
set wintouch-continental=wintouch.splendor.zip
set contab=contab%formatdate%.zip
set foto=foto%formatdate%.zip
set wgescar=wgescar%formatdate%.zip
set outlook=outlook%formatdate%.zip
set video=video%formatdate%.zip
echo Fine preparazionone variabili
echo Mi sposto nella cartella %homeFolder%
cd %homeFolder%
echo controllo se la cartella backup esiste

IF NOT EXIST %homeFolder%backup\ (
    echo la cartella non esiste la creo
    mkdir backup
    echo la cartella e stata creata
)
echo Mi sposto nella cartella %homeFolder%backup
cd backup
echo #######################################################################################################################
echo #                                               Backup WinCar inizio                                                  #
echo #######################################################################################################################
REM if not exist %wincar-filename% (
if exist %wincar-filename% (
    mkdir wincar
    cd wincar
    7z a -r  %wincar-archivi% "%home-wincar%\archivi\*.*"
    7z a -r  %wincar-contab% "%home-wincar%\contab\*.*"
    7z a -r  %wincar-fatt-electr% "%home-wincar%\FattureElettroniche\*.*"
    cd ..
    7z a -r  %wincar-filename% "wincar\*.*"
    rd /Q /S wincar
)
echo #######################################################################################################################
echo #                                               Backup WinCar fine                                                    #
echo #######################################################################################################################
echo #######################################################################################################################
echo #                                               Backup WinMec inizio                                                  #
echo #######################################################################################################################
REM if not exist %winmec-filename% (
if exist %winmec-filename% (
    if exist V:\NUL (
        echo V: essite gia smonto la directory
        net use V: /delete
        echo V: Smontata la rimonto
        net use v: %home-winmec%  /user:%username3% %password2% /persistent:no
    ) else (
        echo monto V:
        net use v: %home-winmec%  /user:%username3% %password2% /persistent:no
    )
    mkdir winmec
    cd winmec
    7z a -r  %winmec-archivi% "V:\archivi\*.*"
    7z a -r  %winmec-contab% "V:\contab\*.*"
    7z a -r  %winmec-fatt-electr% "V:\FattureElettroniche\*.*"
    cd ..
    7z a -r  %winmec-filename% "winmec\*.*"
    rd /Q /S winmec
)
echo #######################################################################################################################
echo #                                               Backup WinMec fine                                                    #
echo #######################################################################################################################
echo #######################################################################################################################
echo #                                               Backup Wintouch inizio                                                #
echo #######################################################################################################################
if not exist %wintouch-splendor% (
    mkdir wintouch-splendor
    cd wintouch-splendor
    7z a -r  winnc.splendor.zip "C:\winnc\*.*" 
    7z a -r  winntouch.splendor.zip "C:\winntouch\*.*"
    7z a -tzip winntouch.splendor.ini.zip "C:\Windows\wincheck.ini" "C:\Windows\winnc.ini"
    cd ..
    7z a -r  %wintouch-splendor% "wintouch-splendor\*.*"
    rd /Q /S wintouch-splendor
)
echo #######################################################################################################################
echo #                                               Backup Wintouch fine                                                  #
echo #######################################################################################################################
echo #######################################################################################################################
echo #                                               Backup Wintouch inizio                                                #
echo #######################################################################################################################
if not exist %wintouch-continental% (
    if exist V:\NUL (
        echo V: essite gia smonto la directory
        net use V: /delete
        echo V: Smontata la rimonto
        net use v: \\server-2\C$  /user:%username3% %password2% /persistent:no
    ) else (
        echo monto V:
        net use v: \\server-2\C$  /user:%username3% %password2% /persistent:no
    )
    mkdir wintouch-continental
    cd wintouch-continental
    7z a -r  winnc.continental.zip "V:\winnc\*.*" 
    7z a -r  winntouch.continental.zip "V:\winntouch\*.*"
    7z a -tzip winntouch.continental.ini.zip "V:\Windows\wincheck.ini" "C:\Windows\winnc.ini"
    cd ..
    7z a -r  %wintouch-continental% "wintouch-continental\*.*"
    rd /Q /S wintouch-continental
)
echo #######################################################################################################################
echo #                                               Backup Wintouch fine                                                  #
echo #######################################################################################################################
echo #######################################################################################################################
echo #                                               Backup Contabilita inizio                                             #
echo #######################################################################################################################
if not exist %contab% (
    if exist V:\NUL (
        echo V: essite gia smonto la directory
        net use V: /delete
        echo V: Smontata la rimonto
        net use v: %home-contab% /persistent:no
    ) else (
        echo monto V:
        net use v: %home-contab% /persistent:no
    )
    7z a -r  %contab% "V:\*.*"
)
echo #######################################################################################################################
echo #                                               Backup Contabilita fine                                               #
echo #######################################################################################################################
echo #######################################################################################################################
echo #                                               Backup Foto inizio                                                    #
echo #######################################################################################################################
if not exist %foto% (
    if exist V:\NUL (
        echo V: essite gia smonto la directory
        net use V: /delete
        echo V: Smontata la rimonto
        net use v: %home-foto% /persistent:no
    ) else (
        echo monto V:
        net use v: %home-foto% /persistent:no
    )
    7z a -r  %foto% "V:\*.*" -v5g
)
echo #######################################################################################################################
echo #                                               Backup Foto fine                                                      #
echo #######################################################################################################################
echo #######################################################################################################################
echo #                                               Backup Wgescar inizio Sospeso                                         #
echo #######################################################################################################################
REM if not exist %wgescar% (
REM     if exist V:\NUL (
REM         echo V: essite gia smonto la directory
REM         net use V: /delete
REM         echo V: Smontata la rimonto
REM         net use v: %home-wgescar%  /user:%username2% %password% /persistent:no
REM     ) else (
REM         echo monto V:
REM         net use v: %home-wgescar%  /user:%username2% %password% /persistent:no
REM     )
REM     7z a -r  %wgescar% "V:\*.*"
REM )
echo #######################################################################################################################
echo #                                               Backup Wgescar fine Sospeso                                           #
echo #######################################################################################################################
echo #######################################################################################################################
echo #                                               Backup Outlook inizio                                                 #
echo #######################################################################################################################
if not exist %outlook% (
    7z a -r  %outlook% "Z:\*.*"
)
echo #######################################################################################################################
echo #                                               Backup Outlook fine                                                   #
echo #######################################################################################################################
echo #######################################################################################################################
echo #                                               Backup Video inizio                                                   #
echo #######################################################################################################################
if not exist %video% (
    if exist V:\NUL (
        echo V: essite gia smonto la directory
        net use V: /delete
        echo V: Smontata la rimonto
        net use v: %home-video% /persistent:no
    ) else (
        echo monto V:
        net use v: %home-video% /persistent:no
    )
    7z a -r  %video% "V:\*.*"
)
echo #######################################################################################################################
echo #                                               Backup Video fine                                                     #
echo #######################################################################################################################
echo #######################################################################################################################
echo #                                          Pulizia dei vecchi file inizio                                             #
echo #######################################################################################################################
echo Anno = %formatdate~0,4%
echo Mese = %formatdate~5,2%
echo Giorno = %formatdate~7,2%
echo #######################################################################################################################
echo #                                          Pulizia dei vecchi file fine                                               #
echo #######################################################################################################################
if exist V:\NUL (
    echo V: essite gia smonto la directory
    net use V: /delete
    echo V: Smontata la rimonto
    net use v: %home-backup% /persistent:no
) else (
    echo monto V:
    net use v: %home-backup% /persistent:no
)
echo %formatdate%

if not exist V:\%formatdate% (
    V:
    mkdir %formatdate%
    C:
)
xcopy *.zip* V:\%formatdate%\  /y
cd ..
rd /Q /S backup
rem Get end time:
for /F "tokens=1-4 delims=:.," %%a in ("%time%") do (
   set /A "end=(((%%a*60)+1%%b %% 100)*60+1%%c %% 100)*100+1%%d %% 100"
)
rem Get elapsed time:
set /A elapsed=end-start
rem Show elapsed time:
set /A hh=elapsed/(60*60*100), rest=elapsed%%(60*60*100), mm=rest/(60*100), rest%%=60*100, ss=rest/100, cc=rest%%100
if %mm% lss 10 set mm=0%mm%
if %ss% lss 10 set ss=0%ss%
if %cc% lss 10 set cc=0%cc%
echo %hh%:%mm%:%ss%,%cc%
if exist V:\NUL (
    net use V: /delete
)

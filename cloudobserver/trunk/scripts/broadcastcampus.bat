@echo off

echo Starting Campus 1 broadcast...
start "Campus 1 Broadcast" /min %CloudObserverBin%\CloudObserver.ConsoleApps.Broadcast 21 1 %CloudObserverController% JPEG http://www.campus.spbu.ru/images/webcam/camera1.jpg false
if not %CloudObserverDelay%==0 %CloudObserverBin%\CloudObserver.ConsoleApps.Delay %CloudObserverDelay%

echo Starting Campus 2 broadcast...
start "Campus 2 Broadcast" /min %CloudObserverBin%\CloudObserver.ConsoleApps.Broadcast 22 1 %CloudObserverController% JPEG http://www.campus.spbu.ru/images/webcam/camera2.jpg false
if not %CloudObserverDelay%==0 %CloudObserverBin%\CloudObserver.ConsoleApps.Delay %CloudObserverDelay%

echo Starting Campus 3 broadcast...
start "Campus 3 Broadcast" /min %CloudObserverBin%\CloudObserver.ConsoleApps.Broadcast 23 1 %CloudObserverController% JPEG http://www.campus.spbu.ru/images/webcam/camera3.jpg false
if not %CloudObserverDelay%==0 %CloudObserverBin%\CloudObserver.ConsoleApps.Delay %CloudObserverDelay%

echo Starting Campus 4 broadcast...
start "Campus 4 Broadcast" /min %CloudObserverBin%\CloudObserver.ConsoleApps.Broadcast 24 1 %CloudObserverController% JPEG http://www.campus.spbu.ru/images/webcam/camera4.jpg false
if not %CloudObserverDelay%==0 %CloudObserverBin%\CloudObserver.ConsoleApps.Delay %CloudObserverDelay%

echo Starting Campus 5 broadcast...
start "Campus 5 Broadcast" /min %CloudObserverBin%\CloudObserver.ConsoleApps.Broadcast 25 1 %CloudObserverController% JPEG http://www.campus.spbu.ru/images/webcam/camera5.jpg false
if not %CloudObserverDelay%==0 %CloudObserverBin%\CloudObserver.ConsoleApps.Delay %CloudObserverDelay%
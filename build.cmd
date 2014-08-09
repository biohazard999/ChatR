@echo off
tools\nuget\nuget.exe update -Self
tools\nuget\nuget.exe install Fake -OutputDirectory tools -ExcludeVersion

fast_build.bat %*
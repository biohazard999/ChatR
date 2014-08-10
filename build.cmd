@echo off
tools\nuget\nuget.exe update -Self
tools\nuget\nuget.exe install Fake -OutputDirectory tools -ExcludeVersion

tools\fake\tools\fake.exe build.fsx %*
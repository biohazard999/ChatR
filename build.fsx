#r "tools/FAKE/tools/FakeLib.dll"

open Fake

let outdir = "bin"
let outdirServer = outdir + "/ChatR.Server"
let outdirWinClient = outdir + "/ChatR.WinClient"
let outdirWpfClient = outdir + "/ChatR.WpfClient"
let outdirWebClient = outdir + "/ChatR.WebClient"

let serverProj =    !! "src/ChatR.Server/ChatR.Server*.csproj"
let winClientProj = !! "src/ChatR.WinClient/ChatR.WinClient*.csproj"
let wpfClientProj = !! "src/ChatR.WpfClient/ChatR.WpfClient*.csproj"
let webClientProj = !! "src/ChatR.WebClient/ChatR.WebClient*.csproj"

Target "Clear" (fun _ -> 
    CleanDir outdir
)

Target "Build" (fun _ -> 
    
    MSBuildRelease outdirServer "Build" serverProj
        |> Log "ChatR.Server-Output: "

    MSBuildRelease outdirWinClient "Build" winClientProj
        |> Log "ChatR.WinClient-Output: "

    MSBuildRelease outdirWpfClient "Build" wpfClientProj
        |> Log "ChatR.WpfClient-Output: "

    MSBuildRelease outdirWebClient "Build" webClientProj
        |> Log "ChatR.WebClient-Output: "
)


"Clear"
    ==> "Build"


RunTargetOrDefault "Build"

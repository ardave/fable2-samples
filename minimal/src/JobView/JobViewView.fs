module Job.View.View

open System
open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Job.View.Types


let private split (ch:char) (options:StringSplitOptions) (s:string) =
    s.Split([|ch|], options)

let private joinWithSemicolons lst = lst |> Array.ofList |> fun (arr:string array) -> String.Join(";", arr)

let private splitClean ch s = split ch StringSplitOptions.RemoveEmptyEntries s

let renderJobOption (jobDtoOpt:DTOs.Job option) =
    let jobOpt = jobDtoOpt |> Option.map (Domain.Job.FromDto Domain.Job.Direction.FromServer)
    match jobOpt with
    | Some job ->
        let paragraphs =
            job.Description
            |> splitClean '\n'
            |> Array.map str
            |> List.ofArray
            |> List.fold(fun acc elem -> [br[]; br[]; elem]@acc) []
            |> List.rev

        let stringToTag s = span [ ClassName "tag" ] [ str s ]

        let listToTags lst =
            div [ ClassName "tags" ]
                (lst |> List.map stringToTag)

        let languages = job.SelectedLanguages   |> listToTags
        let frameworks = job.SelectedFrameworks |> listToTags

        let (Domain.Job.RemoteWork remoteWorkStr) = job.RemoteWork

        // div []
        let introElems =    [
                h1[ ClassName "subtitle" ][ str "Title"]
                str job.Title
                br[]
                br[]
                h1[ ClassName "subtitle"] [ str "Languages"]
                languages
                br[]
                h1[ ClassName "subtitle" ] [ str "Frameworks" ]
                frameworks
                br[]
                h1[ ClassName "subtitle" ][ str "Remote Work Style" ]
                (remoteWorkStr |> stringToTag)
                br[]
                br[]
                h1[ ClassName "subtitle" ][ str "Description" ]
             ]
        let allElems = introElems @ paragraphs
        div [] allElems
    | None     ->
        div [][ str "Loading job details ... " ]

let root model dispatch =
    match model.CurrentState with
    | Nothing ->
        div [][ str "Waiting for ID of Job to load ... " ]
    | Loading jobId ->
        div [][ str "Loading job details ... " ]
    | Loaded jobDtoOption ->
        renderJobOption jobDtoOption
    | Error ex ->
        div [][ str "Error ... " ]
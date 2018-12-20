module Job.Search.View 

open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Job.Search.Types
open Domain.Job
open DTOs
open Sieve.Domain

let toList x = [x]

let private titleElements (jobs:Domain.Job.Job list) =
    jobs
    |> List.map(fun job ->
        match job.Id with
        | Some (JobId idStr) ->
            a[Href (sprintf "/#jobview/%s" idStr)]
                [
                    str job.Title
                    br[]
                ]
        | None ->
            str job.Title
    )

let jobsContent model =
    match model.Status with
    | Nothing       ->
        []
    | Loading       ->
        h1 [ ClassName "title is-3" ] [ str "Loading recent jobs ..." ]
        |> toList
    | ShowMessage m -> 
        h1 [ ClassName "title is-3" ] [ str m ]
        |> toList
    | Loaded jobs   ->
        titleElements jobs

let root model dispatch =
    let searchBox = SearchBox.searchBox dispatch SearchTextChanged SubmitSearch model.SearchQuery
    let langLabel = "Required Programming Languages"
    let frameworkLabel = "Required Frameworks"
    let languageDropdowns = AddableDropdown.createDropdowns dispatch model.SelectedLanguages DataOptions.programmingLanguages langLabel LanguageSelectionChanged RemoveProgrammingLanguage AddProgrammingLanguage
    let frameworkDropdowns = AddableDropdown.createDropdowns dispatch model.SelectedFrameworks DataOptions.frameworks frameworkLabel FrameworkSelectionChanged RemoveFramework AddFramework
    div []
        [
            div [ ClassName "box" ]
                (searchBox@languageDropdowns@frameworkDropdowns)
            div []
                (jobsContent model)
        ]
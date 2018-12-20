module Job.Search.State

open Elmish
open Fable.Core
open Job.Search.Types
open Sieve.Domain

let Init() = JobSearchModel.Empty, []

let update msg model =
    match msg with
    | SearchTextChanged s ->
        { model with SearchQuery = s }, []
    | SubmitSearch _ ->
        let cmd = Cmd.ofAsync MyRemoting.sieveApiProxy.Search model.ToDto SearchResponse SearchError
        printfn "Submitting search for:\n%A" model
        model, cmd
    | SearchResponse jobList ->
        let jobs =
            jobList
            |> List.map (Domain.Job.FromDto Domain.Job.Direction.FromServer)
        let status =
            match jobs with
            | [] -> ShowMessage "Search returned no results."
            | _  -> Loaded jobs
        { model with Status = status }, []
    | SearchError ex ->
        { model with Status = ShowMessage ex.Message }, []
    | AddProgrammingLanguage ->
        if not model.SelectedLanguages.ContainsAnyUnselected then
            model.SelectedLanguages.SelectedItems.Add DataOptions.nothingSelected
        model, []
    | LanguageSelectionChanged (s, idx) ->
        model.SelectedLanguages.SelectedItems.[idx] <- s
        model, []
    | RemoveProgrammingLanguage idx ->
        model.SelectedLanguages.SelectedItems.RemoveAt idx
        model, []
    | AddFramework ->
        if not model.SelectedFrameworks.ContainsAnyUnselected then
            model.SelectedFrameworks.SelectedItems.Add DataOptions.nothingSelected
        model, []
    | FrameworkSelectionChanged (s, idx) ->
        model.SelectedFrameworks.SelectedItems.[idx] <- s
        model, []
    | RemoveFramework idx ->
        model.SelectedFrameworks.SelectedItems.RemoveAt idx
        model, []

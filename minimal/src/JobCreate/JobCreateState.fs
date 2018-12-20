module Job.Create.State

open Elmish
open Auth.Common
open Job.Create.Types
open Sieve.Domain
open Job.Create.Types

let init () : JobCreateModel * Cmd<JobCreateMessage> =
  {JobCreateModel.Empty with
    SelectedLanguages = { SelectedItems = ResizeArray(["C#"; "F#"]); ShowCannotAddError = false }
    SelectedFrameworks = { SelectedItems = ResizeArray(["Django"; "Rails"; "Ember"]); ShowCannotAddError = false }
    }, []

let rec remove i l =
    match i, l with
    | 0, x::xs -> xs
    | i, x::xs -> x::remove (i - 1) xs
    | i, [] -> failwith "index out of range"

let update msg model =
    match msg with
    | TitleChanged       s ->
      { model with Title = s }, []
    | DescriptionChanged s ->
      { model with Description = s}, []
    | LanguageSelectionChanged (s, idx) ->
      model.SelectedLanguages.SelectedItems.[idx] <- s
      model, []
    | FrameworkSelectionChanged (s, idx) ->
      model.SelectedFrameworks.SelectedItems.[idx] <- s
      model, []
    |  RemoteSelectionChanged (s, _) ->
      { model with RemoteWork = Domain.Job.RemoteWork s }, []
    | SubmitWithToken token ->
      let securedJob = SecureRequest.Create token.Token model.ToDto
      { model with SubmissionInProgress = true }, Cmd.ofAsync MyRemoting.sieveApiProxy.CreateJob securedJob JobPostResponse JobPostError
    | JobPostResponse _ | JobPostError _ | Submit->
      failwith "Should be handled at a higher level"
    | AddProgrammingLanguage ->
      if not model.SelectedLanguages.ContainsAnyUnselected then
        model.SelectedLanguages.SelectedItems.Add DataOptions.nothingSelected
      model, []
    | AddFramework ->
      if not model.SelectedFrameworks.ContainsAnyUnselected then
        model.SelectedFrameworks.SelectedItems.Add DataOptions.nothingSelected
      model, []
    | RemoveFramework x ->
      model.SelectedFrameworks.SelectedItems.RemoveAt x
      model, []
    | RemoveProgrammingLanguage x ->
      model.SelectedLanguages.SelectedItems.RemoveAt x
      model, []


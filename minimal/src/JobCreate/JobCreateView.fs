module Job.Create.View

open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Job.Create.Types
open Sieve.Domain

let languageDropdowns dispatch model =
    AddableDropdown.createDropdowns dispatch model.SelectedLanguages DataOptions.programmingLanguages "Programming Language(s)" LanguageSelectionChanged RemoveProgrammingLanguage AddProgrammingLanguage

let frameworkDropdowns dispatch model =
    AddableDropdown.createDropdowns dispatch model.SelectedFrameworks DataOptions.frameworks "Programming Framework(s)" FrameworkSelectionChanged RemoveFramework AddFramework

let root model dispatch =
  div
    [ ClassName "content" ]
    [ h1 [] [ str "Create Job" ]
      div [ ClassName "field" ]
          [
             label [ ClassName "label" ][ str "Job Title" ]
             div [ ClassName "control" ]
                 [
                     input [
                             ClassName "input is-success"
                             Placeholder "Job Title"
                             Type "text"
                             AutoFocus true
                             Value model.Title
                             Required true
                             OnChange (fun ev -> !! ev.target?value |> TitleChanged |> dispatch)
                             Disabled false
                           ]
                 ]
          ]

      div [ ClassName "box" ] (languageDropdowns dispatch model)

      div [ ClassName "box" ] (frameworkDropdowns dispatch model)

      div [ ClassName "box" ]
        [
            label [ ClassName "label" ][ str "Remote working option" ]
            Dropdown.createDropdown dispatch DataOptions.remoteWorkOptions None JobCreateMessage.RemoteSelectionChanged 0
        ]

      div [ ClassName "field" ]
          [
             label [ ClassName "label" ][ str "Job Description" ]
             div [ ClassName "control" ]
                 [
                     textarea [
                             ClassName "text"
                             Placeholder "Job Description"
                             Rows 20.
                             Cols 95.
                             Value model.Description
                             Required true
                             OnChange (fun ev -> !! ev.target?value |> DescriptionChanged |> dispatch)
                             Disabled false
                           ][]
                 ]
             div [ ClassName "control" ]
                 [
                     button 
                         [ 
                             ClassName ("button is-text is-primary" + if model.SubmissionInProgress then " is-loading" else "" )
                             OnClick (fun _ -> JobCreateMessage.Submit |> dispatch)
                         ]
                         [
                             i [ ClassName "fas fa-lock fa-fw" ][]
                             str "Submit"
                         ]
                 ]
        ]
    ]

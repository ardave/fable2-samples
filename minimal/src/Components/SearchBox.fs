module SearchBox

open Fable.Core
open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props

let searchBox dispatch onTextChanged onSearchSubmitted searchQuery =
    [
        label [ ClassName "label" ][ str "Search Jobs" ]
        div [ ClassName "control" ]
            [
                input [
                        ClassName "input is-success"
                        Placeholder "Search Jobs"
                        Type "text"
                        AutoFocus true
                        Value searchQuery
                        Required true
                        OnChange (fun ev -> !! ev.target?value |> onTextChanged |> dispatch)
                        Disabled false
                      ]
           ]
        div [ ClassName "control" ]
            [
                br[]
                button 
                    [ 
                        ClassName "button is-text is-primary"// + if model.SubmissionInProgress then " is-loading" else "" )
                        OnClick (fun _ -> onSearchSubmitted searchQuery |> dispatch)
                    ]
                    [
                        i [ ClassName "fas fa-lock fa-fw" ][]
                        str "Submit"
                    ]
            ]
    ]

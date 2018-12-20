module SignUp.View

open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.Import.Browser
open Auth.Common
open SignUp.Types

let mutable email = ""
let mutable password = ""

let root model dispatch =
    div [ ClassName "control" ]
        [
            label [ ClassName "label" ][ str model.StatusMessage ]
            input [
                    ClassName "input is-success"
                    Placeholder "Email Address"
                    Type "text"
                    AutoFocus true
                    // Value email
                    Required true
                    OnChange (fun ev -> email <- !! ev.target?value)
                    Disabled false
                  ]
            input [
                    ClassName "input is-success"
                    Placeholder "Password"
                    Type "password"
                    AutoFocus false
                    // Value password
                    Required true
                    OnChange (fun ev -> password <- !! ev.target?value)
                    Disabled false
                  ]
            button 
                [ 
                    ClassName ("button is-text is-primary")
                    OnClick (fun _ ->
                        let signUpRequest = { Email = Email email; Password = Password password }
                        SignUpMessage.Submit signUpRequest |> dispatch)
                ]
                [
                    i [ ClassName "fas fa-lock fa-fw" ][]
                    str "Submit"
                ]
        ]
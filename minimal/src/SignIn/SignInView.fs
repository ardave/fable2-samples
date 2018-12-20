module SignIn.View 
open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.Import.Browser
open Auth.Common
open SignIn.Types

let mutable email = ""
let mutable password = ""

let signInForm model dispatch message =
    div [ ClassName "control" ]
        [
            label [ ClassName "label" ][ str message ]
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
                        let signInRequest = { Email = Email(email.Trim()); Password = Password password; }
                        SignInMessage.Submit signInRequest |> dispatch)
                ]
                [
                    i [ ClassName "fas fa-lock fa-fw" ][]
                    str "Submit"
                ]
        ]


let root model dispatch = 
    match model.SignInInfo with
    | SignedIn (securityToken, email) ->
        let (Email emailStr) = model.Email
        label [ ClassName "label" ][ str (sprintf "Successfully signed in: %s" emailStr)]
    | SignInInfo.SignInError msg ->
        signInForm model dispatch msg
    | NotSignedIn ->
        signInForm model dispatch  "Enter Email and Password to sign in:"

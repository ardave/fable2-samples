module SignUp.State

open Elmish
open SignUp.Types

let Init() =
    SignUpModel.Empty, ()

let update msg model =
    match msg with
    | Submit signUpRequest ->
        { model with SubmissionInProgress = true }, Cmd.ofAsync MyRemoting.sieveApiProxy.SignUp signUpRequest SignUpResponseReceived SignUpError
    | SignUpResponseReceived result ->
        match result with
        | Ok _ ->
            { model with StatusMessage = "Account successfully created" }, []
        | Error s ->
            { model with StatusMessage = s }, []
    | SignUpError exn ->
        { model with StatusMessage = sprintf "Error: %s" exn.Message }, []

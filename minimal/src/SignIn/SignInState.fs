module SignIn.State

open Auth
open SignIn.Types
open Elmish

let update msg signInModel =
    let signInModel =
        match signInModel.SignInInfo with
        | SignedIn _ ->
            { signInModel with SignInInfo = NotSignedIn }
        | _          ->
            signInModel

    let newModel, cmd =
        match msg with
        | Submit signInRequest ->
            { signInModel with SubmissionInProgress = true }, Cmd.ofAsync MyRemoting.sieveApiProxy.SignIn signInRequest SignInResponseReceived SignInError
        | SignInResponseReceived result ->
            match result with
            | Ok (securityToken, email) ->
                { signInModel with SignInInfo = SignedIn(securityToken, email) }, []
            | Error e ->
                { signInModel with SignInInfo = SignInInfo.SignInError e }, []
        | SignInError ex ->
            { signInModel with SignInInfo = SignInInfo.SignInError ex.Message }, []
        | SignOut ->
            { signInModel with SignInInfo = NotSignedIn }, []
    newModel, cmd

let Init() =
    SignInModel.Empty, []
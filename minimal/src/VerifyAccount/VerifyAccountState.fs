module VerifyAccount.State

open Elmish
open VerifyAccount.Types

let Init() =
    let model:VerifyAccountModel =
        { CurrentState = Nothing }
    model, []

let update msg model =
    match msg with
    | InitialLoad token ->
        printfn "InitialLoad"
        let cmd = Cmd.ofAsync MyRemoting.sieveApiProxy.VerifyAccount token VerifyAccountSuccess VerifyAccountError
        model, cmd
    | VerifyAccountSuccess rslt ->
        printfn "VerifyAccountSuccess"
        match rslt with
        | Ok _ -> model, []
        | Error e -> model, []
    | VerifyAccountError excn ->
        printfn "VerifyAccountError"
        model, []




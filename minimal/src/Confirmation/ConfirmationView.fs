module Confirmation.View

open Fable.Core
open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open DTOs

let root model dispatch =
    match model with
    | Ok (JobId jid) ->
        let okText = sprintf "Your request has been submitted.  The newly-created job ID is %s" jid
        div
            [ ]
            [
                p[]
                    [
                        str okText
                    ]
            ]
    | Error msg ->
        div
            [ ]
            [
                p[]
                    [
                        b   []
                            [
                                str "We're sorry, an error has occurred."
                                br []
                                str msg
                            ]
                    ]
            ]

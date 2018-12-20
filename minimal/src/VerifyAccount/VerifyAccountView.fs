module VerifyAccount.View

open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props

let root model token dispatch =
    div []
        [ str "Here is the verify account view."
          str token ]

module Dropdown

open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Sieve.Domain

let createDropdownOptions strList currentlySelected =
    strList
    |> List.sort
    |> (fun lst -> DataOptions.nothingSelected::lst)
    |> List.map(fun item ->
        let shouldBeSelected =
            match currentlySelected with
            | Some s -> s = item
            | None   -> false
        option [ Value item; Selected shouldBeSelected ] [ str item ])

let createDropdown dispatch strList currentlySelected onChangeMsg (idx:int) =
    select [OnChange (fun ev -> ((!! ev.target?value, idx)) |> onChangeMsg |> dispatch)]
           (createDropdownOptions strList currentlySelected)
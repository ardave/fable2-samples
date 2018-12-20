module AddableDropdown

open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Sieve.Domain

type SelectionCollection = {
    SelectedItems         : ResizeArray<string>
    ShowCannotAddError    : bool
} with
    member x.ContainsAnyUnselected =
        printfn "Selected items:\n%A" x.SelectedItems
        x.SelectedItems.Contains DataOptions.nothingSelected
    static member Empty = {
        SelectedItems = (ResizeArray())
        ShowCannotAddError = false
    }
    static member Create strLst = { SelectedItems = strLst; ShowCannotAddError = false }

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

let dropdownDeleteButton msg idx dispatch =
    a [ 
        ClassName "button is-small is-danger"
        OnClick (fun _ -> msg idx |> dispatch)
      ]
      [
        span [ ClassName "icon is-small" ]
             [
                i [ ClassName "fas fa-times" ][]
             ]
      ]

let createDropdown dispatch strList currentlySelected onChangeMsg (idx:int) =
    select [OnChange (fun ev -> ((!! ev.target?value, idx)) |> onChangeMsg |> dispatch)]
           (createDropdownOptions strList currentlySelected)

let createAddItemButton dispatch onAddItemMsg =
    a [ 
          ClassName "button is-success is-small"
          OnClick (fun _ -> onAddItemMsg |> dispatch)
        ]
        [
            span [ ClassName "icon is-small" ]
                [
                    i [ ClassName "fas fa-plus" ][]
                ]
        ]

let createDropdowns dispatch selectionCollection availableOptions lbl onChangeMsg onDeleteMsg onAddItemMsg =
    // Create at least one dropdown with all available options even if no
    // options were previously selected:
    let label = label [ ClassName "label" ][ str lbl ]
    let addItemButton = createAddItemButton dispatch onAddItemMsg
    let dropdownsForSelectedItems = 
        match selectionCollection.SelectedItems.Count with
        | 0 ->
            [
                createDropdown dispatch availableOptions None onChangeMsg 0
                dropdownDeleteButton onDeleteMsg 0 dispatch
            ]
        | _  ->
            let lame = ResizeArray()
            selectionCollection.SelectedItems
            |> Seq.iteri(fun idx lang ->
               lame.Add <| createDropdown dispatch availableOptions (Some lang) onChangeMsg idx
               lame.Add <| dropdownDeleteButton onDeleteMsg idx dispatch
               )
            lame |> List.ofSeq
    
    label::addItemButton::dropdownsForSelectedItems

module Navbar.View

open Fable.Helpers.React
open Fable.Helpers.React.Props
open SignIn.Types


type ButtonAction =
| Link of string
| Click

let navButton classy buttonAction faClass txt dispatch =
    let httpAttr:IHTMLProp =
        match buttonAction with
        | Link str -> Href str                               :> IHTMLProp
        | Click    -> OnClick (fun _ -> SignOut |> dispatch) :> IHTMLProp

    p
        [ ClassName "control" ]
        [ a
            [ ClassName (sprintf "button %s" classy)
              httpAttr ]
            [ span
                [ ClassName "icon" ]
                [ i
                    [ ClassName (sprintf "fa %s" faClass) ]
                    [ ] ]
              span
                [ ]
                [ str txt ] ] ]


let navButtons signInInfo dispatch =
    match signInInfo with
    | SignedIn _ ->
        span
            [ ClassName "nav-item" ]
            [ div
                [ ClassName "field is-grouped" ]
                [ navButton "twitter" Click "fa-twitter" "Sign Out" dispatch ] ]
    | _ ->
        div []
            [
                div
                    [ ClassName "nav-item" ]
                    [ div
                        [ ClassName "field is-grouped" ]
                        [ navButton "github" (Link(Global.toHash Global.Page.SignIn)) "fa-twitter" "Sign In" dispatch
                          navButton "github" (Link(Global.toHash Global.Page.SignUp)) "fa-twitter" "Sign Up" dispatch ] ]
            ]

let root signInInfo dispatch =
    nav
        [ ClassName "nav" ]
        [ div
            [ ClassName "nav-left" ]
            [ h1
                [ ClassName "nav-item is-brand title is-4" ]
                [ str "Sieve" ] ]
          div
            [ ClassName "nav-right" ]
            [ navButtons signInInfo dispatch ] ]

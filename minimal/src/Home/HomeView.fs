module Home.View

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Home.Types
open DTOs
open Domain.Job

let private toList elem = [elem]

let private titleElements jobs =
    match jobs with
    | [] ->
        h1 [ ClassName "title is-3" ] [ str "No recent jobs found." ]
        |> toList
    | _  ->
        jobs
        |> List.map(fun job ->
            match job.Id with
            | Some (JobId idStr) ->
                let pageViewHash = Global.toHash (Global.Page.JobView "")
                a[Href (sprintf "/%s/%s" pageViewHash idStr)]
                // a[Href (sprintf "/#jobview/%s" idStr)]
                    [
                        str job.Title
                        br[]
                    ]
            | None ->
                str job.Title
        )   

let recentJobsContent model =
    match model.Status with
    | LoadingRecentJobs         ->
        h1 [ ClassName "title is-3" ] [ str "Loading recent jobs ..." ]
        |> toList
    | LoadingError errorMessage -> 
        h1 [ ClassName "title is-3" ] [ str errorMessage              ]
        |> toList
    | JobsLoaded jobs           ->
        titleElements jobs

let root model dispatch =
    div []
        [
            div [ ClassName "box" ]
                [
                    p[]
                        [
                            strong [ ClassName "title" ][ str "Job Seekers:  " ]
                            str "Eliminate recruiter spam.  Control exactly the jobs you see."
                        ]
                    p[]
                        [
                            strong [ ClassName "title" ] [ str "Hiring Managers:  " ]
                            str "Know who you're talking to.  Begin your search with better matches."
                        ]
                ]

            div [ClassName "box"]
                (SearchBox.searchBox dispatch SearchTextChanged SubmitSearch model.SearchQuery)

            div [ ClassName "box" ]
                ((label [ ClassName "label" ][ str "Latest Jobs:" ])::(recentJobsContent model))
        ]

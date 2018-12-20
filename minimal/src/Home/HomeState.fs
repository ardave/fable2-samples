module Home.State

open System
open System.Text
open Elmish
open Fable.Core
open Home.Types

let init () : HomeModel * Cmd<HomeMsg> =
    let homeModel = { HomeModel.Empty with Status = LoadingRecentJobs }
    let getNewestJobsCmd = Cmd.ofAsync MyRemoting.sieveApiProxy.GetJobs 12 LoadNewestJobsResponse LoadNewestJobsError
    homeModel, getNewestJobsCmd

let update msg model : HomeModel * Cmd<HomeMsg> =
    match msg with
    | LoadNewestJobs ->
        let cmd = Cmd.ofAsync MyRemoting.sieveApiProxy.GetJobs 12 LoadNewestJobsResponse LoadNewestJobsError
        model, cmd
    | LoadNewestJobsResponse myresp ->
        let jobs =
            myresp
            |> List.map (Domain.Job.FromDto Domain.Job.Direction.FromServer)
            |> JobsLoaded
        { model with Status = jobs }, []
    | LoadNewestJobsError ex      ->
        let errorMessage = sprintf "Error loading newest jobs: %s" ex.Message
        let newHomeModel = { model with Status = LoadingError errorMessage }
        newHomeModel, []
    | SearchTextChanged s ->
        { model with SearchQuery = s }, []
    | SubmitSearch sq             ->
        printfn """Submitting search "%s"  """ sq
        let cmd = Cmd.ofAsync MyRemoting.sieveApiProxy.SearchSimple sq SearchResponse SearchError
        { model with SearchQuery = sq }, cmd
    | SearchResponse jobList ->
        let jobs =
            printfn "Response Received: %A" jobList
            jobList
            |> List.map (Domain.Job.FromDto Domain.Job.Direction.FromServer)
        { model with Status = JobsLoaded jobs }, []
    | SearchError ex              ->
        printfn "Search Error: %s" ex.Message
        model, []

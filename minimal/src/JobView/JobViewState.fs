module Job.View.State

open Elmish
open Fable.Core
open Global
open Job.View.Types

let init() = { CurrentState = Nothing }, []

let update msg model =
    let newModelState, cmd =
        match msg with
        | InitialLoad jobId ->
            let cmd = Cmd.ofAsync MyRemoting.sieveApiProxy.GetJob jobId GetJobByIdResponse GetJobByIdError
            Loading jobId, cmd
        | GetJobByIdResponse jobOpt ->
            // map from dto to domain obj here.
            Loaded jobOpt, []
        | GetJobByIdError e ->
            Error e, []

    { model with CurrentState = newModelState }, cmd

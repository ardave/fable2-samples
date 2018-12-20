namespace Domain

open System
open Sieve.Domain
open DTOs

module Job =
    let strToStrOpt str =
        match String.IsNullOrWhiteSpace str with
        | true  -> None
        | false -> Some str

    type RemoteWork = RemoteWork of string
        with
            member x.String = 
                let (RemoteWork rw) = x
                rw

    type Job = {
        Created            : DateTime
        Description        : string
        Id                 : JobId option
        Title              : string
        SelectedLanguages  : string list
        SelectedFrameworks : string list
        RemoteWork         : RemoteWork
    }

    type Direction =
    | FromServer
    | FromClient

    let private verifyValuesAreValid selectedValues validValues =
        let offendingValues = selectedValues |> List.except validValues
        match offendingValues with
        | [] -> ()
        | _  -> failwithf "The following values are not valid:\n%A" offendingValues

    let FromDto direction (dto:DTOs.Job) =
        // verifyValuesAreValid dto.SelectedFrameworks DataOptions.frameworks
        // verifyValuesAreValid dto.SelectedLanguages  DataOptions.programmingLanguages

        {
            Created            = match direction with
                                 | FromClient -> DateTime.UtcNow
                                 | FromServer -> dto.Created
            Description        = dto.Description
            Id                 = dto.Id// |> strToStrOpt |> Option.map JobId
            Title              = dto.Title
            SelectedFrameworks = dto.SelectedFrameworks
            SelectedLanguages  = dto.SelectedLanguages
            RemoteWork         = RemoteWork dto.RemoteWork
        }

    let toDto (job:Job) : DTOs.Job =
        let (RemoteWork remoteWork) = job.RemoteWork
        {
            Created            = job.Created
            Description        = job.Description
            Id                 = job.Id
            Title              = job.Title
            SelectedFrameworks = job.SelectedFrameworks
            SelectedLanguages  = job.SelectedLanguages
            RemoteWork         = remoteWork
        }

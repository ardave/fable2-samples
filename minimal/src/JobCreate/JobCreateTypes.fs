module Job.Create.Types

open System
open Global
open Sieve.Domain
open AddableDropdown
open Domain.Job
open DTOs

type JobCreateModel = {
    SubmissionInProgress : bool
    Title                : string
    Description          : string
    Created              : DateTime
    SelectedFrameworks   : SelectionCollection
    SelectedLanguages    : SelectionCollection
    RemoteWork           : RemoteWork
} with
    member x.ToDto : DTOs.Job = { 
        Title = x.Title
        Description = x.Description
        Created = x.Created
        Id = None
        SelectedFrameworks = x.SelectedFrameworks.SelectedItems |> List.ofSeq |> List.filter (fun f -> f <> DataOptions.nothingSelected)
        SelectedLanguages  = x.SelectedLanguages.SelectedItems  |> List.ofSeq |> List.filter (fun f -> f <> DataOptions.nothingSelected)
        RemoteWork         = x.RemoteWork.String
    }
    static member Empty = {
        SubmissionInProgress = false
        Title                = ""
        Description          = ""
        Created              = DateTime.UtcNow
        SelectedFrameworks   = SelectionCollection.Empty
        SelectedLanguages    = SelectionCollection.Empty
        RemoteWork           = RemoteWork DataOptions.nothingSelected} 

type JobCreateMessage =
  | TitleChanged              of string
  | DescriptionChanged        of string
  | LanguageSelectionChanged  of string * int
  | FrameworkSelectionChanged of string * int
  | RemoteSelectionChanged    of string * int
  | Submit
  | SubmitWithToken           of Auth.Common.SecurityToken
  | JobPostResponse           of Result<JobId, string>
  | JobPostError              of exn
  | AddProgrammingLanguage
  | AddFramework
  | RemoveProgrammingLanguage of int
  | RemoveFramework           of int



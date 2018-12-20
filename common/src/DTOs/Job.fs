module DTOs

open System
#if FABLE_COMPILER
open Thoth.Json
open Thoth.Json.Decode
#else
open Thoth.Json.Net
#endif

let DateTimeDecoder:Decode.Decoder<DateTime> =
    Decode.object
        (fun get ->
            let y  = get.Required.Field "Year"        Decode.int
            let mo = get.Required.Field "Month"       Decode.int
            let d  = get.Required.Field "Day"         Decode.int
            let h  = get.Required.Field "Hour"        Decode.int
            let mi = get.Required.Field "Minute"      Decode.int
            let s  = get.Required.Field "Second"      Decode.int
            let ms = get.Required.Field "Millisecond" Decode.int
            DateTime(y, mo, d, h, mi, s, ms, DateTimeKind.Utc))

let DateTimeEncoder (dt:DateTime) =
    Encode.object 
        [
            "Year",        Encode.int dt.Year
            "Month",       Encode.int dt.Month
            "Day",         Encode.int dt.Day
            "Hour",        Encode.int dt.Hour
            "Minute",      Encode.int dt.Minute
            "Second",      Encode.int dt.Second
            "Millisecond", Encode.int dt.Millisecond
        ]

type JobId = JobId of string
    with
        static member Decoder:Decode.Decoder<JobId> =
            Decode.map JobId Decode.string
             
        member x.Encode =
            let (JobId str) = x
            Encode.string str

let jobIdEncoder (JobId str) =
    Encode.string str

[<CLIMutable>]
type Job = {
    Created            : DateTime
    Id                 : JobId option
    Description        : string
    Title              : string
    SelectedFrameworks : string list
    SelectedLanguages  : string list
    RemoteWork         : string
} with
    static member Create created id desc title fws langs remote =
        { Created=created; Id=id; Description=desc; Title=title; SelectedFrameworks=fws; SelectedLanguages=langs; RemoteWork=remote }
    static member Decoder:Decode.Decoder<Job> =
        Decode.object
            (fun get ->
                 {
                    Created            = get.Required.Field "Created" DateTimeDecoder
                    Id                 = get.Optional.Field "Id" JobId.Decoder
                    Description        = get.Required.Field "Description" Decode.string  
                    Title              = get.Required.Field "Title" Decode.string
                    SelectedFrameworks = get.Required.Field "SelectedFrameworks" (Decode.list Decode.string)
                    SelectedLanguages  = get.Required.Field "SelectedLanguages" (Decode.list Decode.string)
                    RemoteWork         = get.Required.Field "RemoteWork" Decode.string
                 }
            )

    static member FromJson json = Decode.fromString Job.Decoder json
    member x.Encode =
        Encode.object
            [
                "Created",            DateTimeEncoder x.Created
                "Id",                 (Encode.option jobIdEncoder) x.Id
                "Description",        Encode.string   x.Description
                "Title",              Encode.string   x.Title
                "SelectedFrameworks", Encode.array   (x.SelectedFrameworks |> List.map Encode.string |> Array.ofList)
                "SelectedLanguages",  Encode.array   (x.SelectedLanguages  |> List.map Encode.string |> Array.ofList)
                "RemoteWork",         Encode.string   x.RemoteWork
            ]
    member x.ToJson = x.Encode.ToString()

let private jobListDecoder:Decode.Decoder<Job list> = Decode.list Job.Decoder
let private jobArrayDecoder:Decode.Decoder<Job array> = Decode.array Job.Decoder

let decodeJobList = Decode.fromString jobListDecoder
let decodeJobArray = Decode.fromString jobArrayDecoder

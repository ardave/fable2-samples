module Home.Types
open Global

type HomeStatus =
| LoadingRecentJobs
| LoadingError     of string
| JobsLoaded of Domain.Job.Job list

type HomeModel ={
    SearchQuery   : string
    Status        : HomeStatus
} with
    static member Empty = {
        SearchQuery   = ""
        Status = LoadingRecentJobs
    }

type HomeMsg =
    | LoadNewestJobs
    | LoadNewestJobsResponse of DTOs.Job list
    | LoadNewestJobsError    of exn
    | SearchTextChanged      of string
    | SubmitSearch           of string
    | SearchResponse         of DTOs.Job list
    | SearchError            of exn

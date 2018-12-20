module Job.Search.Types
open Global
open JobSearchRequest

type JobSearchStatus =
| Nothing
| Loading
| ShowMessage of string
| Loaded      of Domain.Job.Job list

let empty() = AddableDropdown.SelectionCollection.Empty

type JobSearchModel = {
    SearchQuery : string
    Status      : JobSearchStatus
    SelectedLanguages  : AddableDropdown.SelectionCollection
    SelectedFrameworks : AddableDropdown.SelectionCollection
} with
    static member Empty = {
            SearchQuery        = ""
            Status             = Nothing
            SelectedLanguages  = empty()
            SelectedFrameworks = empty()
        }
    member x.ToDto:JobSearchRequest.SearchJobs =
        {
            aggs  = createAggs
            query = Term { Description = x.SearchQuery}
            sort = [||]
        }

type JobSearchMessage =
| SearchTextChanged         of string
| SubmitSearch              of string
| SearchResponse            of DTOs.Job list
| SearchError               of exn
| LanguageSelectionChanged  of string * int
| AddProgrammingLanguage
| RemoveProgrammingLanguage of int
| FrameworkSelectionChanged of string * int
| AddFramework
| RemoveFramework           of int

module Job.View.Types
open Global
open Domain.Job
open DTOs

type JobViewMessage =
| InitialLoad        of JobId
| GetJobByIdResponse of DTOs.Job option
| GetJobByIdError    of exn

type JobViewModelState =
| Nothing
| Loading of JobId
| Loaded  of DTOs.Job option
| Error   of exn

// Maybe give the viewmodel some awareness of loading errors
// so that loading errors don't result in endless loops of
// loading attempts:
type JobViewModel = {
    CurrentState : JobViewModelState
}

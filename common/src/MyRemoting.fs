module MyRemoting
open Auth.Common
open JobSearchRequest
open DTOs

// Could probably split this file into shared + client-side code.

type SieveApi = {
    GetJob        : JobId      -> Async<DTOs.Job option>
    GetJobs       : int        -> Async<DTOs.Job list>
    Search        : SearchJobs -> Async<DTOs.Job list>
    SearchSimple  : string     -> Async<DTOs.Job list>
    CreateJob     : SecureRequest<DTOs.Job> -> Async<Result<JobId, string>>
    SignIn        : SignInRequest -> Async<Result<SecurityToken * Email, string>>
    SignUp        : SignUpRequest -> Async<Result<unit, string>>
    VerifyAccount : string -> Async<Result<unit, string>>
}

open Fable.Remoting.Client

let routeBuilder typeName methodName =
    sprintf "/api/%s/%s" typeName methodName

let sieveApiProxy : SieveApi =
    Remoting.createApi()
    |> Remoting.withRouteBuilder routeBuilder
    |> Remoting.buildProxy<SieveApi>

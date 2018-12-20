module SignIn.Types
open Auth.Common

type SignInInfo =
| SignedIn of SecurityToken * Email
| SignInError of string
| NotSignedIn
with
    member x.GetSecurityTokenOrFail =
        match x with
        | SignedIn (token, email) -> token
        | _ -> failwith "Not signed in"

type SignInModel = {
    Email                : Email
    Password             : Password
    SubmissionInProgress : bool
    SignInInfo           : SignInInfo
} with
    static member Empty =
        {
            Email                = Email ""
            Password             = Password ""
            SubmissionInProgress = false
            SignInInfo           = NotSignedIn
        }

type SignInMessage = 
| Submit of SignInRequest
| SignInResponseReceived of Result<SecurityToken * Email, string>
| SignInError of exn
| SignOut
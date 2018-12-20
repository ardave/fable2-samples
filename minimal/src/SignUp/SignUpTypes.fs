module SignUp.Types

open Auth.Common

type SignUpModel = {
    Email                : Email
    Password             : Password
    SubmissionInProgress : bool
    StatusMessage        : string
} with
    static member Empty = { Email = Email ""; Password = Password ""; SubmissionInProgress = false; StatusMessage = "Please enter your details here:" }

type SignUpMessage = 
| Submit of SignUpRequest
| SignUpResponseReceived of Result<unit, string>
| SignUpError of exn
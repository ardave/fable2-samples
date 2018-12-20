module VerifyAccount.Types

type VerifyAccountMessage =
| InitialLoad of string
| VerifyAccountSuccess of Result<unit, string>
| VerifyAccountError of exn

type VerifyAccountModelState =
| Nothing
| Loaded

type VerifyAccountModel = {
    CurrentState : VerifyAccountModelState
}


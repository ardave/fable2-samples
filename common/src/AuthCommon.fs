module Auth.Common

open System
open System.Collections.Generic
open System.Linq

type Email    = Email    of string
type Password = Password of string

type SignInRequest = {
    Email    : Email
    Password : Password
}

type SignUpRequest = SignInRequest


type SecurityToken = {
    Token      : Guid
    Expiration : DateTimeOffset
}
    with
        static member Create() =
            { Token      = Guid.NewGuid()
              Expiration = DateTimeOffset.UtcNow.AddDays 14. }

type SecureRequest<'t> =
    { Token : Guid
      Content : 't }
    with
        static member Create<'t> token (content:'t) =
            { Token = token; Content = content}


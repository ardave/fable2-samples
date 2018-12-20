module Confirmation.Types

type ConfirmationModel = {
    email        : string
    confirmation : string -> string
}
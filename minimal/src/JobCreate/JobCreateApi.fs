module Job.Create.Api

open Fable.PowerPack
open Fable.PowerPack.Fetch
open DTOs
open Global

let private postProps =
    [ RequestProperties.Method HttpMethod.POST
      requestHeaders [ ContentType "application/json" ]]

module Api

open Fable.PowerPack
open Fable.PowerPack.Fetch
open Global

let private propsGET =
    [ RequestProperties.Method HttpMethod.GET
      requestHeaders [ContentType "application/json"]]

let private propsPost body =
    [ RequestProperties.Method HttpMethod.POST
      requestHeaders [ContentType "application/json"]
      RequestProperties.Body (unbox body)]

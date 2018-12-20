[<AutoOpen>]
module Utils

let print x =
    printfn "%A" x
    x

let unpackOrFail = function
    | Ok apiSearchResponse -> apiSearchResponse
    | Error e ->
#if FABLE_COMPILER
        failwithf "%A" e
#else
        System.IO.File.WriteAllText("Errors.txt", e)
        failwithf "Expected Ok but was 'Error %A'.  Detail written to Errors.txt" e
#endif

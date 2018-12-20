module PostResponse

#if FABLE_COMPILER
open Thoth.Json
open Thoth.Json.Decode
#else
open Thoth.Json.Net
open Thoth.Json.Net.Decode
#endif

type Shards = {
    total      : int
    successful : int
    failed     : int
} with
    static member Decoder:Decoder<Shards> =
        decode
            (fun total successful failed -> { total=total; successful=successful; failed=failed } )
            |> required "total"      int
            |> required "successful" int
            |> required "failed"     int

type PostResponse = {
    _index        : string
    _type         : string
    _id           : string
    _version      : int
    result        : string
    _shards       : Shards
    _seq_no       : int
    _primary_term : int
} with
    static member Decoder:Decoder<PostResponse> =
        decode
            (fun idx typ id ver res shrd seqno priterm ->
                { _index        = idx
                  _type         = typ
                  _id           = id
                  _version      = ver
                  result        = res
                  _shards       = shrd
                  _seq_no       = seqno
                  _primary_term = priterm})
            |> required "_index"        string
            |> required "_type"         string
            |> required "_id"           string
            |> required "_version"      int
            |> required "result"        string
            |> required "_shards"       Shards.Decoder
            |> required "_seq_no"       int
            |> required "_primary_term" int
    static member FromJson json = decodeString PostResponse.Decoder json

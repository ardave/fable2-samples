module JobSearchRequest

#if FABLE_COMPILER
open Thoth.Json
open Thoth.Json.Decode
#else
open Thoth.Json.Net
open Thoth.Json.Net.Decode
#endif

let toString x = x.ToString()

type Terms = {
    field : string
} with
    static member Decoder:Decoder<Terms> =
        map (fun field ->
            { field = field } : Terms)
            (field "field" string)



type Framework = {
    terms : Terms
} with
    static member Decoder:Decoder<Framework> =
        map (fun fw ->
            { terms = fw } : Framework)
            (field "terms" Terms.Decoder)


type Language = {
    terms : Terms
} with
    static member Decoder:Decoder<Language> =
        map (fun lang ->
            { terms = lang } : Language)
            (field "terms" Terms.Decoder)

type Aggs = {
    language :  Language
    framework : Framework
} with
    static member Decoder:Decoder<Aggs> =
        map2 (fun lang fw ->
            {
                language  = lang
                framework = fw
            } : Aggs)
            (field "language" Language.Decoder)
            (field "framework" Framework.Decoder)

let createAggs =
    {
        language =
            {
                terms =
                    {
                        field = "SelectedLanguages"
                    }
            }
        framework =
            {
                terms =
                    {
                        field = "SelectedFrameworks"
                    }
            }
    }

type Sort = {
    Created : string
}

type Term = {
    // Name matches that of job DTO, will probably
    // want/need to add others
    Description : string
} with
    static member Decoder:Decoder<Term> =
        map (fun term -> { Description = term })
            (field "Description" string)

type IdValues = {
    values : string array
}

type Query =
| Term of Term
| MatchAll
| ById of IdValues
with
    member x.Encode =
        match x with
        | MatchAll ->
            Encode.object [ "match_all", Encode.object []]
        | Term t   ->
            Encode.object [
                "term", Encode.object
                    [ "Description", Encode.string t.Description ] ]
        | ById ids ->
            Encode.object [
                "ids", Encode.object
                    [ "values", Encode.array (ids.values |> Array.map Encode.string) ] ]
    static member Decoder:Decoder<Query> =
        let MatchAllDecoder:Decoder<Query> =
            Decode.field "match_all" (Decode.succeed MatchAll)

        let QueryTermDecoder:Decoder<Query> =
            Decode.field "term" Term.Decoder
            |> Decode.map Term

        (Decode.oneOf
            [ MatchAllDecoder
              QueryTermDecoder ])

    static member CreateFromStringQuery str =
        Term { Description = str }

type SearchJobs = {
    aggs  : Aggs
    query : Query
    sort  : Sort array
} with
    static member Decoder:Decoder<SearchJobs> =
        map2 (fun aggs query  ->
                { aggs  = aggs
                  query = query
                  sort  = [||] })
             (field "aggs" Aggs.Decoder)
             (field "query" Query.Decoder)
    static member FromJson json = decodeString SearchJobs.Decoder json
    member x.Encode =
        Encode.object
            [
                "query", x.query.Encode
                "aggs", Encode.object
                    [
                        "language", Encode.object
                            [
                                "terms", Encode.object
                                    [ "field", Encode.string x.aggs.language.terms.field ]
                            ]
                        "framework", Encode.object
                            [
                                "terms", Encode.object
                                    [ "field", Encode.string x.aggs.framework.terms.field ]
                            ]
                    ]
            ]
    member x.ToJson =
        let json = x.Encode.ToString()
        printfn "The search looks like:\n%s" json
        json

type SearchByJobId = {
    query : Query
} with
    member x.Encode =
        Encode.object
            [ "query", x.query.Encode ]
    member x.ToJson = x.Encode.ToString()

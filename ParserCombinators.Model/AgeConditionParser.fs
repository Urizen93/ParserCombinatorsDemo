namespace ParserCombinators.Model

open FParsec
open FParsec.Pipes
open Helpers

module AgeConditionParser =
    let private ageKeyword = %ci "AGE"
    
    let private operator = %[
        %">=" >>% GreaterOrEqual
        %'>' >>% Greater
        %"<=" >>% LessOrEqual
        %'<' >>% Less
    ]
    
    let private natural =
        // pint32 predictably parses any int32 value
        pint32
        // |>> is your map function for parsers (LINQ's Select for C#)
        // Some and None represent optional value,
        // which is something you could call "a way to represent nullability"
        |>> fun value -> if value >= 0 then Some value else None
        // <!> is more general map, used here for options
        // <|>% is a resolution operator, meaning that it returns value on it's right hand
        // if value on the left is "null". It's similar to ?? (coalesce operator)
        >>= fun maybeNatural -> preturn <!> maybeNatural <|>% fail "Must be a natural number"
        // attempt backtracks a parser if it failed; here it's there for better error output only
        |> attempt
    
    let ageCondition : Parser<AgeCondition, unit> =
        %% ageKeyword -- spaces -- +.operator -- spaces -- +. natural
        // -|> allows you to build some value based on the output
        // The identity function does the job here, and it's a bit tricky:
        // 'operator' returns int -> AgeCondition, and 'natural' returns int,
        // hence allowing 'operator' result to be trivially applied to 'natural' result
        -|> id